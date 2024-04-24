using BackendBase.Models;

namespace BackendBase.Factories;

public static class ActivityFactory
{
    public static List<Activity> Make()
    {
        var dict = _getDefaultFields();
        var res = new List<Activity>();

        foreach (var (col, name) in dict)
        {
            res.Add(_makeActivity(name, col));
        }

        return res;
    }

    private static Activity _makeActivity(string name, int col)
    {
        return new Activity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Column = col
        };
    }

    private static Dictionary<int, string> _getDefaultFields()
    {
        return new Dictionary<int, string>
        {
            { 16, "Лекции" },
            { 19, "Практические (семинарские) занятия" },
            { 21, "Лабораторные работы, клинические практические занятия" },
            { 24, "Индивидуальные занятия" },
            { 25, "Предэкзаменационные консультации" },
            { 27, "Руководство КП(Р)" },
            { 28, "Прием КР,  РГР, ГР, ИБ, письменных заданий" },
            { 30, "Руководство практикой, НИР, НИД" },
            { 31, "Зачет" },
            { 34, "Защита КП(Р)" },
            { 35, "Защита отчета по практике" },
            { 37, "Экзамен" },
            { 38, "Государственный экзамен" },
            { 39, "Руководство ВКР" },
            { 40, "Консультации по части  ДП" },
            { 41, "ГЭК по защитам ВКР" },
            { 42, "Участие в работе АК" }
        };
    }
}