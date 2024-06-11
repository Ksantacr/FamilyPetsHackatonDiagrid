using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Dapr.Workflow;
using PetWorkflow.Shared;
using Polly;

namespace PetWorkflow.Activities;

public class InitRekognitionModel : WorkflowActivity<string, bool>
{
    public override async Task<bool> RunAsync(WorkflowActivityContext context, string input)
    {
        var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.USEast1);

        var describeModelRequest = new DescribeProjectVersionsRequest
        {
            ProjectArn = ConfigAWSPet.projectArn, //Tu projectArn
            VersionNames = new List<string> { ConfigAWSPet.versionName } //Tu versionName
        };

        try
        {
            Console.WriteLine($"Starting model: {ConfigAWSPet.modelArn}");
            await rekognitionClient.StartProjectVersionAsync(new StartProjectVersionRequest
            {
                ProjectVersionArn = ConfigAWSPet.modelArn,
                MinInferenceUnits = ConfigAWSPet.minInferenceUnits
            });

            var retryPolicy = Polly.Policy
                .HandleResult<DescribeProjectVersionsResponse>(r => r.ProjectVersionDescriptions[0].Status != "RUNNING")
                .WaitAndRetryAsync(15, retryAttempt => TimeSpan.FromMinutes(2));

            var describeResponse =
                await retryPolicy.ExecuteAsync(() => rekognitionClient.DescribeProjectVersionsAsync(describeModelRequest));

            var model = describeResponse.ProjectVersionDescriptions[0];
            Console.WriteLine($"Final Status: {model.Status}");
            Console.WriteLine($"Message: {model.StatusMessage}");

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}