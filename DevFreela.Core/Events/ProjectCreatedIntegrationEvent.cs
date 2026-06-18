namespace DevFreela.Core.Events
{
    public sealed record ProjectCreatedIntegrationEvent(
        int ProjectId,
        string Title,
        int ClientId,
        int FreelancerId,
        decimal TotalCost);
}
