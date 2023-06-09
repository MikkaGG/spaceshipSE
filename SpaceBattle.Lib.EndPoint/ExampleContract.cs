using System.Runtime.Serialization;

namespace SpaceBattle.Lib.EndPoint;

[DataContract(Name = "Order")]
public class Order
{
    [DataMember(Name = "type", Order = 1)]
    public string? type {get; set;}
    [DataMember(Name = "game id", Order = 2)]
    public int gameId {get; set;}
    [DataMember(Name = "game item id", Order = 3)]
    public string? gameItemId {get; set;}
    [DataMember(Name = "params", Order = 4)]
    public Parameters? others {get; set;}
}

[DataContract(Name = "Parameters")]
public class Parameters
{
    [DataMember(Name = "param")]
    public string? param {get; set;}
    [DataMember(Name = "value")]
    public object? val {get; set;}
}
