﻿using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Data.Entities;

public class Invoice : BaseTable
{
    [NotUpdated]
    public int BusinessId { get; set; }
    [NotUpdated]
    public int Number { get; set; }	
    [NotUpdated]
	[Column(TypeName = "decimal(6,2)")]
	public decimal Amount { get; set; }
	[Column(TypeName = "decimal(6,2)")]
	public decimal PaidAmount { get; set; }
}