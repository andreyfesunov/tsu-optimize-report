﻿namespace BackendBase.Models;

public class Activity : Base
{
    public ICollection<ActivityEventType> ActivitiesEventsTypes { get; set; }
    public string Name { get; set; }
}