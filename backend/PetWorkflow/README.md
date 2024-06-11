# PetWorkflow

## Configuration

1. Create a new Application id: `basic-workflows`
1.	diagrid appid create workflow-app
1. Build the project: `dotnet build`
1. Configure environment variables:
	1. `export DAPR_HTTP_ENDPOINT=DAPR_HTTP_ENDPOINT`
	1. `export DAPR_GRPC_ENDPOINT=DAPR_GRPC_ENDPOINT`
	1. `export DAPR_API_TOKEN=DAPR_API_TOKEN`


## Workflow

```mermaid
graph TB
    A[Start]
    B{MLModel is STOPPED}
    C[Init Reknogition Model and Wait]
    D[Get list images from S3 bucket]
    E([for each Image in Images])
    E1[DetectCustomLabelsAsync]
    F[Add metadata and move to new bucket]
    G[Stop model]
    H[END]
    A --> |"input: {modelArn}"| B
    B --> |"[Yes]: {STOPPED}"| C
    B --> |"[No]: {RUNNING}"| H
    C --> D
    D --> |"Images[]"| E
    E -->|"input: {Image}"| E1-->|"output: {breed} {confidence}"| F
    F --> G
    G --> H
```
