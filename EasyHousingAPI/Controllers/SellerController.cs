using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using EasyHousingAPI.Models;

namespace EasyHousingSprint2.Controllers
{
    /// <summary>
    /// Azure Storage Controller
    /// </summary>
    /// <returns></returns>
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private IConfiguration Configuration;



        public SellerController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        /// <summary>
        /// Update Blob
        /// </summary>
        /// <param name="seller"></param>
        /// <returns></returns>
        // POST: Seller/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateAzureBlob([FromBody] Seller seller)
        {
            try
            {
                string sellerStr = Newtonsoft.Json.JsonConvert.SerializeObject(seller);
                string conStr = Configuration.GetConnectionString("EasyHousingConn");
                try
                {
                    UploadBlob(conStr, sellerStr, "sellercontainer", true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return Ok("Updated");
            }
            catch
            {
                return Ok("Updated");
            }
        }

        /// <summary>
        /// Upload the blob
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="fileContent"></param>
        /// <param name="containerName"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>

        public static string UploadBlob(string conStr, string fileContent, string containerName, bool isAppend = false)
        {
            string result = "Success";
            try
            {
                string fileName, existingContent;
                BlobClient blobClient;
                SetVariables(conStr, containerName, out fileName, out existingContent, out blobClient);

                if (isAppend)
                {
                    string fillerStart = "";
                    string fillerEnd = "]";
                    existingContent = GetContentFromBlob(conStr, fileName, containerName);
                    if (string.IsNullOrEmpty(existingContent.Trim()))
                    {
                        fillerStart = "[";
                        fileContent = fillerStart + existingContent + fileContent + fillerEnd;
                    }
                    else
                    {
                        existingContent = existingContent.Substring(0, existingContent.Length - 3);
                        fileContent = fillerStart + existingContent + "," + fileContent + fillerEnd;
                    }


                }
                var ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                tw.Write(fileContent);
                tw.Flush();
                ms.Position = 0;

                blobClient.UploadAsync(ms, true);
            }

            catch (Exception ex)
            {

                result = "Failed";
                throw ex;
            }
            return result;
        }
        private static void SetVariables(string conStr, string containerName, out string fileName, out string existingContent, out BlobClient blobClient)
        {
            var serviceClient = new BlobServiceClient(conStr);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);

            fileName = "Sprint2 Azure.txt";
            existingContent = "";
            blobClient = containerClient.GetBlobClient(fileName);
        }

        private static string GetContentFromBlob(string conStr, string fileName, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(conStr);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            string line = string.Empty;
            if (blobClient.Exists())
            {
                var response = blobClient.Download();
                using (var streamReader = new StreamReader(response.Value.Content))
                {
                    while (!streamReader.EndOfStream)
                    {
                        line += streamReader.ReadLine() + Environment.NewLine;
                    }
                }
            }
            return line;
        }
    }
}