namespace BackendBase.Dto;

public class LessonTypeDto
{
    public LessonTypeDto(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public readonly Guid Id;
    public readonly string Name;
}
