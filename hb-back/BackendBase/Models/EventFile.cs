using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models
{
    /** @deprecated */
    public class EventFile : Base
    {
        protected EventFile() { }

        [SetsRequiredMembers]
        public EventFile(Guid FileId, Guid EventId, Guid? Id = null)
            : base(Id)
        {
            this.FileId = FileId;
            this.EventId = EventId;
        }

        public required Guid FileId { get; init; }
        public required Guid EventId { get; init; }

        public File? File { get; init; }
        public Event? Event { get; init; }
    }
}
