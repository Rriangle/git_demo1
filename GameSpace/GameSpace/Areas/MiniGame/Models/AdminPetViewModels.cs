using GameSpace.Models;
using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminPetIndexViewModel
    {
        public PagedResult<PetReadModel> Pets { get; set; } = new();
        public PetSummaryReadModel PetSummary { get; set; } = new();
        public PetQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetRulesViewModel
    {
        public PetRuleReadModel PetRule { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetDetailsViewModel
    {
        public PetDetailReadModel Pet { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetEditViewModel
    {
        public PetDetailReadModel Pet { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetSkinColorChangeLogViewModel
    {
        public PagedResult<PetSkinColorChangeLogReadModel> SkinColorChangeLogs { get; set; } = new();
        public PetQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminPetBackgroundColorChangeLogViewModel
    {
        public PagedResult<PetBackgroundColorChangeLogReadModel> BackgroundColorChangeLogs { get; set; } = new();
        public PetQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }
}
