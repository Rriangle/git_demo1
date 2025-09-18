using System;
using System.Collections.Generic;

namespace GameSpace.Models;

public partial class EvoucherToken
{
    public int TokenId { get; set; }

    public int EvoucherId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }

    // 添加缺少的屬性
    public int UserID { get; set; }
    public int EVoucherTypeID { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}
