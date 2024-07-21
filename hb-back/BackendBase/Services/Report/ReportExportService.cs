using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services.Report;
using BackendBase.Models;
using BackendBase.Repositories;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace BackendBase.Services.Report;

public class ReportExportService : IReportExportService
{
    private readonly IUserRepository _userRepo;
    private readonly IRecordRepository _recordRepo;
    private readonly IStateUserRepository _stateUserRepo;
    private readonly UserInfo _userInfo;

    public ReportExportService(
        IUserRepository userRepo,
        IStateUserRepository stateUserRepo,
        IRecordRepository recordRepo,
        UserInfo userInfo
    )
    {
        _userRepo = userRepo;
        _stateUserRepo = stateUserRepo;
        _recordRepo = recordRepo;
        _userInfo = userInfo;
    }

    public async Task<IWorkbook> ExportReport(string reportId)
    {
        var user = await _userRepo.GetById(Guid.Parse(_userInfo.GetUserId()));
        var stateUser = await _stateUserRepo.GetById(Guid.Parse(reportId));
        var records = await _recordRepo.Get(Guid.Parse(reportId));
        var workbook = new XSSFWorkbook();

        _addTitlePage(workbook, user, stateUser, records);
        _addAcademicWorkPage(workbook, user, stateUser);
        _addScienceWorkPage(workbook, user, stateUser);
        _addGuidanceWorkPage(workbook, user, stateUser);
        _addEducationalWorkPage(workbook, user, stateUser);
        _addChangesWorkPage(workbook, user, stateUser);

        return workbook;
    }

    class CellData
    {
        public CellData(
            int row,
            int col,
            string content,
            ICellStyle? style = null,
            CellRangeAddress? merge = null
        )
        {
            Row = row;
            Col = col;
            Content = content;
            Style = style;
            Merge = merge;
        }

        public readonly int Row;
        public readonly int Col;
        public readonly string Content;
        public readonly ICellStyle? Style;
        public readonly CellRangeAddress? Merge;
    }

    /* Delegates for different types of pages */
    delegate List<CellData> WriteEvent(Event ev, int offset, ICellStyle style);
    delegate List<CellData> WriteLesson(Lesson lesson, int offset, ICellStyle style);
    delegate List<CellData> WriteComment(Comment comment, int offset, ICellStyle style);

    /* Academic and Science pages */
    private readonly WriteEvent _writeEventLarge = (ev, offset, style) => new List<CellData>{
                new(offset, 0, ev.EventType.Name, style),
                new(offset, 1, ev.StartedAt.ToString("d"), style),
                new(offset, 2, ev.EndedAt.ToString("d"), style),
                new(offset, 3, "", style, new CellRangeAddress(offset, offset, 3, 5))
    };
    private readonly WriteLesson _writeLessonLarge = (lesson, offset, style) => new List<CellData>{
                new(offset, 0, lesson.LessonType.Name, style),
                new(offset, 1, "", style),
                new(offset, 2, "", style),
                new(offset, 3, lesson.PlanDate.ToString() ?? "", style),
                new(offset, 4, lesson.FactDate.ToString() ?? "", style),
                new(offset, 5, "", style),
    };
    private readonly WriteComment _writeCommentLarge = (comment, offset, style) => new List<CellData>{
                new(offset, 0, comment.Content, style),
                new(offset, 1, "", style),
                new(offset, 2, "", style),
                new(offset, 3, comment.PlanDate.ToString() ?? "", style),
                new(offset, 4, comment.FactDate.ToString() ?? "", style),
                new(offset, 5, "", style),
    };

    /* Guidance page */
    private readonly WriteEvent _writeEventGuidance = (ev, offset, style) => new List<CellData>
    {
        new(offset, 0, ev.EventType.Name, style),
        new(offset, 1, "", style),
        new(offset, 2, "", style),
        new(offset, 3, "", style),
        new(offset, 4, "", style),
    };
    private readonly WriteLesson _writeLessonGuidance = (lesson, offset, style) => new List<CellData> { };
    private readonly WriteComment _writeCommentGuidance = (comment, offset, style) => new List<CellData>
    {
        new(offset, 0, comment.Content, style),
        new(offset, 1, "", style),
        new(offset, 2, comment.PlanDate.ToString() ?? "", style),
        new(offset, 3, comment.FactDate.ToString() ?? "", style),
        new(offset, 4, "", style),
    };

