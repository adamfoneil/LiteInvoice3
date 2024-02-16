using LiteInvoice.Entities;
using Refit;

namespace WebApp.Client;

public interface IApiClient
{
    [Get("/Queries/MyProjects")]
    Task<IEnumerable<Project>> GetMyProjectsAsync([Header("Authoriuzation")]string userName);

    [Get("/Queries/MyCustomers")]
    Task<IEnumerable<Customer>> GetMyCustomersAsync([Header("Authoriuzation")] string userName);

    [Post("/Invoice/{projectId}")]
    Task<Invoice> CreateInvoice([Header("Authoriuzation")] string userName, int projectId);

    [Get("/Queries/MyPendingWorkEntries?projectId={projectId}")]
    Task<IEnumerable<WorkEntry>> GetMyPendingWorkEntries([Header("Authoriuzation")] string userName, int projectId);

    [Post("/Entities/WorkEntries")]
    Task<WorkEntry> SaveWorkEntryAsync([Header("Authoriuzation")] string userName, WorkEntry workEntry);

    [Delete("/Entities/WorkEntries")]
    Task DeleteWorkEntryAsync([Header("Authoriuzation")] string userName, WorkEntry workEntry);

    [Get("/Queries/MyPendingLineEntries?projectId={projectId}")]
    Task<IEnumerable<LineEntry>> GetMyPendingLineEntries([Header("Authoriuzation")] string userName, int projectId);

    [Delete("/Entities/LineEntries")]
    Task DeleteLineEntryAsync([Header("Authoriuzation")] string userName, LineEntry lineEntry);

    [Post("/Entities/LineEntries")]
    Task<LineEntry> SaveLineEntryAsync([Header("Authoriuzation")] string userName, LineEntry lineEntry);
}
