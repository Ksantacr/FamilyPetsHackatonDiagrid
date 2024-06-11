using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Dapr.Workflow;
using PetWorkflow.Shared;

namespace PetWorkflow.Activities;

public class ProcessS3Images : WorkflowActivity<string, List<string>>
{
    public override async Task<List<string>> RunAsync(WorkflowActivityContext context, string input)
    {
        var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);

        try
        {
            // List images
            var listObjectsRequest = new ListObjectsV2Request { BucketName = ConfigAWSPet.sourceBucket };
            var listObjectsResponse = await s3Client.ListObjectsV2Async(listObjectsRequest);

            return listObjectsResponse.S3Objects.Select(x => x.Key).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<string>();
        }
    }
}