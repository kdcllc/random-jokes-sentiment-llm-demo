# Random Jokes Demo

This demo retrieves a random joke from an API, saves it to an Azure Storage table, and performs sentiment analysis using an LLM model.

## Getting Started

To get started with this project, follow these steps:

1. Clone the repository:

   ```bash
       git clone https://github.com/kdcllc/random-jokes-sentiment-llm-demo.git
    ```

2. Set up the Azure Storage account:

   - Create an Azure Storage account if you don't already have one.
   - Create a table named `Jokes` in the storage account.

3. Set up the configuration:

    ```bash
      dotnet user-secrets set "OpenAiOptions:Key" ""
      dotnet user-secrets set "OpenAiOptions:Endpoint" "https://{name}.openai.azure.com/"
      dotnet user-secrets set "OpenAiOptions:DeploymentId" "gpt-35-turbo"
    ```

4. Run the application:

   ```bash
    dotnet run
   ```

   This will retrieve a random joke, save it to the `Jokes` table in your Azure Storage account, and perform sentiment analysis using the LLM model.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
