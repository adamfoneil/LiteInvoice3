I do a little bit of freelance work and need to create invoices. There are invoicing solutions out there, and [Billdu](https://www.billdu.com/) comes closest to what I want. But I can't justify a monthly subscription. I've used PayPal invoicing. I'm not wild about its UX, but tracking and receiving payment is easy. One of my two customers doesn't use PayPal, however, so I need a system that can use PayPal but not depend on it. I'd like a single unified interface for entering hours and creating invoices that the customer can view without a login, and offer payment links. I'd like to connect or plug in several different payment providers -- PayPal, Stripe, and who knows what else. One customer uses snail mail, so simply having my mailing address on the invoice works.

So, this a Blazor Server app using a Postgres backend to enter hours and create invoices. I'm using DigitalOcean to host the database, and there's no local database option currently. I am using standard EF migrations to manage the database, so I think in theory you could create the database yourself locally.

Another reason I make apps like this is to refine my own patterns and techniques. I've made several Blazor apps, but I'm always trying to simplify and improve things, and this project is no different in that regard.

- **CRUD operations** I've made peace with EF migrations for managing the database schema, but I still don't enjoy EF for CRUD operations.
  - I'm using [Dapper.Entities](https://github.com/adamfoneil/Dapper.Entities) for CRUD, in particular the [PostgreSql](https://www.nuget.org/packages/Dapper.Entities.PostgreSql/) package.
  - Entity and repository classes are [here](https://github.com/adamfoneil/LiteInvoice3/tree/master/LiteInvoice.Data/Entities). The "root" object or the closest thing to an EF `DbContext` is the [DapperEntities](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.Data/Entities/DapperEntities.cs) object itself. This is added to the DI container [at startup](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Program.cs#L32).
  - There is an EF [ApplicationDbContext](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.Data/ApplicationDbContext.cs), but only for enabling migrations.
  - For consistency of some entity properties, I use a [BaseTable](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.Data/Entities/Conventions/BaseTable.cs).
  - For consistency of insert/update behavior, I use a [BaseRepository](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.Data/Entities/Conventions/BaseRepository.cs). This is where I apply the user's time zone in order to set insert/update timestamps.
  - All of my pages/components that need data access inherit from [DataComponent](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/DataComponent.cs). This is how the current user is set for auditing purposes.

- **SQL queries** I use [Dapper.QX](https://github.com/adamfoneil/Dapper.QX) for SQL queries, and query objects are in [Queries](https://github.com/adamfoneil/LiteInvoice3/tree/master/LiteInvoice.Data/Queries). You might think I'm crazy to do SQL like this, but I like having full control over SQL. I appreciate what EF tries to do, but I find that its abstractions simply get in the way, sorry.
  - See also the [query tests](https://github.com/adamfoneil/LiteInvoice3/blob/master/Tests/Queries.cs). This is how I ensure that inline SQL compiles throughout the application.
  - To ensure tenant isolation, for example queries that are user-specific, I use an interface [IUserQuery](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.Data/Interfaces/IUserQuery.cs) on queries that need to be user-isolated. Along with this I use these `QueryAsync` methods that [ensure the current userId param is set](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.Data/Entities/DapperEntities.Queries.cs#L19). For example, see how I load the [current user's list of customers](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Components/Pages/Setup/Projects.razor#L44).
 
- **UI in progress** Check out pages in the [Setup](https://github.com/adamfoneil/LiteInvoice3/tree/master/LiteInvoice.App/Components/Pages/Setup) folder to see what I have working so far.
  - I'm using [Radzen](https://blazor.radzen.com/) components. I have a library of a few reusable elements in my own [Radzen.Components](https://github.com/adamfoneil/LiteInvoice3/tree/master/Radzen.Components) project -- this is stuff I got tired of copying and pasting between projects. I might make a NuGet package out of this, but for now it's just a project in the solution. This has common grid save/delete/edit/cancel buttons in [GridControls](https://github.com/adamfoneil/LiteInvoice3/blob/master/Radzen.Components/GridControls.razor) and a few other reusable things along those lines.
  - The one unique thing I'd draw your attention to is [GridHelper](https://github.com/adamfoneil/LiteInvoice3/blob/master/Radzen.Components/Abstract/GridHelper.cs). This is makes subsequent [RadzenGrid](https://blazor.radzen.com/datagrid) setup and operation really compact IMO. See this example [ProjectsGrid](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Components/Pages/Setup/Projects.GridHelper.cs), which is in use [here](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Components/Pages/Setup/Projects.razor#L35).
  - [Business.razor](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Components/Pages/Setup/Business.razor) has example of single form entry.
  - [Customers.razor](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Components/Pages/Setup/Customers.razor) shows a list customer tiles and corresponding entry form.
  - [Projects.razor](https://github.com/adamfoneil/LiteInvoice3/blob/master/LiteInvoice.App/Components/Pages/Setup/Projects.razor) has a grid example.  
