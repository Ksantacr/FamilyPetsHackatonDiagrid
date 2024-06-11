using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Dapr.Workflow;
using PetWorkflow.Shared;

namespace PetWorkflow.Activities;

public class StopRekognitionModel : WorkflowActivity<string, bool>
{
    public override async Task<bool> RunAsync(WorkflowActivityContext context, string input)
    {
        try
        {
            var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.USEast1);
            
            await rekognitionClient.StopProjectVersionAsync(new StopProjectVersionRequest
            {
                ProjectVersionArn = ConfigAWSPet.modelArn
            });
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}