using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Infrastructure
{
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; } //TODO: Renamed since a possible conflict with State entity column
    }
}