using System;
using System.Collections.Generic;

namespace GameSpace.Models;

public partial class SignIn
{
    public int SignInId { get; set; }
    public int UserId { get; set; }
    public DateTime SignInTime { get; set; }
    public int PointsGained { get; set; }
    public int ExpGained { get; set; }
    public string? CouponGained { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual User User { get; set; } = null!;
}
