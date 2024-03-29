﻿using Dapper.QX.Abstract;
using Dapper.QX.Attributes;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Interfaces;
using LiteInvoice.Entities;

namespace LiteInvoice.Server.Queries;

public class MyProjects : TestableQuery<Project>, IUserQuery
{
	public MyProjects() : base(
		"""
		SELECT 
			"p".*
		FROM 
			"Projects" AS "p" 
			INNER JOIN "Customers" AS "c" ON "p"."CustomerId" = "c"."Id"
			INNER JOIN "AspNetUsers" AS "u" ON "c"."BusinessId" = "u"."CurrentBusinessId"
		WHERE
			"u"."UserId" = @userId {andWhere}
		""")
	{			
	}

	public int UserId { get; set; }

	[Where(@"""p"".""IsActive""=@isActive")]
	public bool? IsActive { get; set; } = true;

	[Where(@"""p"".""CustomerId""=@customerId")]
	public int? CustomerId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyProjects() { UserId = 1 };
		yield return new MyProjects() { UserId = 1, CustomerId = 1 };
		yield return new MyProjects() { UserId = 1, IsActive = true };
	}
}
