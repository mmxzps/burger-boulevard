using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    [JsonStringEnumMemberName("pending")]
    Pending,
    [JsonStringEnumMemberName("preparing")]
    Preparing,
    [JsonStringEnumMemberName("done")]
    Done
}

public class Order
{
    public int Id { get; set; }
    public required OrderStatus Status { get; set; }
    public virtual ICollection<OrderComponent> Components { get; set; } = [];
    public bool TakeAway { get; set; }
    [Precision(8, 4)]
    public decimal TotalPrice { get; set; }

    public DateTime OrderTime { get; set; } = DateTime.Now;

    
}
