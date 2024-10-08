﻿using Tsu.IndividualPlan.Domain.Extensions;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Seeders;

public static class WorkAndEventTypeFactory
{
    public static Tuple<List<Work>, List<EventType>> Make()
    {
        var ets = new List<EventType>();
        var ws = new List<Work>();

        var w1 = new Work(
            Id: Guid.Parse(SystemWorks.AcademicMethodicalWorkId),
            Name: "Учебно-методическая работа",
            Order: 1
        );

        var ets1 = new List<EventType>
        {
            new(
                WorkId: w1.Id,
                Name: "Актуализация экзаменационных билетов промежуточной аттестации",
                Description: "До 2-х часов на один комплект билетов"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация экзаменационных билетов промежуточной аттестации",
                Description: "До 2-х часов на один комплект билетов"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация основных образовательных программ высшего образования (ООП ВО)",
                Description: "До 150 часов на группу разработчиков"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация тестовых заданий государственной итоговой (итоговой) аттестации",
                Description: "До 1 час на тестовое задание"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация тестовых заданий промежуточной аттестации",
                Description: "До 0,5 часов на тестовое задание"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация экзаменационных билетов государственной итоговой (итоговой) аттестации",
                Description: "До 0,5 часа на билет"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация/переработка действующей рабочей программы учебной дисциплины",
                Description: "До 5 часов на одну программу"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Актуализация/переработка для повторного издания методических указаний по самостоятельной работе студентов",
                Description: "До 20 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Актуализация/переработка для повторного издания методических указаний по курсовому проектированию",
                Description: "До 20 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация/переработка для повторного издания методических указаний по лабораторным работам",
                Description: "До 20 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Актуализация/переработка для повторного издания методических указаний по подготовке и защите ВКР, учитывающих особенности испытаний по конкурентной ООП",
                Description: "До 40 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Актуализация/переработка для повторного издания методических указаний по практическим занятиям",
                Description: "До 20 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Внедрение в учебный процесс инновационных форм учебных занятий, развивающих у обучающихся навыки командной работы, межличностной коммуникации, принятия решений, лидерские качества",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Заполнение модуля «Электронный дневник» АИС УУП, внесение информации о посещаемости и успеваемости",
                Description: "0,2 – 0,5 час на группу на одно занятие"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Заполнение модуля «Электронный дневник» АИС УУП,привязка видов работ к календарному графику проведения занятий по дисциплине",
                Description: "0,5 - 1 час на один вид заня-тий (лекционные, практиче-ские, лабораторные)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Изготовление препаратов (анатомических, микробиологических, патанатомических и др.)",
                Description: "До 100 часов в семестр (по согласованию с заведующим кафедрой)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Корректировка учебного плана ООП",
                Description: "20-50 часов на один план"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Корректировка в ЭИОС ТулГУ образовательного контента по реализуемым дисциплинам (модулям), практикам",
                Description:
                "До 50 часов за корректировку образовательного контента по всем видам занятий, текущему контролю успеваемости и промежуточной аттестации по 1 дисциплине (модулю) или практике"
            ),
            new(
                WorkId: w1.Id,
                Name: "Научное редактирование учебников, учебных пособий",
                Description: "До 5 часов на один печатный лист"
            ),
            new(WorkId: w1.Id, Name: "Нормоконтроль КП и ВКР", Description: "0,2 часа на 1 лист"),
            new(
                WorkId: w1.Id,
                Name: "Подбор, подготовка и размножение текстов на иностранном языке для выдачи студентам",
                Description: "До 0,1 часа на один текст"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка видеофильмов для аудиторных занятий",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка видеофильмов для занятий с применением дистанционной образовательной технологии",
                Description: "От 50 до 300 час от 50 до 300 час за 80 кБ электронного файла"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка видеолекции для занятий с применением дистанционной образовательной технологии",
                Description: "От 50 до 300 час за 80 кБ электронного файла"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка документов к государственной аккредитации ООП ВО",
                Description: "150 часов"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Подготовка и размещение в электронной информационно-образовательной среде университета (ЭИОС ТулГУ) образовательного контента по реализуемым дисциплинам (модулям), практикам",
                Description:
                "До 100 часов за подготовку и размещение образовательного контента по всем видам занятий, текущему контролю успеваемости и промежуточной аттестации по 1 дисциплине (модулю) или практике, материал по которой, размещается впервые"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка индивидуальных заданий на производственную и преддипломную практики",
                Description: "0,25 ч. на 1 задание"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка к лекционным занятиям",
                Description:
                "2-5 часов на 1 час занятий по впервые читаемой дисциплине; 0,5-1 час на 1 час занятий по повторно читаемой дисциплине (дублирование занятий в потоках и группах не учитывается)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка к практическим занятиям по дисциплине",
                Description:
                "2-5 часов на 1 час занятий по впервые читаемой дисциплине; 0,5-1 часов на 1 час занятий по повторно читаемой дис-циплине (дублирование занятий в потоках и группах не учитывается)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка материалов для лицензирования направления (специальности)",
                Description: "150 часов"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Подготовка материалов для проведения деловых, ситуационных и ролевых игр, круглых столов, групповых дискуссий, тренингов, и т.п.",
                Description:
                "6 часа на подготовку к игре проводимой впервые, 3 часа на подготовку к игре при повторном проведении"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка презентаций для аудиторных занятий",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка презентаций для занятий с применением дистанционной образовательной технологии",
                Description: "От 50 до 300 час от 50 до 300 час за 80 кБ электронного файла"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка презентаций лекций (для раздачи)",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка раздаточного материала для практических занятий",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка слайдов для аудиторных занятий",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Подготовка слайдов для занятий с применением дистанционной образовательной технологии",
                Description: "От 50 до 300 час от 50 до 300 час за 80 кБ электронного файла"
            ),
            new(
                WorkId: w1.Id,
                Name: "Посещение занятий других преподавателей по утвержденному кафедральному графику взаимопосещений",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Постановка новой лабораторной работы",
                Description: "30-60 часов"
            ),
            new(
                WorkId: w1.Id,
                Name: "Модернизация действующих лабораторных работ",
                Description: "10-30 часов"
            ),
            new(
                WorkId: w1.Id,
                Name: "Проведение олимпиады со студентами или абитуриентами",
                Description: "4 часа на 1 тур"
            ),
            new(
                WorkId: w1.Id,
                Name: "Проверка олимпиадных работ студентов и абитуриентов",
                Description: "0,3 часа на 1 работу"
            ),
            new(
                WorkId: w1.Id,
                Name: "Проектирование фонда оценочных средств сформированности компетенций",
                Description: "От 50 до 300 часов по решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Работа в научно-методических советах Минобрнауки, в УМО, в редколлегиях научных журналов",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w1.Id,
                Name: "Работа экспертом Росаккредагентства, Рособрнадзора",
                Description: "До 400 часов в год (по решению УМУ)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка основных образовательных программ высшего образования (ООП ВО)",
                Description: "До 350 часов на группу разработчиков"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка виртуальной лабораторной работы",
                Description: "От 50 до 300 час за 80 кБ электронного файла"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Разработка комплекта тестовых заданий (в том числе и в электронном виде) государственной итоговой (итоговой) аттестации",
                Description: "До 2 часов на тестовое задание"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Разработка комплекта тестовых заданий (в том числе и в электронном виде) промежуточной аттестации",
                Description: "До 1,5 часов на тестовое задание"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Разработка методических указаний для преподавателя по проведению индивидуальных занятий по программам магистратуры",
                Description: "20-60 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка методических указаний по выполнению ККР, ГР, РГР, ТР, ИБ",
                Description: "20-60 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка методических указаний по выполнению курсовых проектов (работ) КП (КР)",
                Description: "20-60 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка методических указаний по лабораторным работам",
                Description: "20-60 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Разработка методических указаний по подготовке и защите ВКР, учитывающих особенности испытаний по конкурентной ООП",
                Description: "100 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка методических указаний по практическим (семинарским) занятиям",
                Description: "20-60 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка методических указаний по СРС",
                Description: "20-60 часов на один печатный лист (внутривузовское издание)"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Разработка программ для интерактивных лекций, деловых и ролевых игр, ситуационных и имитационных моделей, систем автоматизированного проектирования",
                Description: "От 50 до 300 час за 1 п.л. или за 80 кБ электронного файла"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка рабочей программы учебной дисциплины",
                Description: "20-60 часов на один печатный лист в электронном виде"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка учебного плана ООП",
                Description: "До 150 часов на один план"
            ),
            new(
                WorkId: w1.Id,
                Name: "Разработка электронного учебника",
                Description:
                "От 100 ч. до 350 ч. в зависимости от объема ( при наличии соавторов часы делятся в соответствии с долей участия)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Редактирование конспекта лекций",
                Description: "До 5 часов на один печатный лист"
            ),
            new(
                WorkId: w1.Id,
                Name: "Рецензирование аттестационной, дипломной, проектной работы по ДОП",
                Description: "3 часа на одну ВКР (одному рецензенту направляется до 8 АР)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Рецензирование бакалаврской работы, дипломного проекта, дипломной работы",
                Description: "2 часа на одну работу (одному рецензенту направляется до 8 работ)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Рецензирование ВКР аспиранта",
                Description: "3 часа на одну ВКР (одному рецензенту направляется до 6 ВКР)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Рецензирование КП(Р)",
                Description: "0,25 часа на один проект"
            ),
            new(
                WorkId: w1.Id,
                Name: "Рецензирование магистерской диссертации",
                Description: "3 часа на одну диссертацию (одному рецензенту направляется до 6 диссертаций)"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление заданий для ККР, КР",
                Description: "До 0,25 часа на один вариант"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление заданий для РГР, ГР И ТР",
                Description: "До 0,1 часа на один вариант"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление заданий для олимпиад со студентами или абитуриентами",
                Description: "2 часа на 1 вариант"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление заданий на курсовое проектирование по общетехническим дисциплинам",
                Description: "До 0,5 часа на одно задание"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление заданий на курсовое проектирование по специальным дисциплинам",
                Description: "До 0,33 часа на одно задание"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление экзаменационных билетов государственной итоговой (итоговой) аттестации",
                Description: "До 1 часа на билет"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление экзаменационных билетов промежуточной аттестации без задач",
                Description: "До 0,3 часа на один билет"
            ),
            new(
                WorkId: w1.Id,
                Name:
                "Составление экзаменационных билетов промежуточной аттестации с заданиями для контроля практических умений",
                Description: "До 1 часа на билет"
            ),
            new(
                WorkId: w1.Id,
                Name: "Составление экзаменационных билетов промежуточной аттестации с ситуационными задачами",
                Description: "До 2 часа на один билет"
            )
        };

        var w2 = new Work(
            Id: Guid.Parse(SystemWorks.ScienceWorkId),
            Name: "Научная и научно-методическая деятельность",
            Order: 2
        );

        var ets2 = new List<EventType>
        {
            new(
                WorkId: w2.Id,
                Name: "Выступление с докладом на конференции, симпозиуме, съезде",
                Description: "До 20 часов"
            ),
            new(
                WorkId: w2.Id,
                Name: "Выступление с докладом на научном семинаре кафедры",
                Description: "До 10 часов"
            ),
            new(
                WorkId: w2.Id,
                Name: "Выступление с докладом на Ученом совете университета,  ученом совете института",
                Description: "До 15 часов"
            ),
            new(
                WorkId: w2.Id,
                Name: "Написание и подготовка для издательства монографии или учебника (без гонорара)",
                Description: "150-200 час на 1 печатный лист"
            ),
            new(
                WorkId: w2.Id,
                Name: "Написание и подготовка для издательства статьи научного или методического характера",
                Description: "50-70 час на 1 печатный лист"
            ),
            new(
                WorkId: w2.Id,
                Name: "Написание и подготовка для издательства учебного пособия без гонорара",
                Description: "До 100  часов на 1 печатный лист"
            ),
            new(
                WorkId: w2.Id,
                Name: "Научно-техническое консультирование работников предприятий, организаций",
                Description: "Устанавливается заведующим кафедрой по согласованию с УМУ"
            ),
            new(
                WorkId: w2.Id,
                Name: "Осуществление научной деятельности по госбюджетной теме",
                Description: "100-300 часов в год при условии, что преподаватель за нее не получает другой оплаты"
            ),
            new(
                WorkId: w2.Id,
                Name: "Ответственность за научную деятельность на кафедре при числе преподавателей до 10 человек",
                Description: "30 часов"
            ),
            new(
                WorkId: w2.Id,
                Name:
                "Ответственность за научную деятельность на кафедре при числе преподавателей св. 10 до 20 человек",
                Description: "60 часов"
            ),
            new(
                WorkId: w2.Id,
                Name: "Ответственность за научную деятельность на кафедре при числе преподавателей св. 20 человек",
                Description: "90 часов"
            ),
            new(
                WorkId: w2.Id,
                Name: "Подготовка доклада на международные, российские конференции",
                Description: "До 40 часов на один доклад"
            ),
            new(
                WorkId: w2.Id,
                Name: "Подготовка доклада на методический семинар кафедры",
                Description: "До 5 часов на один доклад"
            ),
            new(
                WorkId: w2.Id,
                Name: "Подготовка доклада на научно-методическую конференцию института",
                Description: "До 20 часов на один доклад"
            ),
            new(
                WorkId: w2.Id,
                Name:
                "Подготовка доклада на региональную, всероссийскую, международную, научно-методическую конференцию",
                Description: "До 30 часов на один доклад"
            ),
            new(
                WorkId: w2.Id,
                Name: "Подготовка документации к проведению сертификационного (инспекционного) аудита системы качества",
                Description: "Устанавливается заведующим кафедрой по согласованию с директором института"
            ),
            new(
                WorkId: w2.Id,
                Name: "Подготовка экспоната на выставку",
                Description: "40-60 часов на один экспонат"
            ),
            new(
                WorkId: w2.Id,
                Name: "Работа в методической комиссии института",
                Description: "Устанавливается заведующим кафедрой по согласованию с директором института"
            ),
            new(
                WorkId: w2.Id,
                Name:
                "Работа в совете по защите кандидатских или докторских диссертаций в качестве заместителя председателя",
                Description: "До 50 часов в год на один совет"
            ),
            new(
                WorkId: w2.Id,
                Name: "Работа в совете по защите кандидатских или докторских диссертаций в качестве председателя",
                Description: "До 60 часов в год на один совет"
            ),
            new(
                WorkId: w2.Id,
                Name: "Работа в совете по защите кандидатских или докторских диссертаций в качестве ученого секретаря",
                Description: "До 80 часов в год на один совет"
            ),
            new(
                WorkId: w2.Id,
                Name: "Работа в совете по защите кандидатских или докторских диссертаций в качестве члена совета",
                Description: "До 30 часов в год на один совет"
            ),
            new(
                WorkId: w2.Id,
                Name: "Работа в экспертном совете ВАК",
                Description: "До 60 часов в год на один совет"
            ),
            new(
                WorkId: w2.Id,
                Name:
                "Рецензирование учебников, учебных пособий, монографий, научных статей, докладов, диссертаций, авторефератов диссертаций, конкурсных и других материалов по заданию кафедры",
                Description: "До 10 часов на один печатный лист рецензируемого материала"
            ),
            new(
                WorkId: w2.Id,
                Name: "Создание результата интеллектуальной деятельности",
                Description: "До 50-150 часов за заявку"
            )
        };

        var w3 = new Work(
            Id: Guid.Parse(SystemWorks.GuidanceWorkId),
            Name: "Руководство научно-исследовательской работой студентов",
            Order: 3
        );

        var ets3 = new List<EventType>
        {
            new(
                WorkId: w3.Id,
                Name:
                "Руководство НИРС без представления в текущем учебном году студентом научной статьи, научной работы на конкурс НИРС, доклада на научной конференции, симпозиуме, семинаре",
                Description: "До 15 часов в семестр"
            ),
            new(
                WorkId: w3.Id,
                Name: "Руководство НИРС в кружке СНО по госбюджетной тематике",
                Description: "До 50 часов в год"
            ),
            new(WorkId: w3.Id, Name: "Руководство НИРС в СКБ", Description: "До 100 часов в год"),
            new(
                WorkId: w3.Id,
                Name:
                "Руководство НИРС с представлением в текущем учебном году студентом научной статьи, научной работы на конкурс НИРС, доклада на научной конференции, симпозиуме, семинаре",
                Description: "До 25 часов в семестр"
            )
        };

        var w4 = new Work(
            Id: Guid.Parse(SystemWorks.OrganizationalMethodicalWorkId),
            Name: "Организационно-методическая работа",
            Order: 4
        );

        var ets4 = new List<EventType>
        {
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей заместителя директора без занимания должности ",
                Description: "100-300 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей ответственного по кафедре за воспитательную работу",
                Description: "До 50 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей ответственного по кафедре за дипломное проектирование",
                Description: "До 50 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей ответственного по кафедре за курсовое проектирование",
                Description: "До 50 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей ответственного по кафедре за НИРС",
                Description: "До 70 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name:
                "Исполнение обязанностей ответственного по кафедре за организацию производственных практик студентов",
                Description: "Устанавливается заведующим кафедрой до 70 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей ответственного по кафедре по менеджменту качества",
                Description: "До 60 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей по отдельному виду деятельности на кафедре, в институте",
                Description: "Устанавливается соответственно заведую-щим кафедрой, директором по согласова-нию с УМУ"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей секретаря ученого совета института",
                Description: "До 50 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей уполномоченного по качеству института",
                Description: "До 100 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей Ученого секретаря Ученого совета университета",
                Description: "До 50 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей члена научно-технического  и научно-методического совета университета",
                Description: "До 30 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей члена ученого совета института",
                Description: "До 20 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Исполнение обязанностей члена Ученого совета университета",
                Description: "До 60 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Проведение профориентационной работы",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w4.Id,
                Name: "Работа  руководителем секции (совета) при методическом совете университета",
                Description: "До 100 часов год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Работа в отборочной комиссии факультета",
                Description: "50 – 100 часов год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Работа в приемной комиссии зам. ответственного секретаря",
                Description: "200 – 600 часов год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Работа в приемной комиссии ответственным секретарем",
                Description: "300 – 700 часов год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Работа в приемной комиссии сотрудником",
                Description: "6 часов за один рабочий день"
            ),
            new(
                WorkId: w4.Id,
                Name:
                "Работа заместителем заведующего кафедрой по учебной работе при числе преподавателей на кафедре до 10 человек",
                Description: "30 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name:
                "Работа заместителем заведующего кафедрой по учебной работе при числе преподавателей на кафедре с 10 до 20 человек",
                Description: "60 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name:
                "Работа заместителем заведующего кафедрой по учебной работе при числе преподавателей на кафедре свыше 20 человек",
                Description: "90 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Работа членом секции (совета) при методическом совете университета",
                Description: "До 50 часов год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Руководство институтом",
                Description: "Директор – до 800 часов; заместитель директора – до 400 часов"
            ),
            new(
                WorkId: w4.Id,
                Name:
                "Руководство кафедрой (в т.ч. разработка учебных планов специализаций, планирование и контроль учебной нагрузки, посещение занятий преподавателей кафедры)",
                Description: "До 300 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name:
                "Организация и проведение научной конференции на базе кафедры и подготовка к публикации сборника статей (докладов)",
                Description: "Устанавливается заведующим кафедрой до 70 часов в год"
            ),
            new(
                WorkId: w4.Id,
                Name: "Участие в заседаниях кафедры и работе методических семинаров кафедры",
                Description: "До 40 часов в год"
            )
        };

