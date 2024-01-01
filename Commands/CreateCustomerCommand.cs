namespace CosmosDB.Commands;

public record CreateCustomerCommand
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }= string.Empty;
    public List<CustomerAddress> Addresses { get; set; } = [];
}

public record CustomerAddress
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; }= string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; }= string.Empty;
}