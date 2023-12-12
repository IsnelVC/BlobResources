using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace BlobResources.Pages.Blob
{
    
    public class createModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public createModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        public string Message { get; set; }

        public void OnPost(IFormFile file)
        {
            try
            {
                string blobName = Request.Form["blobName"];

                //Get the connection string
                var connectionString = _configuration.GetConnectionString("AzureStorage");

                //create the service client
                var blobServiceClient = new BlobServiceClient(connectionString);

                //create a container using the provided name
                var blobFileResult = blobServiceClient.CreateBlobContainer(blobName);

                Message = $"Blob '{blobName}' was created successfully as blob '{blobName}'";
            }
            catch (Exception ex) 
            {
                Message= $"An error ocurred:'{ex.Message}'"; 
            }
         
        }
    }
}
