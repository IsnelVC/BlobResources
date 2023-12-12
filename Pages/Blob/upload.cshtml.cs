using Azure.Storage.Blobs;
using BlobResources.Pages.Blob;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BlobResources.Pages.Blob
{
    public class uploadModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public uploadModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
   
        public void OnGet()
        {
        }

        public string Message { get; set; }

        public void OnPost(IFormFile file)
        {
            string blobName = Request.Form["blobName"];
            if (file == null || file.Length == 0)
            {
                Message = "Please select a file.";
                return;
            }
            //Get the connection string
            var connectionString = _configuration.GetConnectionString("AzureStorage");
            //Create the container name
            if (blobName != null && blobName.Length > 0)
            {
                //create the service client
                var blobServiceClient = new BlobServiceClient(connectionString);
                //Get the container using the name
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobName);
                //create a container using the provided name

                // Use a unique name for the blob or handle naming conflicts appropriately
                var blobFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                var blobClient = blobContainerClient.GetBlobClient(blobFileName);

                using (var stream = file.OpenReadStream())
                {
                    blobClient.Upload(stream, true);
                }

                Message = $"File '{file.FileName}' uploaded successfully as blob '{blobFileName}'";
            }
        }

    }
}


