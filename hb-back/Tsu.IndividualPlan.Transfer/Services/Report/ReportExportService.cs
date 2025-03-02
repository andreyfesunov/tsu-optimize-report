using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.Transfer.Extensions.Lib;
using Tsu.IndividualPlan.Transfer.Interfaces.Report;

namespace Tsu.IndividualPlan.Transfer.Services.Report;

public class ReportExportService : IReportExportService
{
    private readonly IRecordRepository _recordRepo;
    private readonly IStateUserRepository _stateUserRepo;
    private readonly UserInfo _userInfo;
    private readonly IUserRepository _userRepo;

    private readonly WriteComment _writeCommentGuidance = (comment, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, comment.Content, style),
            new(offset, 1, "", style),
            new(offset, 2, comment.PlanDate.ToString() ?? "", style),
            new(offset, 3, comment.FactDate.ToString() ?? "", style),
            new(offset, 4, "", style)
        };

    private readonly WriteComment _writeCommentLarge = (comment, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, comment.Content, style),
            new(offset, 1, "", style),
            new(offset, 2, "", style),
            new(offset, 3, comment.PlanDate.ToString() ?? "", style),
            new(offset, 4, comment.FactDate.ToString() ?? "", style),
            new(offset, 5, "", style)
        };

    private readonly WriteComment _writeCommentShort = (comment, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, comment.Content, style, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, comment.PlanDate.ToString() ?? "", style),
            new(offset, 3, comment.FactDate.ToString() ?? "", style),
            new(offset, 4, "", style)
        };

    private readonly WriteEmpty _writeEmptyGuidance = (offset, style) =>
        new List<CellData>
        {
            new(offset, 0, "", style),
            new(offset, 1, "", style),
            new(offset, 2, "", style),
            new(offset, 3, "", style),
            new(offset, 4, "", style)
        };

    private readonly WriteEmpty _writeEmptyLarge = (offset, style) =>
        new List<CellData>
        {
            new(offset, 0, "", style),
            new(offset, 1, "", style),
            new(offset, 2, "", style),
            new(offset, 3, "", style),
            new(offset, 4, "", style),
            new(offset, 5, "", style)
        };

    private readonly WriteEmpty _writeEmptyShort = (offset, style) =>
        new List<CellData>
        {
            new(offset, 0, "", style, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, "", style),
            new(offset, 3, "", style),
            new(offset, 4, "", style)
        };

    /* Guidance page */
    private readonly WriteEvent _writeEventGuidance = (ev, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, ev.EventType.Name, style),
            new(offset, 1, "", style),
            new(offset, 2, "", style),
            new(offset, 3, "", style),
            new(offset, 4, "", style)
        };

    /* Academic and Science pages */
    private readonly WriteEvent _writeEventLarge = (ev, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, ev.EventType.Name, style),
            new(offset, 1, ev.StartedAt.ToString("MM.dd.yyyy"), style),
            new(offset, 2, ev.EndedAt.ToString("MM.dd.yyyy"), style),
            new(offset, 3, "", style, new CellRangeAddress(offset, offset, 3, 5))
        };

    /* Others pages */
    private readonly WriteEvent _writeEventShort = (ev, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, ev.EventType.Name, style, new CellRangeAddress(offset, offset, 0, 1)),
            new(offset, 2, "", style),
            new(offset, 3, "", style),
            new(offset, 4, "", style)
        };

    private readonly WriteLesson _writeLessonGuidance = (lesson, offset, style) =>
        new List<CellData>();

    private readonly WriteLesson _writeLessonLarge = (lesson, offset, style) =>
        new List<CellData>
        {
            new(offset, 0, lesson.LessonType.Name, style),
            new(offset, 1, "", style),
            new(offset, 2, "", style),
            new(offset, 3, lesson.PlanDate.ToString() ?? "", style),
            new(offset, 4, lesson.FactDate.ToString() ?? "", style),
            new(offset, 5, "", style)
        };

    private readonly WriteLesson _writeLessonShort = (ev, offset, style) => new List<CellData>();

    private readonly string _root;

    public ReportExportService(
        IUserRepository userRepo,
        IStateUserRepository stateUserRepo,
        IRecordRepository recordRepo,
        UserInfo userInfo,
        IHostEnvironment env, IConfiguration conf
    )
    {
        _userRepo = userRepo;
        _stateUserRepo = stateUserRepo;
        _recordRepo = recordRepo;
        _userInfo = userInfo;

        _root = Path.Combine(env.ContentRootPath, conf["Storage:Folder"] ?? throw new InvalidOperationException());
    }

    public async Task<IWorkbook> ExportReport(string reportId)
    {
        var stateUser = await _stateUserRepo.GetById(Guid.Parse(reportId));
        var user = await _userRepo.GetById(stateUser.UserId);
        var records = await _recordRepo.Get(Guid.Parse(reportId));
        var workbook = new XSSFWorkbook();

        _addTitlePage(workbook, user, stateUser, records);
        await _addFirstHalfPages(workbook, user, stateUser);
        _addAcademicWorkPage(workbook, user, stateUser, 54);
        _addScienceWorkPage(workbook, user, stateUser, 54);
        _addGuidanceWorkPage(workbook, user, stateUser, 26);
        _addEducationalWorkPage(workbook, user, stateUser);
        _addChangesWorkPage(workbook, user, stateUser);

        return workbook;
    }

    private void _addTitlePage(
        IWorkbook workbook,
        User user,
        StateUser stateUser,
        ICollection<Record> records
    )
    {
        var sheet = workbook.CreateSheet("1_Тит");

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = HorizontalAlignment.Center;
        centeredStyle.WrapText = true;

        var underlineStyle = _getUnderlineStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var firstHalfHours = records.Select(x => x.Hours).Sum();
        var secondHalfHours =
            stateUser
                .Events.Select(x => x.Lessons)
                .Select(x => x.Select(v => v.PlanDate).Sum())
                .Sum()
            + stateUser
                .Events.Select(x => x.Comments)
                .Select(x => x.Select(v => v.PlanDate).Sum())
                .Sum()
            ?? 0;

        var pageData = new List<CellData>
        {
            new(2, 2, "МИНОБРАНАУКИ РОССИИ", centeredStyle, new CellRangeAddress(2, 2, 2, 8)),
            new(
                3,
                2,
                "ФГБОУ ВО \"Тульский государственный университет\"",
                centeredStyle,
                new CellRangeAddress(3, 3, 2, 8)
            ),
            new(
                5,
                1,
                "Кафедра________________________________________________________",
                centeredStyle,
                new CellRangeAddress(5, 5, 1, 9)
            ),
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
            new(
                28,
                1,
                stateUser.State.Job.Name
                + " "
                + user.Lastname
                + " "
                + user.Firstname[0]
                + ". "
                + "("
                + stateUser.Rate
                + ")",
                underlineStyle,
                new CellRangeAddress(28, 28, 1, 8)
            ),
            new(
                29,
                1,
                "должность,ученая степень, ученое звание,  фамилия, инициалы, (должн. исполнение)",
                centeredStyle,
                new CellRangeAddress(29, 29, 1, 8)
            ),
            new(32, 1, "Дата", tableStyle),
            new(
                32,
                2,
                "Сведения о заключении трудового договора, присвоении учёной степени и учёного звания, квалификации «Преподаватель высшей школы»",
                tableStyle,
                new CellRangeAddress(32, 32, 2, 6)
            ),
            new(
                32,
                7,
                "Номер договора, диплома, аттестата, приказа",
                tableStyle,
                new CellRangeAddress(32, 32, 7, 8)
            ),
            new(33, 1, "", tableStyle),
            new(33, 2, "", tableStyle, new CellRangeAddress(33, 33, 2, 6)),
            new(33, 7, "", tableStyle, new CellRangeAddress(33, 33, 7, 8)),
            new(
                41,
                2,
                "1 половина рабочего дня",
                centeredStyle,
                new CellRangeAddress(41, 41, 2, 4)
            ),
            new(41, 5, firstHalfHours.ToString(), centeredStyle),
            new(41, 6, "- час", centeredStyle),
            new(
                42,
                2,
                "2 половина рабочего дня",
                centeredStyle,
                new CellRangeAddress(42, 42, 2, 4)
            ),
            new(42, 5, secondHalfHours.ToString(), centeredStyle),
            new(42, 6, "- час", centeredStyle),
            new(44, 4, "Всего:"),
            new(44, 5, (firstHalfHours + secondHalfHours).ToString(), underlineStyle),
            new(44, 6, "часов")
        };

        _applyPageData(sheet, pageData);
    }

    private async Task _addFirstHalfPages(IWorkbook workbook, User user, StateUser stateUser)
    {
        var firstHalfFile = stateUser.Files.ToList()[0];
        string firstHalfFileExtension = Path.GetExtension(firstHalfFile.Path).ToLower();
        var path = Path.Combine(_root, firstHalfFile.Path);
        await using var fs = new FileStream(path, FileMode.Open);

        using var package = firstHalfFileExtension == ".xlsx"
            ? (IWorkbook)new XSSFWorkbook(fs)
            : (IWorkbook)new HSSFWorkbook(fs);
        for (var i = 1; i < package.NumberOfSheets; i++)
        {
            var sourceSheet = package.GetSheetAt(i);
            var newSheet = workbook.CreateSheet(i == 1 ? "2_Осень" : "3_Весна");

            for (var rowIndex = sourceSheet.FirstRowNum; rowIndex <= sourceSheet.LastRowNum; rowIndex++)
            {
                var sourceRow = sourceSheet.GetRow(rowIndex);
                if (sourceRow == null) continue;

                var newRow = newSheet.CreateRow(rowIndex);
                for (int cellIndex = sourceRow.FirstCellNum; cellIndex < sourceRow.LastCellNum; cellIndex++)
                {
                    var sourceCell = sourceRow.GetCell(cellIndex);
                    var newCell = newRow.CreateCell(cellIndex);
                    if (sourceCell == null) continue;

                    newCell.SetCellValue(sourceCell.ToString());

                    var newCellStyle = workbook.CreateCellStyle();
                    var sourceCellStyle = firstHalfFileExtension == ".xlsx"
                        ? (ICellStyle)(XSSFCellStyle)sourceCell.CellStyle
                        : (ICellStyle)(HSSFCellStyle)sourceCell.CellStyle;

                    if (sourceCellStyle != null)
                    {
                        var fontIndex = sourceCellStyle.FontIndex;
                        var font = package.GetFontAt(fontIndex);

                        var newFont = workbook.CreateFont();
                        newFont.FontName = font.FontName;
                        newFont.FontHeightInPoints = font.FontHeightInPoints;
                        newFont.Color = font.Color;
                        newFont.Boldweight = font.Boldweight;
                        newFont.IsItalic = font.IsItalic;
                        newFont.Underline = font.Underline;

                        newCellStyle.SetFont(newFont);
                        newCellStyle.Alignment = sourceCellStyle.Alignment;
                        newCellStyle.VerticalAlignment = sourceCellStyle.VerticalAlignment;
                        newCellStyle.BorderBottom = sourceCellStyle.BorderBottom;
                        newCellStyle.BorderTop = sourceCellStyle.BorderTop;
                        newCellStyle.BorderLeft = sourceCellStyle.BorderLeft;
                        newCellStyle.BorderRight = sourceCellStyle.BorderRight;
                        newCellStyle.FillForegroundColor = sourceCellStyle.FillForegroundColor;
                        newCellStyle.FillPattern = sourceCellStyle.FillPattern;
                        newCellStyle.WrapText = sourceCellStyle.WrapText;
                    }

                    newCell.CellStyle = newCellStyle;
                }
            }

            for (var j = 0; j < sourceSheet.NumMergedRegions; j++)
            {
                var mergedRegion = sourceSheet.GetMergedRegion(j);
                newSheet.AddMergedRegion(new CellRangeAddress(
                    mergedRegion.FirstRow,
                    mergedRegion.LastRow,
                    mergedRegion.FirstColumn,
                    mergedRegion.LastColumn));
            }
        }

    }

    private void _addAcademicWorkPage(
        IWorkbook workbook,
        User user,
        StateUser stateUser,
        int minCount
    )
    {
        var sheet = workbook.CreateSheet("4_УЧ-МЕТ");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(5, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>
        {
            new(
                0,
                0,
                "II.  УЧЕБНО-МЕТОДИЧЕСКАЯ РАБОТА",
                headerStyle,
                new CellRangeAddress(0, 0, 0, 5)
            ),
            new(2, 0, "Наименование работы", tableStyle, new CellRangeAddress(2, 3, 0, 0)),
            new(2, 1, "Срок выполнения", tableStyle, new CellRangeAddress(2, 2, 1, 2)),
            new(2, 3, "Затраты времени, час", tableStyle, new CellRangeAddress(2, 2, 3, 4)),
            new(2, 5, "Отметка зав.каф.о выполнении", tableStyle, new CellRangeAddress(2, 3, 5, 5)),
            new(3, 1, "Начало", tableStyle),
            new(3, 2, "Конец", tableStyle),
            new(3, 3, "План", tableStyle),
            new(3, 4, "Факт.", tableStyle)
        };

        var offset = 4;

        var events = stateUser.Events.Where(x =>
                x.EventType.WorkId.ToString() == SystemWorks.AcademicMethodicalWorkId
            )
            .ToList();

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventLarge,
            _writeLessonLarge,
            _writeCommentLarge,
            _writeEmptyLarge
        );

        var planHours = _getPlanHours(events);
        var factHours = _getFactHours(events);

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 2)
                ),
                new(offset, 3, planHours.ToString(), tableStyle),
                new(offset, 4, factHours.ToString(), tableStyle),
                new(offset, 5, "", tableStyle)
            }
        );

        _applyPageData(sheet, pageData);
    }

    private void _addScienceWorkPage(
        IWorkbook workbook,
        User user,
        StateUser stateUser,
        int minCount
    )
    {
        var sheet = workbook.CreateSheet("5_НДиНМД");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(5, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>
        {
            new(
                0,
                0,
                "III.  НАУЧНАЯ И НАУЧНО-МЕТОДИЧЕСКАЯ ДЕЯТЕЛЬНОСТЬ",
                headerStyle,
                new CellRangeAddress(0, 0, 0, 5)
            ),
            new(2, 0, "Наименование работы", tableStyle, new CellRangeAddress(2, 3, 0, 0)),
            new(2, 1, "Срок выполнения", tableStyle, new CellRangeAddress(2, 2, 1, 2)),
            new(2, 3, "Затраты времени, час", tableStyle, new CellRangeAddress(2, 2, 3, 4)),
            new(2, 5, "Отметка зав.каф.о выполнении", tableStyle, new CellRangeAddress(2, 3, 5, 5)),
            new(3, 1, "Начало", tableStyle),
            new(3, 2, "Конец", tableStyle),
            new(3, 3, "План", tableStyle),
            new(3, 4, "Факт.", tableStyle)
        };

        var offset = 4;

        var events = stateUser
            .Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.ScienceWorkId)
            .ToList();

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventLarge,
            _writeLessonLarge,
            _writeCommentLarge,
            _writeEmptyLarge
        );

        var planHours = _getPlanHours(events);
        var factHours = _getFactHours(events);

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 2)
                ),
                new(offset, 3, planHours.ToString(), tableStyle),
                new(offset, 4, factHours.ToString(), tableStyle),
                new(offset, 5, "", tableStyle)
            }
        );

        _applyPageData(sheet, pageData);
    }

    private void _addGuidanceWorkPage(
        IWorkbook workbook,
        User user,
        StateUser stateUser,
        int minCount
    )
    {
        var sheet = workbook.CreateSheet("6_НИРС.ОМР");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(1, 24 * 256);
        sheet.SetColumnWidth(4, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var offset = 0;
        var pageData = new List<CellData>();

        var events = stateUser
            .Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.GuidanceWorkId)
            .ToList();
        _applyGuidanceTable(events, pageData, ref offset, headerStyle, tableStyle, minCount);
        events = stateUser
            .Events.Where(x =>
                x.EventType.WorkId.ToString() == SystemWorks.OrganizationalMethodicalWorkId
            )
            .ToList();
        _applyOrganizationalTable(events, pageData, ref offset, headerStyle, tableStyle, minCount);

        _applyPageData(sheet, pageData);
    }

    private void _applyGuidanceTable(
        ICollection<Event> events,
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle,
        int minCount
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "IV. РУКОВОДСТВО НАУЧНО-ИССЛЕДОВАТЕЛЬСКОЙ РАБОТОЙ СТУДЕНТОВ",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 4)
                ),
                new(
                    offset + 1,
                    0,
                    "Виды работ",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 0, 0)
                ),
                new(
                    offset + 1,
                    1,
                    "Ф.И.О. студента,  № группы",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 1, 1)
                ),
                new(
                    offset + 1,
                    2,
                    "Затраты времени, час",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 2, 3)
                ),
                new(
                    offset + 1,
                    4,
                    "Отметка зав.каф. о выполнении",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 4, 4)
                ),
                new(offset + 2, 2, "План", tableStyle),
                new(offset + 2, 3, "Факт.", tableStyle)
            }
        );

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventGuidance,
            _writeLessonGuidance,
            _writeCommentGuidance,
            _writeEmptyGuidance
        );

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 1)
                ),
                new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
                new(offset, 3, _getFactHours(events).ToString(), tableStyle),
                new(offset, 4, "", tableStyle)
            }
        );

        offset += 1;
    }

    private void _applyOrganizationalTable(
        ICollection<Event> events,
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle,
        int minCount
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "V. ОРГАНИЗАЦИОННО-МЕТОДИЧЕСКАЯ РАБОТА",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 4)
                ),
                new(
                    offset + 1,
                    0,
                    "Виды работ",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 0, 1)
                ),
                new(
                    offset + 1,
                    2,
                    "Затраты времени, час",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 2, 3)
                ),
                new(
                    offset + 1,
                    4,
                    "Отметка зав.каф. о выполнении",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 4, 4)
                ),
                new(offset + 2, 2, "План", tableStyle),
                new(offset + 2, 3, "Факт.", tableStyle)
            }
        );

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort,
            _writeEmptyShort
        );

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 1)
                ),
                new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
                new(offset, 3, _getFactHours(events).ToString(), tableStyle),
                new(offset, 4, "", tableStyle)
            }
        );

        offset += 1;
    }

    private void _addEducationalWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        var sheet = workbook.CreateSheet("7_ВР ДПО");

        sheet.SetColumnWidth(0, 64 * 256);
        sheet.SetColumnWidth(4, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var offset = 0;
        var pageData = new List<CellData>();

        var events = stateUser
            .Events.Where(x => x.EventType?.WorkId.ToString() == SystemWorks.EducationalWorkId)
            .ToList();
        _applyEducationalTable(events, pageData, ref offset, headerStyle, tableStyle, 14);
        events = stateUser
            .Events.Where(x => x.EventType?.WorkId.ToString() == SystemWorks.MedicalWorkId)
            .ToList();
        _applyMedicalTable(events, pageData, ref offset, headerStyle, tableStyle, 15);
        events = stateUser
            .Events.Where(x => x.EventType?.WorkId.ToString() == SystemWorks.ExtraWorkId)
            .ToList();
        _applyExtraTable(events, pageData, ref offset, headerStyle, tableStyle, 7);
        _applyRecommendationsTable(pageData, ref offset, headerStyle, tableStyle, 21);

        _applyPageData(sheet, pageData);
    }

    private void _applyEducationalTable(
        List<Event> events,
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle,
        int minCount
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "VI. ВОСПИТАТЕЛЬНАЯ РАБОТА",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 4)
                ),
                new(
                    offset + 1,
                    0,
                    "Виды работ",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 0, 1)
                ),
                new(
                    offset + 1,
                    2,
                    "Затраты времени, час",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 2, 3)
                ),
                new(
                    offset + 1,
                    4,
                    "Отметка зав.каф. о выполнении",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 4, 4)
                ),
                new(offset + 2, 2, "План", tableStyle),
                new(offset + 2, 3, "Факт.", tableStyle)
            }
        );

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort,
            _writeEmptyShort
        );

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 1)
                ),
                new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
                new(offset, 3, _getFactHours(events).ToString(), tableStyle),
                new(offset, 4, "", tableStyle)
            }
        );

        offset += 1;
    }

    private void _applyMedicalTable(
        List<Event> events,
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle,
        int minCount
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "VII. ОСУЩЕСТВЛЕНИЕ МЕДИЦИНСКОЙ ДЕЯТЕЛЬНОСТИ, НЕОБХОДИМОЙ ДЛЯ ПРАКТИЧЕСКОЙ ПОДГОТОВКИ ОБУЧАЮЩИХСЯ",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 4)
                )
            }
        );

        offset += 1;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort,
            _writeEmptyShort
        );

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 1)
                ),
                new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
                new(offset, 3, _getFactHours(events).ToString(), tableStyle),
                new(offset, 4, "", tableStyle)
            }
        );

        offset += 1;
    }

    private void _applyExtraTable(
        List<Event> events,
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle,
        int minCount
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "VIII. ДОПОЛНИТЕЛЬНОЕ ПРОФЕССИОНАЛЬНОЕ ОБРАЗОВАНИЕ ПО ПРОФИЛЮ ПЕДАГОГИЧЕСКОЙ ДЕЯТЕЛЬНОСТИ",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 4)
                ),
                new(
                    offset + 1,
                    0,
                    "Виды работ",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 0, 1)
                ),
                new(
                    offset + 1,
                    2,
                    "Затраты времени, час",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 2, 3)
                ),
                new(
                    offset + 1,
                    4,
                    "Отметка зав.каф. о выполнении",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 2, 4, 4)
                ),
                new(offset + 2, 2, "План", tableStyle),
                new(offset + 2, 3, "Факт.", tableStyle)
            }
        );

        offset += 3;

        _applyEventsData(
            pageData,
            events,
            ref offset,
            minCount,
            tableStyle,
            _writeEventShort,
            _writeLessonShort,
            _writeCommentShort,
            _writeEmptyShort
        );

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "Итого за год",
                    tableStyle,
                    new CellRangeAddress(offset, offset, 0, 1)
                ),
                new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
                new(offset, 3, _getFactHours(events).ToString(), tableStyle),
                new(offset, 4, "", tableStyle)
            }
        );

        offset += 1;
    }

    private void _applyRecommendationsTable(
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle,
        int minCount
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "IX. РЕКОМЕНДАЦИИ КАФЕДРЫ ПО ОТЧЕТУ ПРЕПОДАВАТЕЛЯ",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 4)
                ),
                new(
                    offset + 1,
                    0,
                    "",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 0, 4)
                )
            }
        );

        offset += 2;

        for (var i = 0; i < minCount; i++)
        {
            pageData.AddRange(
                new List<CellData>
                {
                    new(offset, 0, "", tableStyle, new CellRangeAddress(offset, offset, 0, 4))
                }
            );
            offset += 1;
        }
    }

    private void _addChangesWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        var sheet = workbook.CreateSheet("8_ИРИП ОПВП");

        sheet.SetColumnWidth(3, 24 * 256);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);
        var underlineStyle = _getUnderlineStyle(workbook);

        var offset = 0;
        var pageData = new List<CellData>();

        _addChangesTable(pageData, ref offset, headerStyle, tableStyle);
        _addCommentsTable(pageData, ref offset, headerStyle, tableStyle);
        _addSignsTable(pageData, ref offset, underlineStyle);

        _applyPageData(sheet, pageData);
    }

    private void _addChangesTable(
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "X. ИЗМЕНЕНИЕ ИНДИВИДУАЛЬНОГО ПЛАНА",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 3)
                ),
                new(
                    offset + 1,
                    0,
                    "Вводимые изменения",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 0, 1)
                ),
                new(offset + 1, 2, "Раздел плана", tableStyle),
                new(offset + 1, 3, "№ протокола заседания кафедры, дата", tableStyle)
            }
        );

        offset += 2;

        for (var i = 0; i < 30; i++)
        {
            pageData.AddRange(
                new List<CellData>
                {
                    new(offset, 0, "", tableStyle, new CellRangeAddress(offset, offset, 0, 1)),
                    new(offset, 2, "", tableStyle),
                    new(offset, 3, "", tableStyle)
                }
            );

            offset += 1;
        }
    }

    private void _addCommentsTable(
        List<CellData> pageData,
        ref int offset,
        ICellStyle headerStyle,
        ICellStyle tableStyle
    )
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    0,
                    "ОТМЕТКИ О ПРОВЕРКЕ ВЫПОЛНЕНИЯ ПЛАНА",
                    headerStyle,
                    new CellRangeAddress(offset, offset, 0, 3)
                ),
                new(offset + 1, 0, "Замечания", tableStyle),
                new(
                    offset + 1,
                    1,
                    "Фамилия, И.О., должность проверяющего",
                    tableStyle,
                    new CellRangeAddress(offset + 1, offset + 1, 1, 2)
                ),
                new(offset + 1, 3, "Подпись преподавателя", tableStyle)
            }
        );

        offset += 2;

        for (var i = 0; i < 12; i++)
        {
            pageData.AddRange(
                new List<CellData>
                {
                    new(offset, 0, "", tableStyle),
                    new(offset, 1, "", tableStyle, new CellRangeAddress(offset, offset, 1, 2)),
                    new(offset, 3, "", tableStyle)
                }
            );

            offset += 1;
        }
    }

    private void _addSignsTable(List<CellData> pageData, ref int offset, ICellStyle underlineStyle)
    {
        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    1,
                    "План обсужден на заседании кафедры",
                    merge: new CellRangeAddress(offset, offset, 1, 3)
                ),
                new(
                    offset + 1,
                    1,
                    "____ __________20__ г.       протокол №  ______",
                    merge: new CellRangeAddress(offset + 1, offset + 1, 1, 3)
                ),
                new(
                    offset + 2,
                    1,
                    "Заведующий кафедрой",
                    merge: new CellRangeAddress(offset + 2, offset + 2, 1, 2)
                ),
                new(offset + 2, 3, "", underlineStyle),
                new(
                    offset + 3,
                    1,
                    "Подпись преподавателя",
                    merge: new CellRangeAddress(offset + 3, offset + 3, 1, 2)
                ),
                new(offset + 3, 3, "", underlineStyle)
            }
        );

        offset += 4;

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    1,
                    "Выполнение плана в осеннем семестре проверено",
                    merge: new CellRangeAddress(offset, offset, 1, 3)
                ),
                new(
                    offset + 1,
                    1,
                    "и обсуждено на заседании кафедры",
                    merge: new CellRangeAddress(offset + 1, offset + 1, 1, 3)
                ),
                new(
                    offset + 2,
                    1,
                    "____ __________20__ г.       протокол №  ______",
                    merge: new CellRangeAddress(offset + 2, offset + 2, 1, 3)
                ),
                new(
                    offset + 3,
                    1,
                    "Заведующий кафедрой",
                    merge: new CellRangeAddress(offset + 3, offset + 3, 1, 2)
                ),
                new(offset + 3, 3, "", underlineStyle),
                new(
                    offset + 4,
                    1,
                    "Подпись преподавателя",
                    merge: new CellRangeAddress(offset + 4, offset + 4, 1, 2)
                ),
                new(offset + 4, 3, "", underlineStyle)
            }
        );

        offset += 5;

        pageData.AddRange(
            new List<CellData>
            {
                new(
                    offset,
                    1,
                    "Выполнение плана в весеннем семестре и за уч. год",
                    merge: new CellRangeAddress(offset, offset, 1, 3)
                ),
                new(
                    offset + 1,
                    1,
                    "проверено и обсуждено на заседании кафедры",
                    merge: new CellRangeAddress(offset + 1, offset + 1, 1, 3)
                ),
                new(
                    offset + 2,
                    1,
                    "____ __________20__ г.       протокол №  ______",
                    merge: new CellRangeAddress(offset + 2, offset + 2, 1, 3)
                ),
                new(
                    offset + 3,
                    1,
                    "Заведующий кафедрой",
                    merge: new CellRangeAddress(offset + 3, offset + 3, 1, 2)
                ),
                new(offset + 3, 3, "", underlineStyle),
                new(
                    offset + 4,
                    1,
                    "Подпись преподавателя",
                    merge: new CellRangeAddress(offset + 4, offset + 4, 1, 2)
                ),
                new(offset + 4, 3, "", underlineStyle)
            }
        );

        offset += 5;
    }

    private ICellStyle _getHeaderStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();
        style.Alignment = HorizontalAlignment.Center;
        style.WrapText = true;

        var newFont = workbook.CreateFont();
        newFont.IsBold = true;
        style.SetFont(newFont);

        return style;
    }

    private ICellStyle _getUnderlineStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();
        style.BorderBottom = BorderStyle.Thin;
        style.Alignment = HorizontalAlignment.Center;
        style.WrapText = true;
        return style;
    }

    private ICellStyle _getTableStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();
        style.Alignment = HorizontalAlignment.Center;
        style.WrapText = true;
        style.BorderBottom = BorderStyle.Thin;
        style.BorderTop = BorderStyle.Thin;
        style.BorderLeft = BorderStyle.Thin;
        style.BorderRight = BorderStyle.Thin;
        return style;
    }

    private int _getPlanHours(ICollection<Event> events)
    {
        return events.Select(x => x.Lessons.Select(l => l.PlanDate).Select(l => l ?? 0).Sum()).Sum()
               + events
                   .Select(x => x.Comments.Select(c => c.PlanDate).Select(c => c ?? 0).Sum())
                   .Sum();
    }

    private int _getFactHours(ICollection<Event> events)
    {
        return events.Select(x => x.Lessons.Select(l => l.FactDate).Select(l => l ?? 0).Sum()).Sum()
               + events
                   .Select(x => x.Comments.Select(c => c.FactDate).Select(c => c ?? 0).Sum())
                   .Sum();
    }

    private void _applyPageData(ISheet sheet, ICollection<CellData> pageData)
    {
        foreach (var data in pageData)
        {
            var row = sheet.GetRowSafe(data.Row);
            var cell = row.GetCellSafe(data.Col);

            cell.SetCellValue(data.Content);

            if (data.Merge != null)
            {
                sheet.AddMergedRegion(data.Merge);

                for (var i = data.Merge.FirstRow; i <= data.Merge.LastRow; i++)
                {
                    var tmpRow = sheet.GetRowSafe(i);
                    for (var j = data.Merge.FirstColumn; j <= data.Merge.LastColumn; j++)
                    {
                        var tmpCol = tmpRow.GetCellSafe(j);
                        tmpCol.CellStyle = data.Style;
                    }
                }
            }

            if (data.Style != null) cell.CellStyle = data.Style;
        }
    }

    private void _applyEventsData(
        List<CellData> pageData,
        ICollection<Event> events,
        ref int offset,
        int minCount,
        ICellStyle style,
        WriteEvent writeEvent,
        WriteLesson writeLesson,
        WriteComment writeComment,
        WriteEmpty writeEmpty
    )
    {
        var startOffset = offset;

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

        var endOffset = offset;

        for (var i = 0; i < minCount - (endOffset - startOffset); i++)
        {
            pageData.AddRange(writeEmpty(offset, style));
            offset += 1;
        }
    }

    private class CellData
    {
        public readonly int Col;
        public readonly string Content;
        public readonly CellRangeAddress? Merge;

        public readonly int Row;
        public readonly ICellStyle? Style;

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
    }

    /* Delegates for different types of pages */
    private delegate List<CellData> WriteEvent(Event ev, int offset, ICellStyle style);

    private delegate List<CellData> WriteLesson(Lesson lesson, int offset, ICellStyle style);

    private delegate List<CellData> WriteComment(Comment comment, int offset, ICellStyle style);

    private delegate List<CellData> WriteEmpty(int offset, ICellStyle style);
}