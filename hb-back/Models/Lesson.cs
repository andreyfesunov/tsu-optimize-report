﻿namespace BackendBase.Models;

public class Lesson : Base
{
    public Event Event { get; set; }
    public LessonType LessonType { get; set; }

    public DateTime FactDate { get; set; }
    public DateTime PlanDate { get; set; }
}