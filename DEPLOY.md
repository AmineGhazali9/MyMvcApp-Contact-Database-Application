# Deploying MyMvcApp to Azure (ARM template)

This repository includes a basic ARM template that provisions an App Service Plan, a Web App, and Application Insights, and configures the app settings to include the App Insights instrumentation key.

Quick deploy using Azure CLI

1. Create a resource group (replace `myResourceGroup` and `eastus` as needed):

```bash
az group create --name myResourceGroup --location eastus
```

2. Validate the template (recommended):

```bash
az deployment group validate --resource-group myResourceGroup --template-file azuredeploy.json --parameters @azuredeploy.parameters.json
```

3. Deploy the template:

```bash
az deployment group create --resource-group myResourceGroup --template-file azuredeploy.json --parameters @azuredeploy.parameters.json
```

After deployment completes, the output will include the web app default host name and Application Insights key.

Notes and next steps

- Replace the values in `azuredeploy.parameters.json` with production-appropriate names.
- For production workloads, change the App Service SKU from the Free tier (`F1`) to a supported production SKU.
- Consider adding a Storage Account, Key Vault, and CI/CD pipeline (GitHub Actions) for automated deployments.
