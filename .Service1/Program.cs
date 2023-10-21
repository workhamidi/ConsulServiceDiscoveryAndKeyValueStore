using Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IConsulClient, ConsulClient>
(p => 
    new ConsulClient(consulConfig =>
{
    consulConfig.Address = new Uri("http://localhost:60171");
    consulConfig.Token = "91e121be-0667-edf3-4239-d322f6d0d5d9";
    consulConfig.Datacenter = "d11";
}));


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
