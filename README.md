# Email Queue Trigger Function for .NET Core API 

This project is a .NET Core backend system that includes an Azure Function designed to process emails from a queue using an EmailQueueTrigger. The Azure Function listens for messages in an Azure Storage Queue and sends out emails based on the message content (This project is used in the [Ecommerce Backend Repository](https://github.com/Torri04/Ecommerce-Backend))

## Table of Contents
- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Configuration](#configuration)
- [Running the Function](#running-the-function)
- [Deploying to Azure](#deploying-to-azure)

## Prerequisites

Before running the project, ensure you have the following installed:

- [.NET Core SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) (For deploying and managing resources on Azure)

## Setup

1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/your-repo-name.git
    cd your-repo-name
    ```

2. **Restore the dependencies**:
    ```bash
    dotnet restore
    ```

3. **Build the project**:
    ```bash
    dotnet build
    ```

## Configuration

The function relies on several configuration settings, which can be found in the `local.settings.json` file for local development and in the Azure portal for production.

### `local.settings.json`

Create a `local.settings.json` file in the root of the project with the following structure:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "<Your-Azure-ConnectionString>",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "Mail": "<Your-Email>",
    "DisplayName": "<Email-DisplayName>",
    "Password": "<Your-AppPassword>",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
}
```

## Running the Function

To run the Azure Function locally, use the following command:

```bash
func start
```

## Deploying to Azure

1. **Login to Azure**:
    ```bash
    az login
    ```

2. **Create a Function App in Azure** (if not already created):
    ```bash
    az functionapp create --resource-group <YourResourceGroup> --consumption-plan-location <YourRegion> --runtime dotnet-isolated --functions-version 4 --name <YourFunctionAppName> --storage-account <YourStorageAccount>
    ```

3. **Deploy the function**:
    ```bash
    func azure functionapp publish <YourFunctionAppName>
    ```

4. **Set Configuration in Azure**:
   Go to the Azure portal, navigate to your Function App, and set the application settings (`Mail`, `DisplayName`, `Password`, `Host`, `Port`) as per your requirements.

