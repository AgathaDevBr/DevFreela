namespace DevFreela.API.Models
{
    public sealed record LoginResponse(string AccessToken, DateTime ExpiresAt);
}
