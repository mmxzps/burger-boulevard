using Microsoft.EntityFrameworkCore;

namespace Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<BackendContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")
              ?? throw new InvalidOperationException("Missing connection string 'Database'.")));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt => opt.CustomSchemaIds(type => type.ToString()));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
          app.UseSwagger();
          app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
