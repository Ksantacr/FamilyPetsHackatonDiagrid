using Dapr.Workflow;
using PetWorkflow.Activities;

namespace PetWorkflow.Workflows;

public class PetFamilyWorkflow : Workflow<string, string>
{
    public override async Task<string> RunAsync(WorkflowContext context, string input)
    {
        var status = await context.CallActivityAsync<string>(
            nameof(VerifyProjectStatusRekognition),
            input);
        if (status == "STOPPED")
        {
            // Init model from Amazon Rekognition Custom Labels
            var initModelRekogniton = await context.CallActivityAsync<bool>(nameof(InitRekognitionModel), status);

            if (initModelRekogniton)
            {
                var processS3Images = await context.CallActivityAsync<List<string>>(nameof(ProcessS3Images), status);

                if (processS3Images.Count > 0)
                {
                    // GetLabelsFromRekognition
                    foreach (var image in processS3Images)
                    {
                        await context.CallActivityAsync<List<string>>(nameof(GetLabelsFromRekognition), image);
                    }
                }

                var stopModelProcess = await context.CallActivityAsync<bool>(nameof(StopRekognitionModel), "");

                if (stopModelProcess)
                {
                    return "ALL_OK";
                }

                return "PROBLEM_WHILE_STOP_MODEL";
            }

            return "IMAGE_NEED_PROCESS";
        }

        return "MODEL_IN_PROCESS";
    }
}