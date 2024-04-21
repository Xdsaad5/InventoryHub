using System;
using System.Collections.Generic;

namespace Product_Inventory.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public string? Location { get; set; }

    public int? IsAdmin { get; set; }
}
