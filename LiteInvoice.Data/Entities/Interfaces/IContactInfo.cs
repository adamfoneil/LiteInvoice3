﻿namespace LiteInvoice.Data.Entities.Interfaces;

public interface IContactInfo
{	
	string Name { get; set; }
	string? Email { get; set; }
	string? PhoneNumber { get; set; } 
	string? MailingAddress { get; set; }
	string? City { get; set; }
	string? State { get; set; }
	string? PostalCode { get; set; }
	string? Country { get; set; }
}