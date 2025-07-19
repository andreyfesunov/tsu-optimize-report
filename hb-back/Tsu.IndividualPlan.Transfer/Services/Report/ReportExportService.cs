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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using UserInfo = Tsu.IndividualPlan.Domain.Models.Project.UserInfo;
using Comment = Tsu.IndividualPlan.Domain.Models.Business.Comment;

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
        var workbook = _getReportPlaceholder(stateUser);
        _addTitlePage(workbook, user, stateUser, records);
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
        var sheet = workbook.GetSheetAt(0);

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = HorizontalAlignment.Center;
        centeredStyle.WrapText = true;

        var underlineStyle = _getUnderlineStyle(workbook);
        var tableStyle = _getTableStyle(workbook);
        var nameStyle = _getNameStyle(workbook);

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
            new(5, 0, $"Кафедра {stateUser.State.Department.Name.ToString()}", nameStyle),
            new(21, 7, stateUser.State.StartDate.Year.ToString(), centeredStyle),
            new(21, 14, stateUser.State.EndDate.Year.ToString(), centeredStyle),
            new(
                23,
                0,
                stateUser.State.Job.Name
                + " "
                + user.Lastname
                + " "
                + user.Firstname[0]
                + ". "
                + "("
                + stateUser.Rate
                + ")",
                nameStyle
            ),
            new(31, 9, firstHalfHours.ToString(), centeredStyle),
            new(32, 9, secondHalfHours.ToString(), centeredStyle),
            new(34, 16, (firstHalfHours + secondHalfHours).ToString(), underlineStyle),
            new(37, 0, $"Тула {stateUser.State.StartDate.Year.ToString()} г.")
        };

        _applyPageData(sheet, pageData);
    }

    private void _finishTitlePage(
        IWorkbook workbook,
        User user,
        StateUser stateUser,
        ICollection<Record> records
    )
    {
        var sheet = workbook.GetSheetAt(0);

        var centeredStyle = workbook.CreateCellStyle();
        centeredStyle.Alignment = HorizontalAlignment.Center;
        centeredStyle.WrapText = true;

        var underlineStyle = _getUnderlineStyle(workbook);

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

        var secondHalfHoursCell = sheet.FindCell("2 половина рабочего дня");
        var sumHalfHoursCell = sheet.FindCell("Всего:");

        var pageData = new List<CellData>
        {
            new(
                secondHalfHoursCell.Address.Row,
                secondHalfHoursCell.IsMergedCell
                ? sheet.GetMergedRegionContainingCell(secondHalfHoursCell.Address).LastColumn + 1
                : secondHalfHoursCell.Address.Column + 1,
                secondHalfHours.ToString(), centeredStyle),
            new(
                sumHalfHoursCell.Address.Row,
                sumHalfHoursCell.IsMergedCell
                ? sheet.GetMergedRegionContainingCell(sumHalfHoursCell.Address).LastColumn + 1
                : sumHalfHoursCell.Address.Column + 1,
                (firstHalfHours + secondHalfHours).ToString(), underlineStyle),
        };

        _applyPageData(sheet, pageData);
    }

    private void _copyLayout(ISheet sourceSheet, ISheet targetSheet)
    {
        // 1. Копируем ширину колонок
        for (int col = 0; col <= sourceSheet.GetRow(0).LastCellNum; col++)
        {
            double width = sourceSheet.GetColumnWidth(col);
            targetSheet.SetColumnWidth(col, width);

            // Если колонка скрыта
            targetSheet.SetColumnHidden(col, sourceSheet.IsColumnHidden(col));
        }

        // 2. Копируем высоту строк
        for (int rowIdx = sourceSheet.FirstRowNum; rowIdx <= sourceSheet.LastRowNum; rowIdx++)
        {
            IRow sourceRow = sourceSheet.GetRow(rowIdx);
            if (sourceRow == null) continue;

            IRow targetRow = targetSheet.GetRow(rowIdx) ?? targetSheet.CreateRow(rowIdx);

            // Копируем высоту
            targetRow.Height = sourceRow.Height;

            // Если строка скрыта
            targetRow.ZeroHeight = sourceRow.ZeroHeight;
        }

        // 3. Копируем объединенные ячейки
        for (int i = 0; i < sourceSheet.NumMergedRegions; i++)
        {
            CellRangeAddress merged = sourceSheet.GetMergedRegion(i);
            targetSheet.AddMergedRegion(merged);
        }
    }

    private IWorkbook _getReportPlaceholder(StateUser stateUser)
    {
        var firstHalfFile = stateUser.Files.ToList()[0];
        string firstHalfFileExtension = Path.GetExtension(firstHalfFile.Path).ToLower();
        var path = Path.Combine(_root, firstHalfFile.Path);

        using (var wbSource = new XLWorkbook(Path.Combine(_root, "SecondPartExample.xlsx")))
        using (var wbTarget = new XLWorkbook(path))
        {
            wbTarget.Worksheets.First().Delete();
            wbSource.Worksheet(1).CopyTo(wbTarget, wbSource.Worksheets.First().Name, 1);

            for (int i = 3; i < 8; i++)
                wbSource.Worksheet(i + 1).CopyTo(wbTarget, wbSource.Worksheets.ElementAt(i).Name);

            using (var memoryStream = new MemoryStream())
            {
                wbTarget.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var result = (IWorkbook)new XSSFWorkbook(memoryStream);
                result.GetSheetAt(1).PrintSetup.Landscape = true;
                result.GetSheetAt(2).PrintSetup.Landscape = true;
                return result;
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
        var sheet = workbook.GetSheetAt(3);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>();

        var offset = 5;

        var events = stateUser.Events.Where(x =>
                x.EventType.WorkId.ToString() == SystemWorks.AcademicMethodicalWorkId
            )
            .ToList();

        _applySemestrSensitiveEventsData(
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
        var sheet = workbook.GetSheetAt(4);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>();

        var offset = 5;

        var events = stateUser
            .Events.Where(x => x.EventType.WorkId.ToString() == SystemWorks.ScienceWorkId)
            .ToList();

        _applySemestrSensitiveEventsData(
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
        var sheet = workbook.GetSheetAt(5);

        var headerStyle = _getHeaderStyle(workbook);
        var tableStyle = _getTableStyle(workbook);

        var pageData = new List<CellData>();
        var offset = 0;

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
                new(offset, 2, _getPlanHours(events).ToString(), tableStyle),
                new(offset, 3, _getFactHours(events).ToString(), tableStyle),
                new(offset, 4, "", tableStyle)
            }
        );

        offset += 1;
    }

    private void _addEducationalWorkPage(IWorkbook workbook, User user, StateUser stateUser)
    {
        var sheet = workbook.GetSheetAt(6);

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
        var sheet = workbook.GetSheetAt(7);

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

    private ICellStyle _getNameStyle(IWorkbook workbook)
    {
        var style = workbook.CreateCellStyle();

        // Настройки границ и выравнивания
        style.Alignment = HorizontalAlignment.Center;
        style.VerticalAlignment = VerticalAlignment.Center; // Добавляем вертикальное выравнивание для полноты
        style.WrapText = true;

        // Создаём и настраиваем шрифт
        IFont font = workbook.CreateFont();
        font.FontName = "Tahoma";    // Шрифт
        font.FontHeightInPoints = 12; // Размер 12pt
        font.IsBold = true;          // Жирный
        font.IsItalic = true;        // Курсив

        // Применяем шрифт к стилю
        style.SetFont(font);

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
                //sheet.AddMergedRegion(data.Merge);

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

    private void _applySemestrSensitiveEventsData(
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
        _applySemestrEventsData(pageData, events.Where(x => x.SemestrId == 1).ToList(), ref offset, minCount, style, writeEvent, writeLesson, writeComment, writeEmpty);
        offset = 33;
        _applySemestrEventsData(pageData, events.Where(x => x.SemestrId == 2).ToList(), ref offset, minCount, style, writeEvent, writeLesson, writeComment, writeEmpty);
    }

    private void _applySemestrEventsData(
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
        _applyEventsData(pageData, events, ref offset, 26, style, writeEvent, writeLesson, writeComment, writeEmpty);

        var planHours = _getPlanHours(events);
        var factHours = _getFactHours(events);

        pageData.AddRange(
            new List<CellData>
            {
                new(offset, 3, planHours.ToString(), style),
                new(offset, 4, factHours.ToString(), style),
                new(offset, 5, "", style)
            }
        );
        offset += 1;
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