using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Dapr.Workflow;
using PetWorkflow.Shared;

namespace PetWorkflow.Activities;

public class GetLabelsFromRekognition : WorkflowActivity<string, List<string>>
{
    public override async Task<List<string>> RunAsync(WorkflowActivityContext context, string key)
    {
        
        try
        {
            var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.USEast1);
            var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);

            var detectCustomLabelsRequest = new DetectCustomLabelsRequest
            {
                ProjectVersionArn = ConfigAWSPet.modelArn,
                Image = new Image
                    { S3Object = new Amazon.Rekognition.Model.S3Object { Bucket = ConfigAWSPet.sourceBucket, Name = key } },
                //MaxResults = 1, // Optional, you can adjust as needed
                //MinConfidence = 50 // Optional, for filtering low-confidence labels
            };

            var detectCustomLabelsResponse = await rekognitionClient.DetectCustomLabelsAsync(detectCustomLabelsRequest);

            var request = new CopyObjectRequest
            {
                SourceBucket = ConfigAWSPet.sourceBucket,
                SourceKey = key,
                DestinationBucket = ConfigAWSPet.destinationBucket,
                DestinationKey = key,
                MetadataDirective = S3MetadataDirective.REPLACE
            };

            // Add metadata

            if (detectCustomLabelsResponse.CustomLabels.Count > 0)
            {
                request.Metadata.Add("pet-breed", detectCustomLabelsResponse.CustomLabels.FirstOrDefault().Name);
                request.Metadata.Add("confidence",
                    detectCustomLabelsResponse.CustomLabels.FirstOrDefault().Confidence.ToString());
            }
            else
            {
                request.Metadata.Add("pet-breed", "NOT_FOUND");
            }

            request.Metadata.Add("Origin", "CatalystWorkflows");

            await s3Client.CopyObjectAsync(request);
            await s3Client.DeleteObjectAsync(ConfigAWSPet.sourceBucket, key);

            return detectCustomLabelsResponse.CustomLabels.Select(x => $"{x.Name}:{x.Confidence}").ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<string>();
        }
    }
}