        var w5 = new Work(
            Id: Guid.Parse(SystemWorks.EducationalWorkId),
            Name: "Воспитательная работа",
            Order: 5
        );

        var ets5 = new List<EventType>
        {
            new(
                WorkId: w5.Id,
                Name: "Информационно-воспитательная работа",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w5.Id,
                Name:
                "Организация и проведение диагностической работы, работы по ведению мониторинга, работы предусмотренной планами воспитательных, творческих и иных мероприятий, проводимых с обучающимися",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w5.Id,
                Name:
                "Организация и проведение спортивных мероприятий (занятий по физической подготовке со студентами в оздоровительно-спортивных лагерях и на учебно-тренировочных сборах, спортивные праздники, вечера,  показательные выступления спортсменов, спортивные соревнования)",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w5.Id,
                Name: "Работа в качестве куратора групп",
                Description: "50-100 часов в год"
            ),
            new(
                WorkId: w5.Id,
                Name: "Работа со студенческим активом",
                Description: "По решению кафедры"
            )
        };

        var w6 = new Work(
            Id: Guid.Parse(SystemWorks.MedicalWorkId),
            Name: "Осуществление медицинской деятельности, необходимой для практической подготовки обучающихся",
            Order: 6
        );

        var ets6 = new List<EventType>
        {
            new(
                WorkId: w6.Id,
                Name: "Проведение обследования пациентов с целью установления диагноза",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name: "Назначение лечения пациенту и контроль его эффективности и безопасности",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name: "Проведение хирургических операций у больных ",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name: "Реализация и контроль эффективности индивидуальных реабилитационных программ для пациентов",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name:
                "Проведение профилактических мероприятий для взрослого населения и детей по возрастным группам и состоянию здоровья, проведение санитарнопросветительной работы по формированию здорового образа жизни и контроль их эффективности",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name: "Оказание медицинской помощи населению.",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name:
                "Организация лечебнодиагностического процесса (выбор методов диагностики и лечения, лекарственное обеспечение пациентов)",
                Description: "По решению кафедры"
            ),
            new(
                WorkId: w6.Id,
                Name: "Организация деятельности подчиненного медицинского персонала",
                Description: "По решению кафедры"
            )
        };

        var w7 = new Work(
            Id: Guid.Parse(SystemWorks.ExtraWorkId),
            Name: "Дополнительное профессиональное образованию по профилю педагогической деятельности",
            Order: 7
        );

        var ets7 = new List<EventType>
        {
            new(
                WorkId: w7.Id,
                Name: "Повышение квалификации в ТулГУ или другом вузе",
                Description: "В объеме установленной программы обучения"
            ),
            new(
                WorkId: w7.Id,
                Name: "Профессиональная переподготовка, в т.ч. для получения дополнительной квалификации",
                Description: "В объеме установленной программы обучения"
            ),
            new(
                WorkId: w7.Id,
                Name:
                "Стажировка на производственном предприятии, в вузе или какой-либо другой организации в другом городе",
                Description: "До 150 часов в месяц"
            ),
            new(
                WorkId: w7.Id,
                Name: "Стажировка на производственном предприятии, в вузе или какой-либо другой организации в Туле",
                Description: "До 100 часов в месяц"
            )
        };

        return new Tuple<List<Work>, List<EventType>>(
            new List<Work> { w1, w2, w3, w4, w5, w6, w7 },
            new List<EventType> { ets1, ets2, ets3, ets4, ets5, ets6, ets7 }
        );
    }
}