using CosmosDB.Commands;
using CosmosDB.Data;
using CosmosDB.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCosmosDb(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/cutomer", (DemoContext dbContext, bool ensureCreated ) =>
{
    if (!ensureCreated)
    {
        dbContext.Database.EnsureCreated();
        ensureCreated = true;
    }

    var forecast =  Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
    return forecast;
})
.WithName("GetCustomer")
.WithOpenApi();

app.MapPost("/cutomer",  async (DemoContext dbContext, CreateCustomerCommand customer ) =>
{
    var newCustomer = new Customer
    {
        Name = customer.Name,
        Email = customer.Email
    };

    var setAddresses = customer.Addresses.FirstOrDefault();

    if (setAddresses is not null)
    {
        var address = new Address
        {
            Street = setAddresses.Street,
            City = setAddresses.City,
            State = setAddresses.State,
            ZipCode = setAddresses.ZipCode
        };
        newCustomer.DefaultShippingAddress = new DefaultAddress(address);
        newCustomer.DefaultBillingAddress = new DefaultAddress(address);
    }

    var addedCustomer = await dbContext.AddAsync(newCustomer);
    await dbContext.SaveChangesAsync();

    if(setAddresses is null)
    {
        foreach (var customerAddress in customer.Addresses)
        {
            var address = new Address
            {
                CustomerId = addedCustomer.Entity.Id ?? Guid.NewGuid(),
                Street = customerAddress.Street,
                City = customerAddress.City,
                State = customerAddress.State,
                ZipCode = customerAddress.ZipCode
            };
            address.CustomerId = addedCustomer.Entity.Id ?? Guid.NewGuid();
            await dbContext.AddAsync(address);
        }

        await dbContext.SaveChangesAsync();
    }

    return Results.Ok(addedCustomer.Entity);
    
})
.WithName("CreateCustomer")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
