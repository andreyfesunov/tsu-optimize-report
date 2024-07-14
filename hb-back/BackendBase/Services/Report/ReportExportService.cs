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

    private void _addTitlePage(IWorkbook workbook, User user, StateUser stateUser, ICollection<Record> records)
    {
        ISheet sheet = workbook.CreateSheet("1_Тит");

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        centeredStyle.WrapText = true;

        var underlineStyle = workbook.CreateCellStyle();
        underlineStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        underlineStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

        var tableStyle = workbook.CreateCellStyle();
        tableStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        tableStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        tableStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        tableStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        tableStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        tableStyle.WrapText = true;

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
        ISheet sheet = workbook.CreateSheet("4-УЧ_МЕТ");

        var headerStyle = workbook.CreateCellStyle();
        headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        centeredStyle.WrapText = true;
        centeredStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        centeredStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        centeredStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        centeredStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

        var pageData = new List<CellData>{
            new(0, 0, "II.  УЧЕБНО-МЕТОДИЧЕСКАЯ РАБОТА", headerStyle, new CellRangeAddress(0, 0, 0, 5)),
            new(2, 0, "Наименование работы", centeredStyle, new CellRangeAddress(2, 3, 0, 0)),
            new(2, 1, "Срок выполнения", centeredStyle, new CellRangeAddress(2, 2, 1, 2)),
            new(2, 3, "Затраты времени, час", centeredStyle, new CellRangeAddress(2, 2, 3, 4)),
            new(2, 5, "Отметка зав.каф.о выполнении", centeredStyle, new CellRangeAddress(2, 3, 5, 5)),
            new(3, 1, "Начало", centeredStyle),
            new(3, 2, "Конец", centeredStyle),
            new(3, 3, "План", centeredStyle),
            new(3, 4, "Факт.", centeredStyle),
        };

        var offset = 4;

        var events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.AcademicMethodicalWorkId).ToList();

        _applyEventsData(pageData, events, ref offset, centeredStyle);

        var planHours = events.Select(x => x.Lessons.Select(l => l.PlanDate).Select(l => l ?? 0).Sum()).Sum() +
            events.Select(x => x.Comments.Select(c => c.PlanDate).Select(c => c ?? 0).Sum()).Sum();
        var factHours = events.Select(x => x.Lessons.Select(l => l.FactDate).Select(l => l ?? 0).Sum()).Sum() +
            events.Select(x => x.Comments.Select(c => c.FactDate).Select(c => c ?? 0).Sum()).Sum();

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", centeredStyle, new CellRangeAddress(offset, offset, 0, 2)),
            new(offset, 3, planHours.ToString(), centeredStyle),
            new(offset, 4, factHours.ToString(), centeredStyle),
            new(offset, 5, "", centeredStyle)
        });

        _applyPageData(sheet, pageData);
    }

    private void _addScienceWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        ISheet sheet = workbook.CreateSheet("5-НДиНМД");

        var headerStyle = workbook.CreateCellStyle();
        headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        centeredStyle.WrapText = true;
        centeredStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        centeredStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        centeredStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        centeredStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

        var pageData = new List<CellData>{
            new(0, 0, "III.  НАУЧНАЯ И НАУЧНО-МЕТОДИЧЕСКАЯ ДЕЯТЕЛЬНОСТЬ", headerStyle, new CellRangeAddress(0, 0, 0, 5)),
            new(2, 0, "Наименование работы", centeredStyle, new CellRangeAddress(2, 3, 0, 0)),
            new(2, 1, "Срок выполнения", centeredStyle, new CellRangeAddress(2, 2, 1, 2)),
            new(2, 3, "Затраты времени, час", centeredStyle, new CellRangeAddress(2, 2, 3, 4)),
            new(2, 5, "Отметка зав.каф.о выполнении", centeredStyle, new CellRangeAddress(2, 3, 5, 5)),
            new(3, 1, "Начало", centeredStyle),
            new(3, 2, "Конец", centeredStyle),
            new(3, 3, "План", centeredStyle),
            new(3, 4, "Факт.", centeredStyle),
        };

        var offset = 4;

        var events = stateUser.Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.ScienceWorkId).ToList();

        _applyEventsData(pageData, events, ref offset, centeredStyle);

        var planHours = events.Select(x => x.Lessons.Select(l => l.PlanDate).Select(l => l ?? 0).Sum()).Sum() +
            events.Select(x => x.Comments.Select(c => c.PlanDate).Select(c => c ?? 0).Sum()).Sum();
        var factHours = events.Select(x => x.Lessons.Select(l => l.FactDate).Select(l => l ?? 0).Sum()).Sum() +
            events.Select(x => x.Comments.Select(c => c.FactDate).Select(c => c ?? 0).Sum()).Sum();

        pageData.AddRange(new List<CellData>{
            new(offset, 0, "Итого за год", centeredStyle, new CellRangeAddress(offset, offset, 0, 2)),
            new(offset, 3, planHours.ToString(), centeredStyle),
            new(offset, 4, factHours.ToString(), centeredStyle),
            new(offset, 5, "", centeredStyle)
        });

        _applyPageData(sheet, pageData);
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

    private void _applyEventsData(List<CellData> pageData, ICollection<Event> events, ref int offset, ICellStyle centeredStyle)
    {
        foreach (var ev in events)
        {
            pageData.AddRange(new List<CellData>{
                new(offset, 0, ev.EventType.Name, centeredStyle),
                new(offset, 1, ev.StartedAt.ToString("d"), centeredStyle),
                new(offset, 2, ev.EndedAt.ToString("d"), centeredStyle),
                new(offset, 3, "", centeredStyle, new CellRangeAddress(offset, offset, 3, 5))
            });

            offset += 1;

            foreach (var comment in ev.Comments)
            {
                pageData.AddRange(new List<CellData>
                {
                    new(offset, 0, comment.Content, centeredStyle),
                    new(offset, 1, "", centeredStyle),
                    new(offset, 2, "", centeredStyle),
                    new(offset, 3, comment.PlanDate.ToString() ?? "", centeredStyle),
                    new(offset, 4, comment.FactDate.ToString() ?? "", centeredStyle),
                    new(offset, 5, "", centeredStyle),
                });

                offset += 1;
            }
            foreach (var lesson in ev.Lessons)
            {
                pageData.AddRange(new List<CellData>
                {
                    new(offset, 0, lesson.LessonType.Name, centeredStyle),
                    new(offset, 1, "", centeredStyle),
                    new(offset, 2, "", centeredStyle),
                    new(offset, 3, lesson.PlanDate.ToString() ?? "", centeredStyle),
                    new(offset, 4, lesson.FactDate.ToString() ?? "", centeredStyle),
                    new(offset, 5, "", centeredStyle),
                });

                offset += 1;
            }
        }
    }
}
