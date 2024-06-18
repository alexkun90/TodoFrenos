﻿using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool StateCateg { get; set; } = true;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
