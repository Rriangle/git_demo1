using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class PetQueryModel
    {
        [Display(Name = "寵物ID")]
        public int? PetId { get; set; }

        [Display(Name = "用戶ID")]
        public int? UserId { get; set; }

        [Display(Name = "寵物名稱")]
        public string? PetName { get; set; }

        [Display(Name = "頁碼")]
        public int PageNumber { get; set; } = 1;

        [Display(Name = "每頁筆數")]
        public int PageSize { get; set; } = 10;
    }
}
