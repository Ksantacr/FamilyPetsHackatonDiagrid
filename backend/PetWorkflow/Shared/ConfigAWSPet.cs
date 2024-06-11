namespace PetWorkflow.Shared;

public static class ConfigAWSPet
{
    public static string sourceBucket = Environment.GetEnvironmentVariable("sourceBucket");
    public static string destinationBucket = Environment.GetEnvironmentVariable("destinationBucket");
    public static string modelArn = Environment.GetEnvironmentVariable("modelArn");
    public static string projectArn = Environment.GetEnvironmentVariable("projectArn");
    public static string versionName = Environment.GetEnvironmentVariable("versionName");
    public static int minInferenceUnits = int.Parse(Environment.GetEnvironmentVariable("minInferenceUnits"));
}