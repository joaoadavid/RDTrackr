using RDTrackR.Communication.Enums;

namespace RDTrackR.Communication.Requests.Recipe
{
    public class RequestRecipeJson
    {
        public string Title { get; set; } = string.Empty;
        public CookingTime? CookingTime { get; set; }
        public Difficulty? Difficulty { get; set; }
        public List<string> Ingredients { get; set; } = [];
        public List<RequestInstructionJson> Instructions { get; set; } = [];
        public List<DishType> DishTypes { get; set; } = [];
    }
}
