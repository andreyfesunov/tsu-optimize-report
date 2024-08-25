using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Factories;

public static class ActivityFactory
{
    public static List<Activity> Make()
    {
        var dict = _getDefaultFields();
        var res = new List<Activity>();

        foreach (var (col, name) in dict) res.Add(_makeActivity(name, col));

        return res;
    }

    private static Activity _makeActivity(string name, int col)
    {
        return new Activity(name, col);
    }

    private static Dictionary<int, string> _getDefaultFields()
    {
        return new Dictionary<int, string>
        {
            { 15, "Лекции" },
            { 18, "Практические (семинарские) занятия" },
            { 20, "Лабораторные работы, клинические практические занятия" },
            { 23, "Индивидуальные занятия" },
            { 24, "Предэкзаменационные консультации" },
            { 26, "Руководство КП(Р)" },
            { 27, "Прием КР,  РГР, ГР, ИБ, письменных заданий" },
            { 29, "Руководство практикой, НИР, НИД" },
            { 30, "Зачет" },
            { 33, "Защита КП(Р)" },
            { 34, "Защита отчета по практике" },
            { 36, "Экзамен" },
            { 37, "Государственный экзамен" },
            { 38, "Руководство ВКР" },
            { 39, "Консультации по части  ДП" },
            { 40, "ГЭК по защитам ВКР" },
            { 41, "Участие в работе АК" }
        };
    }
}