namespace DevFreela.Core.Messaging
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(string queue, T message, CancellationToken cancellationToken = default);
    }
}
