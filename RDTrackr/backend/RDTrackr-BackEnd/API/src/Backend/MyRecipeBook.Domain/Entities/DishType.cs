using System.ComponentModel.DataAnnotations.Schema;

namespace RDTrackR.Domain.Entities
{
    [Table("DishType")]
    public class DishType : EntityBase
    {
        public RDTrackR.Domain.Enums.DishType Type { get; set; }
        public long RecipeId { get; set; }
    }
}
