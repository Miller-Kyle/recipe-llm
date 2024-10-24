# Chef-GPT - Healthy Recipe Generator
Chef-GPT is an Azure Function-powered application that uses AI to generate healthy, easy-to-cook recipes, complete with step-by-step instructions and AI-generated images of the final meal. The project leverages GPT-4o mini for recipe generation and DALL-E 3 for creating meal visuals. The entire application is provisioned and deployed using Azure Developer CLI (azd), with model integration managed via Azure AI Studio.

## Features

- **Healthy Recipe Generation**: Create recipes based on user preferences for quick and nutritious meals.
- **Step-by-Step Instructions**: Provide detailed cooking instructions to guide users through meal preparation.
- **AI-Generated Meal Images**: Use DALL-E 3 to visualize the final dish based on the recipe.
- **Azure Functions**: Utilize a serverless architecture to ensure scalability.
- **Azure AI Studio Integration**: Manage AI models for recipe and image generation.

## Prerequisites

1. **Azure Account**: An active Azure subscription is required.
2. **Azure Developer CLI (azd)**: Ensure [azd is installed](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd).
3. **Azure AI Studio Models**:
    - GPT-4o mini for text-based recipe generation.
    - DALL-E 3 for image-based meal visualization.
    - Ensure both models are created and configured in Azure AI Studio.

## Setup

### Login

Log into the Azure CLI and set the subscription. If on Windows, ensure Azure PowerShell is connected.

```bash
az login
azd auth login  # Optional: --use-device-code
```

### Configure Environment

1. Create an azd environment

    ```bash
    azd init -e dev
    ```

2. Configure the environment variables

    | Variable            | Purpose                                                                 |
    |---------------------|-------------------------------------------------------------------------|
    | GPT_ENDPOINT        | The endpoint for the GPT model used for recipe generation               |
    | DALLE_ENDPOINT      | The endpoint for the DALL-E model used for image generation             |
    | AZURE_TENANT_ID     | The Azure Tenant ID associated with your Azure subscription             |
    | AZURE_CLIENT_ID     | The Client ID of the Azure AD application with access to Azure AI Studio|
    | AZURE_CLIENT_SECRET | The Client Secret of the Azure AD application                           |

    ```bash
    azd env set GPT_ENDPOINT <GPT Endpoint>
    azd env set DALLE_ENDPOINT <DALL-E Endpoint>
    azd env set AZURE_TENANT_ID <Azure Tenant ID>
    azd env set AZURE_CLIENT_ID <Azure Client ID>
    azd env set AZURE_CLIENT_SECRET <Azure AD Secret>
    ```

### Deploy the Infrastructure and Function

```bash
azd up
```

## Model Instructions and Safety Parameters

The model's instructions are stored in [gpt-instructions-safety](./model-instruction/gpt-instructions-safety.md). A Powershell script is provided to easily turn the markdown into a string that can be passed as the model's system message. The safety parameters are adapted from the sample safety instructions in Azure AI Studio, they are not complete and should not be assumed correct.

## Additional azd commands

The workspace is brought online using the Azure Developer CLI. Additionally, Visual Studio tasks can be used.

| Action             | Command                    |
|--------------------|----------------------------|
| Start              | `azd up`                   |
| Stop               | `azd down --purge --force` |
| Deploy infra only  | `azd provision`            |
| Deploy function    | `azd deploy`               |

## Usage

Once deployed, the application can be accessed via an HTTP endpoint provided by the Azure Function. Send a POST request with the desired recipe parameters (e.g., dietary preferences, time constraints) to generate a recipe and receive an AI-generated image of the meal.

## Sequence 

::: mermaid
sequenceDiagram
    autonumber
    actor User
    participant RecipeFunction
    participant GptService
    participant DalleService
    
    User->>RecipeFunction: POST - Recipe prompt
    RecipeFunction->>GptService: POST - System context and recipe prompt
    GptService->>RecipeFunction: Response: Recipe chat
    RecipeFunction->>DalleService: POST: Generate image
    DalleService->>GptService: Image
    RecipeFunction->>User: Recipe and image
:::

## Contributing

Contributions are welcome! Please submit a pull request or open an issue to suggest improvements.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.