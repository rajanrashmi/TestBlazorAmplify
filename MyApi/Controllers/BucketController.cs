using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Amazon;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        // GET: api/<BucketController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            string bucketName = "rajan-test-bucket-2";
            List<string> fileNames = new List<string>();


            // Create a client
            AmazonS3Client client = new AmazonS3Client(RegionEndpoint.USEast1);
            

            // Issue call
            ListBucketsResponse myResponse = await client.ListBucketsAsync();

            // View response data
            Console.WriteLine("Buckets owner - {0}", myResponse.Owner.DisplayName);
            foreach (S3Bucket bucket in myResponse.Buckets)
            {
                Console.WriteLine("Bucket {0}, Created on {1}", bucket.BucketName, bucket.CreationDate);
            }

            var foundBucket = myResponse.Buckets.Select(x => x.BucketName == bucketName);
            if (foundBucket.Any())
            {
                var req = new ListObjectsV2Request()
                {
                    BucketName = bucketName,
                    Prefix = "File",
                        
                };
                int a = 0;

                try
                {
                    var bucketObjects = await client.ListObjectsV2Async(req);
                    
                    bucketObjects.S3Objects.ForEach(obj => fileNames.Add(obj.Key));
                }
                catch (Exception ex)
                {
                       Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' getting list of objects.");

                }
                  
               //var objectBucket = await client.ListObjectsAsync(bucketName);
                //var listObjectsV2Paginator = client.Paginators.ListObjectsV2(new ListObjectsV2Request
                //{
                //    BucketName = bucketName,
                    
                    
                //});

                //await foreach (var response in listObjectsV2Paginator.Responses)
                //{
                //    Console.WriteLine($"HttpStatusCode: {response.HttpStatusCode}");
                //    Console.WriteLine($"Number of Keys: {response.KeyCount}");
                //    foreach (var entry in response.S3Objects)
                //    {
                //        Console.WriteLine($"Key = {entry.Key} Size = {entry.Size}");
                //    }
                //}

                //try
                //{
                //    var request = new ListObjectsV2Request
                //    {
                //        BucketName = bucketName,
                //        MaxKeys = 5,
                //    };

                //    Console.WriteLine("--------------------------------------");
                //    Console.WriteLine($"Listing the contents of {bucketName}:");
                //    Console.WriteLine("--------------------------------------");

                //    ListObjectsV2Response response;

                //    do
                //    {
                //        response = await client.ListObjectsV2Async(request);

                //        response.S3Objects
                //            .ForEach(obj => Console.WriteLine($"{obj.Key,-35}{obj.LastModified.ToShortDateString(),10}{obj.Size,10}"));

                //        // If the response is truncated, set the request ContinuationToken
                //        // from the NextContinuationToken property of the response.
                //        request.ContinuationToken = response.NextContinuationToken;
                //    }
                //    while (response.IsTruncated);

                //    //return true;
                //}
                //catch (AmazonS3Exception ex)
                //{
                //    Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' getting list of objects.");
                //   // return false;
                //}
            }


            return fileNames;// new string[] { "value1", "value2" };
        }

        // GET api/<BucketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BucketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BucketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BucketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
