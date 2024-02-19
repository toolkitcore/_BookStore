using System.Text.Json.Serialization;

namespace BookStore.Infrastructure.Outbox.Message;

public sealed class OutboxMessage
{
    [JsonInclude] public Guid Id { get; set; }

    [JsonInclude] public required string Type { get; set; }

    [JsonInclude] public required string Data { get; set; }

    [JsonInclude] public DateTime OccurredOn { get; set; }

    [JsonInclude] public DateTime? ProcessedDate { get; set; }
}