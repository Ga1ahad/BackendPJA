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
        private static string accessKey = "AKIAIGX7QIXDG65TXV7A";
        private static string accessSecret = "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";
        private static string bucket = "clothesybucket";


        public static async Task<GetObjectModel> GetObject(string name)
        {
            var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.EUCentral1);

            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucket,
                Key = name
            };
            var responseObject = await client.GetObjectAsync(request);
            return new GetObjectModel
            {

                ContentType = responseObject.Headers.ContentType,
                Content = responseObject.ResponseStream
            };
        }

        public static async Task<UploadObjectModel> UploadObject(IFormFile file)
        {
            // connecting to the client
            var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.EUCentral1);

            byte[] fileBytes = new Byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

            var fileName = Guid.NewGuid() + file.FileName;

            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucket,
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

        public static async Task<UploadObjectModel> RemoveObject(String fileName)
        {
            var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.EUCentral1);

            var request = new DeleteObjectRequest
            {
                BucketName = bucket,
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