﻿namespace LiteInvoice.Entities.Interfaces;

public interface IHashedResult
{
	int Id { get; }
	string HashedId { get; set; }
}
