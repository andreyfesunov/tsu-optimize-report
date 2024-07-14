using BackendBase.Dto;
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

        return workbook;
    }

    class StaticTitleData {
        public StaticTitleData(
            int row,
            int col,
            string content,
            ICellStyle? style = null,
            CellRangeAddress? merge = null 
        ) {
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

    private void _addTitlePage(IWorkbook workbook, User user, StateUser stateUser, ICollection<Record> records) {
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

        var staticData = new List<StaticTitleData> {
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

        foreach (StaticTitleData data in staticData) {
            var row = sheet.GetRow(data.Row) ?? sheet.CreateRow(data.Row);
            var cell = row.GetCell(data.Col) ?? row.CreateCell(data.Col);

            cell.SetCellValue(data.Content);

            if (data.Style != null) {
                cell.CellStyle = data.Style;
            }
            if (data.Merge != null) {
                sheet.AddMergedRegion(data.Merge);
            }
        }
    }
}
