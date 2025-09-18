using System;
using System.Collections.Generic;

namespace GameSpace.Models;

public partial class UserPoints
{
    public int UserId { get; set; }
    public int Points { get; set; }
    public DateTime LastUpdated { get; set; }
    
    public virtual User User { get; set; } = null!;
}
