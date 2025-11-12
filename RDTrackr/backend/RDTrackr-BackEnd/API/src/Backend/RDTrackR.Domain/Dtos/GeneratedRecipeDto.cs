using RDTrackR.Domain.Enums;

namespace RDTrackR.Domain.Dtos
{
    public class GeneratedRecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public IList<string> Ingredients { get; set; } = [];
        public IList<GeneratedInstructionsDto> Instructions { get; set; } = [];
        public CookingTime CookingTime { get; init; }
    }
}
