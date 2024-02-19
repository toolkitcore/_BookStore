namespace BookStore.Infrastructure.Outbox.Dapr;

public class DaprOutboxOptions
{
    public static string Name = "DaprOutbox";
    public string StateStoreName { get; set; } = "statestore";
    public string OutboxName { get; set; } = "outbox";
}