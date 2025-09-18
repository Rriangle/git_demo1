using System;
using System.Collections.Generic;

namespace GameSpace.Models;

public partial class ManagerData
{
    public int ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public string? ManagerAccount { get; set; }
    public string? ManagerPassword { get; set; }
    public DateTime? AdministratorRegistrationDate { get; set; }
    public string ManagerEmail { get; set; } = null!;
    public bool ManagerEmailConfirmed { get; set; }
    public int? ManagerRoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual ManagerRole? ManagerRole { get; set; }
}
