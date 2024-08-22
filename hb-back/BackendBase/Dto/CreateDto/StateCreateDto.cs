namespace BackendBase.Dto.CreateDto
{
    public class StateCreateDto
    {
        public StateCreateDto(
            Guid JobId,
            DateTime StartDate,
            DateTime EndDate,
            int Count = 1,
            int Hours = 1485
        )
        {
            this.JobId = JobId;
            this.Hours = Hours;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Count = Count;
        }

        public readonly int Count;
        public readonly int Hours;
        public readonly Guid JobId;
        public readonly DateTime StartDate;
        public readonly DateTime EndDate;
    }
}
