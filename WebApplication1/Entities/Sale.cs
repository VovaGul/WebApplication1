﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities;

public class Sale
{
    public int SaleId { get; set; }
    public Good? Good { get; set; }
    public int? SalesAmount { get; set; }
    [Column(TypeName = "Date")]
    public DateTime? SaleDate { get; set; }
}