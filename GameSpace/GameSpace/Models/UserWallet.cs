using System;

public partial class UserWallet
{
    public int UserId { get; set; }
    public int UserPoint { get; set; }
    
    public virtual GameSpace.Models.User User { get; set; } = null!;
}
