using LiteInvoice.Entities;
using Refit;

namespace WebApp.Client;

public interface IApiClient
{
    [Get("/Queries/MyProjects")]
    Task<IEnumerable<Project>> GetMyProjectsAsync();

    [Get("/Queries/MyCustomers")]
    Task<IEnumerable<Customer>> GetMyCustomersAsync();

    [Post("/Invoice/{projectId}")]
    Task<Invoice> CreateInvoice(int projectId);

    [Get("/Queries/MyPendingWorkEntries?projectId={projectId}")]
    Task<IEnumerable<WorkEntry>> GetMyPendingWorkEntries(int projectId);

    [Post("/Entities/WorkEntries")]
    Task<WorkEntry> SaveWorkEntryAsync(WorkEntry workEntry);

    [Delete("/Entities/WorkEntries")]
    Task DeleteWorkEntryAsync(WorkEntry workEntry);

    [Get("/Queries/MyPendingLineEntries?projectId={projectId}")]
    Task<IEnumerable<LineEntry>> GetMyPendingLineEntries(int projectId);

    [Delete("/Entities/LineEntries")]
    Task DeleteLineEntryAsync(LineEntry lineEntry);

    [Post("/Entities/LineEntries")]
    Task<LineEntry> SaveLineEntryAsync(LineEntry lineEntry);
}
