using Microsoft.Extensions.DependencyInjection;
using PocGithubCopilotAgentQdrantCleancode.Application.Interfaces;
using PocGithubCopilotAgentQdrantCleancode.Application.Services;
using PocGithubCopilotAgentQdrantCleancode.Domain.Entities;
using PocGithubCopilotAgentQdrantCleancode.Domain.Interfaces;
using PocGithubCopilotAgentQdrantCleancode.Infrastructure.Repositories;
using PocGithubCopilotAgentQdrantCleancode.Infrastructure.Services;

namespace PocGithubCopilotAgentQdrantCleancode.Presentation.Evaluator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddScoped<IEmbeddingService, OpenAIEmbeddingService>()
                .AddScoped<IRepository, QdrantRepository>()
                .AddScoped<IDocumentService, DocumentService>()
                .BuildServiceProvider();

            var documentService = serviceProvider.GetRequiredService<IDocumentService>();

            Console.WriteLine("Qdrant Document Evaluator");
            Console.WriteLine("=======================");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Add a document");
                Console.WriteLine("2. Search for documents");
                Console.WriteLine("3. List all documents");
                Console.WriteLine("4. Exit");
                Console.Write("\nYour choice: ");

                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        await AddDocumentAsync(documentService);
                        break;
                    case "2":
                        await SearchDocumentsAsync(documentService);
                        break;
                    case "3":
                        await ListAllDocumentsAsync(documentService);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }

            Console.WriteLine("Thank you for using the Qdrant Document Evaluator!");
        }

        private static async Task AddDocumentAsync(IDocumentService documentService)
        {
            Console.WriteLine("\nAdd a new document");
            Console.Write("Document ID (or leave empty for auto-generated): ");
            var id = Console.ReadLine();
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }

            Console.Write("Document content: ");
            var content = Console.ReadLine() ?? string.Empty;

            var document = new Document(id, content);
            var result = await documentService.CreateDocumentAsync(document);
            
            if (result)
            {
                Console.WriteLine($"Document added successfully with ID: {id}");
            }
            else
            {
                Console.WriteLine("Failed to add document.");
            }
        }

        private static async Task SearchDocumentsAsync(IDocumentService documentService)
        {
            Console.WriteLine("\nSearch for documents");
            Console.Write("Enter search query: ");
            var query = Console.ReadLine() ?? string.Empty;

            try
            {
                var results = await documentService.SearchSimilarDocumentsAsync(query);
                
                Console.WriteLine($"\nFound {results.Count()} results:");
                
                foreach (var doc in results)
                {
                    Console.WriteLine($"ID: {doc.Id}, Content: {doc.Content}");
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Search functionality is not yet implemented.");
            }
        }

        private static async Task ListAllDocumentsAsync(IDocumentService documentService)
        {
            Console.WriteLine("\nListing all documents");
            
            var documents = await documentService.GetAllDocumentsAsync();
            if (!documents.Any())
            {
                Console.WriteLine("No documents found.");
                return;
            }
            
            foreach (var doc in documents)
            {
                Console.WriteLine($"ID: {doc.Id}, Content: {doc.Content}");
            }
        }
    }
}
