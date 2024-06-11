using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Dapr.Workflow;
using PetWorkflow.Shared;

namespace PetWorkflow.Activities;

public class VerifyProjectStatusRekognition : WorkflowActivity<string, string>
{
    public override async Task<string> RunAsync(WorkflowActivityContext context, string modelArn)
    {
        var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.USEast1);
        var describeModelRequest = new DescribeProjectVersionsRequest
        {
            ProjectArn = ConfigAWSPet.projectArn,
            VersionNames = new List<string> { ConfigAWSPet.versionName }
        };

        try
        {
            var describeModelResponse = await rekognitionClient.DescribeProjectVersionsAsync(describeModelRequest);

            if (describeModelResponse.ProjectVersionDescriptions.Count > 0)
            {
                var modelStatus = describeModelResponse.ProjectVersionDescriptions[0].Status;
                return modelStatus;
            }

            return "MODEL_NOT_FOUND";
        }
        catch (AmazonRekognitionException e)
        {
            return "ERROR" + e.Message;
        }
    }
}