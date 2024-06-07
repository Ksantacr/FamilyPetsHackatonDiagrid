
using Dapr.Client;

namespace PetCenterCallPet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var apiToken = Environment.GetEnvironmentVariable("DAPR_API_TOKEN");
            var grpcEndpoint = Environment.GetEnvironmentVariable("DAPR_GRPC_ENDPOINT");
            var httpEndpoint = Environment.GetEnvironmentVariable("DAPR_HTTP_ENDPOINT");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDaprClient(options => {
                options.UseDaprApiToken(apiToken);
                options.UseGrpcEndpoint(grpcEndpoint);
                options.UseHttpEndpoint(httpEndpoint);
            });

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
        }
    }
}
