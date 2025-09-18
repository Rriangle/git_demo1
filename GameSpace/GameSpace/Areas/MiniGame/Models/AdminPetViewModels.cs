using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminPetIndexViewModel
    {
        public PagedResult<PetReadModel> Pets { get; set; } = new();
        public PetSummaryReadModel PetSummary { get; set; } = new();
        public PetQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetDetailsViewModel
    {
        public PetDetailReadModel PetDetail { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetStatisticsViewModel
    {
        public PetSummaryReadModel Summary { get; set; } = new();
        public List<PetReadModel> TopPets { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetRulesViewModel
    {
        public PetRuleReadModel Rules { get; set; } = new();
        public PetRuleUpdateModel? UpdateModel { get; set; }
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetEditViewModel
    {
        public int PetId { get; set; }
        public string PetName { get; set; } = string.Empty;
        public string? PetType { get; set; }
        public int PetLevel { get; set; }
        public int PetExperience { get; set; }
        public int PetHappiness { get; set; }
        public int PetHealth { get; set; }
        public int PetHunger { get; set; }
        public string Sidebar { get; set; } = "admin";
    }
}
