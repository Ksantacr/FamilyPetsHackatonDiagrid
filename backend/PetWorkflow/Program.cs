using Dapr.Workflow;
using PetWorkflow.Activities;
using PetWorkflow.Workflows;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprWorkflowClient();

builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<PetFamilyWorkflow>();

    options.RegisterActivity<VerifyProjectStatusRekognition>();
    options.RegisterActivity<InitRekognitionModel>();
    options.RegisterActivity<GetLabelsFromRekognition>();
    options.RegisterActivity<ProcessS3Images>();
    options.RegisterActivity<StopRekognitionModel>();

});

// Dapr uses a random port for gRPC by default. If we don't know what that port
// is (because this app was started separate from dapr), then assume 50001.
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DAPR_GRPC_PORT")))
{
    Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "50001");
}

var app = builder
    .Build();

var workflowClient = app.Services.GetRequiredService<DaprWorkflowClient>();

app.MapGet("/pin", () => "pon");

// Start new workflow to tag images
app.MapPost("/workflow/start", async () =>
{
    // Store state in managed diagrid state store 
    var guid = Guid.NewGuid();
    try
    {
        await workflowClient.ScheduleNewWorkflowAsync(
            name: nameof(PetFamilyWorkflow),
            input: string.Empty,
            instanceId: guid.ToString());

        app.Logger.LogInformation("Started a new PetFamilyWorkflow with id {guid} and input {input}", guid, nameof(PetFamilyWorkflow));
        return Results.Ok(guid);
    }
    catch (Exception ex)
    {
        app.Logger.LogError("Error occurred while starting workflow: {guid}. Exception: {exception}", guid,
            ex.InnerException);
        return Results.StatusCode(500);
    }
});

app.UseCloudEvents();
app.MapSubscribeHandler();

app.Run();