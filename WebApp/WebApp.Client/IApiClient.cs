using LiteInvoice.Entities;
using Refit;

namespace WebApp.Client;

public interface IApiClient
{
    [Get("/api/Queries/MyProjects")]
    Task<IEnumerable<Project>> GetMyProjectsAsync([Header("Authorization")] string userId);

    [Get("/api/Queries/MyCustomers")]
    Task<IEnumerable<Customer>> GetMyCustomersAsync([Header("Authorization")] string userId);

    [Post("/api/Invoice/{projectId}")]
    Task<Invoice> CreateInvoice([Header("Authorization")] string userId, int projectId);

    [Get("/api/Queries/MyPendingWorkEntries?projectId={projectId}")]
    Task<IEnumerable<WorkEntry>> GetMyPendingWorkEntries([Header("Authorization")] string userId, int projectId);

    [Post("/api/Entities/WorkEntries")]
    Task<WorkEntry> SaveWorkEntryAsync([Header("Authorization")] string userId, WorkEntry workEntry);

    [Delete("/api/Entities/WorkEntries")]
    Task DeleteWorkEntryAsync([Header("Authorization")] string userId, [Body]WorkEntry workEntry);

    [Get("/api/Queries/MyPendingLineEntries?projectId={projectId}")]
    Task<IEnumerable<LineEntry>> GetMyPendingLineEntries([Header("Authorization")] string userId, int projectId);

    [Delete("/api/Entities/LineEntries")]
    Task DeleteLineEntryAsync([Header("Authorization")] string userId, [Body]LineEntry lineEntry);

    [Post("/api/Entities/LineEntries")]
    Task<LineEntry> SaveLineEntryAsync([Header("Authorization")] string userId, LineEntry lineEntry);
}
