using LiteInvoice.Entities;
using Refit;

namespace WebApp.Client;

public interface IApiClient
{
    [Get("/Queries/MyProjects")]
    Task<IEnumerable<Project>> GetMyProjectsAsync([Header("Authorization")]string userName);

    [Get("/Queries/MyCustomers")]
    Task<IEnumerable<Customer>> GetMyCustomersAsync([Header("Authorization")]string userName);

    [Post("/Invoice/{projectId}")]
    Task<Invoice> CreateInvoice([Header("Authorization")]string userName, int projectId);

    [Get("/Queries/MyPendingWorkEntries?projectId={projectId}")]
    Task<IEnumerable<WorkEntry>> GetMyPendingWorkEntries([Header("Authorization")]string userName, int projectId);

    [Post("/Entities/WorkEntries")]
    Task<WorkEntry> SaveWorkEntryAsync([Header("Authorization")]string userName, WorkEntry workEntry);

    [Delete("/Entities/WorkEntries")]
    Task DeleteWorkEntryAsync([Header("Authorization")]string userName, WorkEntry workEntry);

    [Get("/Queries/MyPendingLineEntries?projectId={projectId}")]
    Task<IEnumerable<LineEntry>> GetMyPendingLineEntries([Header("Authorization")]string userName, int projectId);

    [Delete("/Entities/LineEntries")]
    Task DeleteLineEntryAsync([Header("Authorization")]string userName, LineEntry lineEntry);

    [Post("/Entities/LineEntries")]
    Task<LineEntry> SaveLineEntryAsync([Header("Authorization")]string userName, LineEntry lineEntry);
}
