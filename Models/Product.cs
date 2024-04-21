using System;
using System.Collections.Generic;

namespace Product_Inventory.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? CategoryId { get; set; }

    public int Quantity { get; set; }

    public virtual Category? Category { get; set; }
}
