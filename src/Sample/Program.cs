using DynamicJsonPropertyNamingPolicy.JsonNamingPolicies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o =>
{
    o.InputFormatters.Insert(0, new DynamicSystemTextJsonInputFormatter());
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Sample", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
