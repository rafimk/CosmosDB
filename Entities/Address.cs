﻿namespace CosmosDB.Entities;

public class Address
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}