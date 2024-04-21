using BackendBase.Models;

namespace BackendBase.Factories;

public static class ActivityFactory
{
    public static IEnumerable<Activity> Make()
    {
        var dict = _getDefaultFields();
        var res = new List<Activity>();

        foreach (var (col, name) in dict)
        {
            res.Add(_makeActivity(name, col));
        }

        return res;
    }

    private static Activity _makeActivity(string name, string col)
    {
        return new Activity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Column = col
        };
    }

    private static Dictionary<string, string> _getDefaultFields()
    {
        return new Dictionary<string, string>
        {
            { "P", "Лекции" },
            { "S", "Практические (семинарские) занятия" },
            { "U", "Лабораторные работы, клинические практические занятия" },
            { "X", "Индивидуальные занятия" },
            { "Y", "Предэкзаменационные консультации" },
            { "AA", "Руководство КП(Р)" },
            { "AB", "Прием КР,  РГР, ГР, ИБ, письменных заданий" },
            { "AD", "Руководство практикой, НИР, НИД" },
            { "AE", "Зачет" },
            { "AH", "Защита КП(Р)" },
            { "AI", "Защита отчета по практике" },
            { "AK", "Экзамен" },
            { "AL", "Государственный экзамен" },
            { "AM", "Руководство ВКР" },
            { "AN", "Консультации по части  ДП" },
            { "AO", "ГЭК по защитам ВКР" },
            { "AP", "Участие в работе АК" }
        };
    }
}