    /* Others pages */
    private readonly WriteEvent _writeEventShort = (ev, offset, style) => new List<CellData>
    {
        new(offset, 0, ev.EventType.Name, style, new CellRangeAddress(offset, offset, 0, 1)),
        new(offset, 2, "", style),
        new(offset, 3, "", style),
        new(offset, 4, "", style),
    };
    private readonly WriteLesson _writeLessonShort = (ev, offset, style) => new List<CellData> { };
    private readonly WriteComment _writeCommentShort = (comment, offset, style) => new List<CellData>
    {
        new(offset, 0, comment.Content, style, new CellRangeAddress(offset, offset, 0, 1)),
        new(offset, 2, comment.PlanDate.ToString() ?? "", style),
        new(offset, 3, comment.FactDate.ToString() ?? "", style),
        new(offset, 4, "", style),
    };

    private void _addTitlePage(IWorkbook workbook, User user, StateUser stateUser, ICollection<Record> records)
    {
        ISheet sheet = workbook.CreateSheet("1_Тит");

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        centeredStyle.WrapText = true;

        var underlineStyle = _getUnderlineStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var firstHalfHours = records.Select(x => x.Hours).Sum();
        var secondHalfHours = stateUser.Events.Select(x => x.Lessons).Select(x => x.Select(v => v.PlanDate).Sum()).Sum() +
            stateUser.Events.Select(x => x.Comments).Select(x => x.Select(v => v.PlanDate).Sum()).Sum() ?? 0;

        var pageData = new List<CellData> {
            new(2, 2, "МИНОБРАНАУКИ РОССИИ", centeredStyle, new CellRangeAddress(2, 2, 2, 8)),
            new(3, 2, "ФГБОУ ВО \"Тульский государственный университет\"", centeredStyle, new CellRangeAddress(3, 3, 2, 8)),
            new(5, 1, "Кафедра________________________________________________________", centeredStyle, new CellRangeAddress(5, 5, 1, 9)),
            new(8, 1, "Утверждаю"),
            new(8, 6, "Утверждаю"),
            new(9, 1, "Проректор по научной работе"),
            new(9, 6, "Проректор по научной работе"),
            new(11, 1, "___________________________"),
            new(11, 6, "___________________________"),
            new(12, 1, "«____» ______________  _____ г."),
            new(12, 6, "«____» ______________  _____ г."),
            new(15, 1, "Директор института"),
            new(15, 3, "_________", null, new CellRangeAddress(15, 15, 3, 8)),
            new(17, 1, "___________________________"),
            new(18, 1, "«____» ______________  _____ г."),
            new(22, 3, "ИНДИВИДУАЛЬНЫЙ ПЛАН", centeredStyle, new CellRangeAddress(22, 22, 3, 7)),
            new(23, 3, "РАБОТЫ ПРЕПОДАВАТЕЛЯ", centeredStyle, new CellRangeAddress(23, 23, 3, 7)),
            new(25, 3, "на", centeredStyle),
            new(25, 4, stateUser.State.StartDate.Year.ToString(), centeredStyle),
            new(25, 5, "-", centeredStyle),
            new(25, 6, stateUser.State.EndDate.Year.ToString(), centeredStyle),
            new(25, 7, "уч. г.г.", centeredStyle),
            new(28, 1, stateUser.State.Job.Name + " " + user.Lastname + " " + user.Firstname[0] + ". " + "(" + stateUser.Rate.ToString() + ")", underlineStyle, new CellRangeAddress(28, 28, 1, 8)),
            new(29, 1, "должность,ученая степень, ученое звание,  фамилия, инициалы, (должн. исполнение)", centeredStyle, new CellRangeAddress(29, 29, 1, 8)),
            new(32, 1, "Дата", tableStyle),
            new(32, 2, "Сведения о заключении трудового договора, присвоении учёной степени и учёного звания, квалификации «Преподаватель высшей школы»", tableStyle, new CellRangeAddress(32, 32, 2, 6)),
            new(32, 7, "Номер договора, диплома, аттестата, приказа", tableStyle, new CellRangeAddress(32, 32, 7, 8)),
            new(33, 1, "", tableStyle),
            new(33, 2, "", tableStyle, new CellRangeAddress(33, 33, 2, 6)),
            new(33, 7, "", tableStyle, new CellRangeAddress(33, 33, 7, 8)),
            new(41, 2, "1 половина рабочего дня", centeredStyle, new CellRangeAddress(41, 41, 2, 4)),
            new(41, 5, firstHalfHours.ToString(), centeredStyle),
            new(41, 6, "- час", centeredStyle),
            new(42, 2, "2 половина рабочего дня", centeredStyle, new CellRangeAddress(42, 42, 2, 4)),
            new(42, 5, secondHalfHours.ToString(), centeredStyle),
            new(42, 6, "- час", centeredStyle),
            new(44, 4, "Всего:"),
            new(44, 5, (firstHalfHours + secondHalfHours).ToString(), underlineStyle),
            new(44, 6, "часов")
        };

        _applyPageData(sheet, pageData);
    }

