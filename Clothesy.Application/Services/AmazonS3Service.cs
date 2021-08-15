using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Clothesy.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Clothesy.Application.Services
{
    public class AmazonS3Service
    {

        public static async Task<GetObjectModel> GetObject(string name, string bucketName, string secretKey, string accessKey)
        {
            var client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.EUCentral1);

            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = name
            };
            var responseObject = await client.GetObjectAsync(request);
            return new GetObjectModel
            {

                ContentType = responseObject.Headers.ContentType,
                Content = responseObject.ResponseStream
            };
        }

        public static async Task<UploadObjectModel> UploadObject(IFormFile file, string bucketName, string secretKey, string accessKey)
        {
            if (file == null)
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = "0b4ec21f-be0e-4ec9-8380-40e21a780f49plain-white-background-1480544970glP.jpg"
                };
            }
            // connecting to the clientZ
            var client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.EUCentral1);

            byte[] fileBytes = new Byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

            var fileName = Guid.NewGuid() + file.FileName;

            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                response = await client.PutObjectAsync(request);
            };

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                // this model is up to you, in my case I have to use it following;
                return new UploadObjectModel
                {
                    Success = true,
                    FileName = fileName
                };
            }
            else
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }

        public static async Task<UploadObjectModel> RemoveObject(String fileName, string bucketName, string secretKey, string accessKey)
        {
            var client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.EUCentral1);

            var request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            };

            var response = await client.DeleteObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new UploadObjectModel
                {
                    Success = true,
                    FileName = fileName
                };
            }
            else
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }

    }
}