using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PixelDrift
{
    public class AzureBlobService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=pixeldriftblob;AccountKey=TeXiD7pd+8ATNsgXsl9aZzJEOnq0bAiloBFVeGQrPKyQTl+ax14U2ou0x/4oZ8laCYh8bPVUqqaJ+AStGKsCZQ==;EndpointSuffix=core.windows.net";


        public CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("container1");
            if(blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                            }
            return blobContainer;
        }
        public AzureBlobService()
        {
            _blobServiceClient = new BlobServiceClient(blobConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("container1");
        }

        public async Task<List<BlobContentInfo>> UploadFiles (List<IFormFile> files)
        {
            var azureResponse = new List<BlobContentInfo>();
            foreach(var file  in files)
            {
                string fileName = file.FileName;
                using(var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
                    azureResponse.Add(client);
                }
            };
            return azureResponse;
        }
        //public async Task<List<BlobItem>> GetUploadBlob()
        //{
        //    var items = new List<BlobItem>();
        //    var UploadedFiles = _blobContainerClient.GetBlobsAsync();
        //    await foreach (BlobItem file in UploadedFiles)
        //    {
        //        items.Add(file);
        //    };
        //    return items;
        //}
    }
}