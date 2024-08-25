namespace Tsu.IndividualPlan.WebApi.Dto;

public class ActivityDto
{
    public ActivityDto(Guid Id, string Name, int Column)
    {
        this.Id = Id;
        this.Name = Name;
        this.Column = Column;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Column { get; init; }
}