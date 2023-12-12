using Azure.Storage.Blobs;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BlobResources.Pages.Blob
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            try
            {
                string blobName = Request.Form["blobName"];
                //Get the connection string
                var connectionString = _configuration.GetConnectionString("AzureStorage");
                //create the service client
                var blobServiceClient = new BlobServiceClient(connectionString);
                var deleteResponse = blobServiceClient.DeleteBlobContainer(blobName);
                try
                {
                    if (deleteResponse!=null)
                    {
                        Console.WriteLine($"Blob '{blobName}' deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Blob '{blobName}' does not exist.");
                    }
                }
                catch (RequestFailedException ex)
                {
                    Console.WriteLine($"Error deleting blob: {ex.Message}");
                }
            
                Message = $"Blob '{blobName}' was deleted successfully.'";
            }
            catch (Exception ex)
            {
                Message = $"An error ocurred:'{ex.Message}'";
            }
        }

    }
}



