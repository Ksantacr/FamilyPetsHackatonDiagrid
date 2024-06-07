# PetCenterServices

## Verify Diagrid acccess

1. `diagrid login`
1. `diagrid whoami`
1. `diagrid project use familypethackaton`

## Create app id
1. `diagrid appid create caller-pet`
1. Verify service status
   2. `diagrid appid get caller-pet --project familypethackaton`
1. `diagrid appid create target-pet`
2. Verify service status
   3. `diagrid appid get target-pet --project familypethackaton`


## Local configuration

`diagrid dev scaffold
`

## Run locally

Build the projects:

`dotnet build PetCenter.csproj`

`dotnet build ../PetCenterCallPet/PetCenterCallPet`

Create the scaffold (yaml with settings)

`diagrid dev scaffold
`
Finally

`diagrid dev start`