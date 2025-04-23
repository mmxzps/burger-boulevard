using Backend.Models;
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

        builder.Services.AddCors(options =>
        {
	        options.AddPolicy("AllowAll",
		        policy =>
		        {
			        policy.AllowAnyOrigin()   
				        .AllowAnyHeader()   
				        .AllowAnyMethod();  
		        });
        });

		builder.Services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
				options.JsonSerializerOptions.WriteIndented = true;
			});

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt => opt.CustomSchemaIds(type => type.ToString()));

        var app = builder.Build();
        //t
        if (app.Environment.IsDevelopment())
        {
          app.UseSwagger();
          app.UseSwaggerUI();
          app.UseCors("AllowAll");
		}

        app.UseHttpsRedirection();

        app.UseAuthorization();

		app.MapControllers();

        app.Run();
    }
}
