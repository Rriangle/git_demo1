using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminAdjustEVouchersViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "電子禮券ID為必填")]
        public int EVoucherId { get; set; }
        
        [Required(ErrorMessage = "數量為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0")]
        public int Quantity { get; set; }
        
        public string Sidebar { get; set; } = "admin";
    }
}