    private void _addAcademicWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        ISheet sheet = workbook.CreateSheet("4_УЧ-МЕТ");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(5, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>{
            new(0, 0, "II.  УЧЕБНО-МЕТОДИЧЕСКАЯ РАБОТА", headerStyle, new CellRangeAddress(0, 0, 0, 5)),
            new(2, 0, "Наименование работы", tableStyle, new CellRangeAddress(2, 3, 0, 0)),
            new(2, 1, "Срок выполнения", tableStyle, new CellRangeAddress(2, 2, 1, 2)),
            new(2, 3, "Затраты времени, час", tableStyle, new CellRangeAddress(2, 2, 3, 4)),
            new(2, 5, "Отметка зав.каф.о выполнении", tableStyle, new CellRangeAddress(2, 3, 5, 5)),
            new(3, 1, "Начало", tableStyle),
            new(3, 2, "Конец", tableStyle),
            new(3, 3, "План", tableStyle),
            new(3, 4, "Факт.", tableStyle),
        };

        var offset = 4;

        var events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.AcademicMethodicalWorkId).ToList();

        _applyEventsData(
                pageData,
                events,
                ref offset,
                tableStyle,
                _writeEventLarge,
                _writeLessonLarge,
                _writeCommentLarge
        );

        var planHours = _getPlanHours(events);
        var factHours = _getFactHours(events);

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 2)),
            new(offset, 3, planHours.ToString(), tableStyle),
            new(offset, 4, factHours.ToString(), tableStyle),
            new(offset, 5, "", tableStyle)
        });

        _applyPageData(sheet, pageData);
    }

    private void _addScienceWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        ISheet sheet = workbook.CreateSheet("5_НДиНМД");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(5, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>{
            new(0, 0, "III.  НАУЧНАЯ И НАУЧНО-МЕТОДИЧЕСКАЯ ДЕЯТЕЛЬНОСТЬ", headerStyle, new CellRangeAddress(0, 0, 0, 5)),
            new(2, 0, "Наименование работы", tableStyle, new CellRangeAddress(2, 3, 0, 0)),
            new(2, 1, "Срок выполнения", tableStyle, new CellRangeAddress(2, 2, 1, 2)),
            new(2, 3, "Затраты времени, час", tableStyle, new CellRangeAddress(2, 2, 3, 4)),
            new(2, 5, "Отметка зав.каф.о выполнении", tableStyle, new CellRangeAddress(2, 3, 5, 5)),
            new(3, 1, "Начало", tableStyle),
            new(3, 2, "Конец", tableStyle),
            new(3, 3, "План", tableStyle),
            new(3, 4, "Факт.", tableStyle),
        };

        var offset = 4;

        var events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.ScienceWorkId).ToList();

        _applyEventsData(
                pageData,
                events,
                ref offset,
                tableStyle,
                _writeEventLarge,
                _writeLessonLarge,
                _writeCommentLarge
        );

        var planHours = _getPlanHours(events);
        var factHours = _getFactHours(events);

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 2)),
            new(offset, 3, planHours.ToString(), tableStyle),
            new(offset, 4, factHours.ToString(), tableStyle),
            new(offset, 5, "", tableStyle)
        });

        _applyPageData(sheet, pageData);
    }

    private void _addGuidanceWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        ISheet sheet = workbook.CreateSheet("6_НИРС.ОМР");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(1, 24 * 256);
        sheet.SetColumnWidth(4, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var offset = 0;
        var pageData = new List<CellData> { };

        var events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.GuidanceWorkId).ToList();
        _applyGuidanceTable(events, pageData, ref offset, headerStyle, tableStyle);
        events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.OrganizationalMethodicalWorkId).ToList();
        _applyOrganizationalTable(events, pageData, ref offset, headerStyle, tableStyle);

        _applyPageData(sheet, pageData);
    }

    private void _applyGuidanceTable(ICollection<Event> events, List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "IV. РУКОВОДСТВО НАУЧНО-ИССЛЕДОВАТЕЛЬСКОЙ РАБОТОЙ СТУДЕНТОВ", headerStyle, new CellRangeAddress(offset, offset, 0, 4)),
            new(offset + 1, 0, "Виды работ", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 0, 0)),
            new(offset + 1, 1, "Ф.И.О. студента,  № группы", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 1, 1)),
            new(offset + 1, 2, "Затраты времени, час", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 2, 3)),
            new(offset + 1, 4, "Отметка зав.каф. о выполнении", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 4, 4)),
            new(offset + 2, 2, "План", tableStyle),
            new(offset + 2, 3, "Факт.", tableStyle),
        });

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            tableStyle,
            _writeEventGuidance,
            _writeLessonGuidance,
            _writeCommentGuidance
        );

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
            new(offset, 3, _getFactHours(events).ToString(), tableStyle),
            new(offset, 4, "", tableStyle),
        });

        offset += 1;
    }

    private void _applyOrganizationalTable(ICollection<Event> events, List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "V. ОРГАНИЗАЦИОННО-МЕТОДИЧЕСКАЯ РАБОТА", headerStyle, new CellRangeAddress(offset, offset, 0, 4)),
            new(offset + 1, 0, "Виды работ", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 0, 1)),
            new(offset + 1, 2, "Затраты времени, час", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 2, 3)),
            new(offset + 1, 4, "Отметка зав.каф. о выполнении", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 4, 4)),
            new(offset + 2, 2, "План", tableStyle),
            new(offset + 2, 3, "Факт.", tableStyle),
        });

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort
        );

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
            new(offset, 3, _getFactHours(events).ToString(), tableStyle),
            new(offset, 4, "", tableStyle),
        });

        offset += 1;
    }

    private void _addEducationalWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        ISheet sheet = workbook.CreateSheet("7_ВР ДПО");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(4, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var offset = 0;
        var pageData = new List<CellData> { };

        var events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.EducationalWorkId).ToList();
        _applyEducationalTable(events, pageData, ref offset, headerStyle, tableStyle);
        events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.MedicalWorkId).ToList();
        _applyMedicalTable(events, pageData, ref offset, headerStyle, tableStyle);
        events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.ExtraWorkId).ToList();
        _applyExtraTable(events, pageData, ref offset, headerStyle, tableStyle);
        _applyRecommendationsTable(pageData, ref offset, headerStyle, tableStyle);

        _applyPageData(sheet, pageData);
    }

    private void _applyEducationalTable(List<Event> events, List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "VI. ВОСПИТАТЕЛЬНАЯ РАБОТА", headerStyle, new CellRangeAddress(offset, offset, 0, 4)),
            new(offset + 1, 0, "Виды работ", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 0, 1)),
            new(offset + 1, 2, "Затраты времени, час", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 2, 3)),
            new(offset + 1, 4, "Отметка зав.каф. о выполнении", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 4, 4)),
            new(offset + 2, 2, "План", tableStyle),
            new(offset + 2, 3, "Факт.", tableStyle),
        });

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort
        );

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
            new(offset, 3, _getFactHours(events).ToString(), tableStyle),
            new(offset, 4, "", tableStyle),
        });

        offset += 1;
    }

    private void _applyMedicalTable(List<Event> events, List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "VII. ОСУЩЕСТВЛЕНИЕ МЕДИЦИНСКОЙ ДЕЯТЕЛЬНОСТИ, НЕОБХОДИМОЙ ДЛЯ ПРАКТИЧЕСКОЙ ПОДГОТОВКИ ОБУЧАЮЩИХСЯ", headerStyle, new CellRangeAddress(offset, offset, 0, 4)),
        });

        offset += 1;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort
        );

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
            new(offset, 3, _getFactHours(events).ToString(), tableStyle),
            new(offset, 4, "", tableStyle),
        });

        offset += 1;
    }

    private void _applyExtraTable(List<Event> events, List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "VIII. ДОПОЛНИТЕЛЬНОЕ ПРОФЕССИОНАЛЬНОЕ ОБРАЗОВАНИЕ ПО ПРОФИЛЮ ПЕДАГОГИЧЕСКОЙ ДЕЯТЕЛЬНОСТИ", headerStyle, new CellRangeAddress(offset, offset, 0, 4)),
            new(offset + 1, 0, "Виды работ", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 0, 1)),
            new(offset + 1, 2, "Затраты времени, час", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 2, 3)),
            new(offset + 1, 4, "Отметка зав.каф. о выполнении", tableStyle, new CellRangeAddress(offset + 1, offset + 2, 4, 4)),
            new(offset + 2, 2, "План", tableStyle),
            new(offset + 2, 3, "Факт.", tableStyle),
        });

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort
        );

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
            new(offset, 3, _getFactHours(events).ToString(), tableStyle),
            new(offset, 4, "", tableStyle),
        });

        offset += 1;
    }

    private void _applyRecommendationsTable(List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "IX. РЕКОМЕНДАЦИИ КАФЕДРЫ ПО ОТЧЕТУ ПРЕПОДАВАТЕЛЯ", headerStyle, new CellRangeAddress(offset, offset, 0, 4)),
            new(offset + 1, 0, "", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 0, 4))
        });

        offset += 2;
    }

    private void _addChangesWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        ISheet sheet = workbook.CreateSheet("8_ИРИП ОПВП");

        sheet.SetColumnWidth(3, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);
        var underlineStyle = _getUnderlineStyle(workbook);

        var offset = 0;
        var pageData = new List<CellData> { };

        _addChangesTable(pageData, ref offset, headerStyle, tableStyle);
        _addCommentsTable(pageData, ref offset, headerStyle, tableStyle);
        _addSignsTable(pageData, ref offset, underlineStyle);

        _applyPageData(sheet, pageData);
    }

    private void _addChangesTable(List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "X. ИЗМЕНЕНИЕ ИНДИВИДУАЛЬНОГО ПЛАНА", headerStyle, new CellRangeAddress(offset, offset, 0, 3)),
            new(offset + 1, 0, "Вводимые изменения", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 0, 1)),
            new(offset + 1, 2, "Раздел плана", tableStyle),
            new(offset + 1, 3, "№ протокола заседания кафедры, дата", tableStyle)
        });

        offset += 2;

        for (int i = 0; i < 3; i++)
        {
            pageData.AddRange(new List<CellData>{
                new(offset, 0, "", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
                new(offset, 2, "", tableStyle),
                new(offset, 3, "", tableStyle)
            });

            offset += 1;
        }
    }

    private void _addCommentsTable(List<CellData> pageData, ref int offset, ICellStyle headerStyle, ICellStyle tableStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 0, "ОТМЕТКИ О ПРОВЕРКЕ ВЫПОЛНЕНИЯ ПЛАНА", headerStyle, new CellRangeAddress(offset, offset, 0, 3)),
            new(offset + 1, 0, "Замечания", tableStyle),
            new(offset + 1, 1, "Фамилия, И.О., должность проверяющего", tableStyle, new CellRangeAddress(offset + 1, offset + 1, 1, 2)),
            new(offset + 1, 3, "Подпись преподавателя", tableStyle)
        });

        offset += 2;

        for (int i = 0; i < 3; i++)
        {
            pageData.AddRange(new List<CellData>{
                new(offset, 0, "", tableStyle),
                new(offset, 1, "", tableStyle, new CellRangeAddress(offset, offset, 1, 2)),
                new(offset, 3, "", tableStyle)
            });

            offset += 1;
        }
    }

    private void _addSignsTable(List<CellData> pageData, ref int offset, ICellStyle underlineStyle)
    {
        pageData.AddRange(new List<CellData>{
            new(offset, 1, "План обсужден на заседании кафедры", merge: new CellRangeAddress(offset, offset, 1, 3)),
            new(offset + 1, 1, "____ __________20__ г.       протокол №  ______", merge: new CellRangeAddress(offset + 1, offset + 1, 1, 3)),
            new(offset + 2, 1, "Заведующий кафедрой", merge: new CellRangeAddress(offset + 2, offset + 2, 1, 2)),
            new(offset + 2, 3, "", underlineStyle),
            new(offset + 3, 1, "Подпись преподавателя", merge: new CellRangeAddress(offset + 3, offset + 3, 1, 2)),
            new(offset + 3, 3, "", underlineStyle),
        });

        offset += 4;

        pageData.AddRange(new List<CellData>{
            new(offset, 1, "Выполнение плана в осеннем семестре проверено", merge: new CellRangeAddress(offset, offset, 1, 3)),
            new(offset + 1, 1, "и обсуждено на заседании кафедры", merge: new CellRangeAddress(offset + 1, offset + 1, 1, 3)),
            new(offset + 2, 1, "____ __________20__ г.       протокол №  ______", merge: new CellRangeAddress(offset + 2, offset + 2, 1, 3)),
            new(offset + 3, 1, "Заведующий кафедрой", merge: new CellRangeAddress(offset + 3, offset + 3, 1, 2)),
            new(offset + 3, 3, "", underlineStyle),
            new(offset + 4, 1, "Подпись преподавателя", merge: new CellRangeAddress(offset + 4, offset + 4, 1, 2)),
            new(offset + 4, 3, "", underlineStyle),
        });

        offset += 5;

        pageData.AddRange(new List<CellData>{
            new(offset, 1, "Выполнение плана в весеннем семестре и за уч. год", merge: new CellRangeAddress(offset, offset, 1, 3)),
            new(offset + 1, 1, "проверено и обсуждено на заседании кафедры", merge: new CellRangeAddress(offset + 1, offset + 1, 1, 3)),
            new(offset + 2, 1, "____ __________20__ г.       протокол №  ______", merge: new CellRangeAddress(offset + 2, offset + 2, 1, 3)),
            new(offset + 3, 1, "Заведующий кафедрой", merge: new CellRangeAddress(offset + 3, offset + 3, 1, 2)),
            new(offset + 3, 3, "", underlineStyle),
            new(offset + 4, 1, "Подпись преподавателя", merge: new CellRangeAddress(offset + 4, offset + 4, 1, 2)),
            new(offset + 4, 3, "", underlineStyle),
        });

        offset += 5;
    }

    private ICellStyle _getHeaderStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        style.WrapText = true;
        return style;
    }

    private ICellStyle _getUnderlineStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        style.WrapText = true;
        return style;
    }

    private ICellStyle _getTableStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        style.WrapText = true;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        return style;
    }

    private int _getPlanHours(ICollection<Event> events)
    {
        return events.Select(x => x.Lessons.Select(l => l.PlanDate).Select(l => l ?? 0).Sum()).Sum() +
            events.Select(x => x.Comments.Select(c => c.PlanDate).Select(c => c ?? 0).Sum()).Sum();
    }

    private int _getFactHours(ICollection<Event> events)
    {
        return events.Select(x => x.Lessons.Select(l => l.FactDate).Select(l => l ?? 0).Sum()).Sum() +
            events.Select(x => x.Comments.Select(c => c.FactDate).Select(c => c ?? 0).Sum()).Sum();
    }

    private void _applyPageData(ISheet sheet, ICollection<CellData> pageData)
    {
        foreach (CellData data in pageData)
        {
            var row = sheet.GetRowSafe(data.Row);
            var cell = row.GetCellSafe(data.Col);

            cell.SetCellValue(data.Content);

            if (data.Merge != null)
            {
                sheet.AddMergedRegion(data.Merge);

                for (int i = data.Merge.FirstRow; i <= data.Merge.LastRow; i++)
                {
                    var tmpRow = sheet.GetRowSafe(i);
                    for (int j = data.Merge.FirstColumn; j <= data.Merge.LastColumn; j++)
                    {
                        var tmpCol = tmpRow.GetCellSafe(j);
                        tmpCol.CellStyle = data.Style;
                    }
                }
            }
            if (data.Style != null)
            {
                cell.CellStyle = data.Style;
            }
        }
    }

    private void _applyEventsData(
            List<CellData> pageData,
            ICollection<Event> events,
            ref int offset,
            ICellStyle style,
            WriteEvent writeEvent,
            WriteLesson writeLesson,
            WriteComment writeComment
    )
    {
        foreach (var ev in events)
        {
            pageData.AddRange(writeEvent(ev, offset, style));
            offset += 1;

            foreach (var comment in ev.Comments)
            {
                pageData.AddRange(writeComment(comment, offset, style));
                offset += 1;
            }
            foreach (var lesson in ev.Lessons)
            {
                pageData.AddRange(writeLesson(lesson, offset, style));
                offset += 1;
            }
        }
    }
}
