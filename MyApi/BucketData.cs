using Amazon.S3.Model;
using Amazon.S3;
using Data;
using System.Text.Json;
using Amazon;

namespace MyApi
{
    public interface IBucketData
    {
        Task<BucketObject> AddBucketObject(BucketObject product);
        Task<bool> DeleteBucketObject(int id);
        Task<IEnumerable<BucketObject>> GetBucketObjects();
        Task<BucketObject> UpdateBucketObject(BucketObject product);
    }

    public class BucketData : IBucketData
    {
        public BucketData()
        {
        
        }
        public Task<BucketObject> AddBucketObject(BucketObject product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBucketObject(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BucketObject>> GetBucketObjects()
        {
            List<BucketObject> bucketObjects = new List<BucketObject>();
            try
            {
                string bucketName = "rajan-test-bucket-2";

                // Create a client
                AmazonS3Client client = new AmazonS3Client(RegionEndpoint.USEast1);
                Console.WriteLine("pass1");



                // Issue call
                ListBucketsResponse myResponse = await client.ListBucketsAsync();
                Console.WriteLine("pass2");


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
                        Console.WriteLine("pass3");

                        var response = await client.ListObjectsV2Async(req);

                        Console.WriteLine("pass4");


                        response.S3Objects.ForEach(obj => bucketObjects.Add(new BucketObject()
                        {

                            FileName = obj.Key,
                            Id = obj.ETag

                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' getting list of objects.");

                    }


                }
            }
            catch (Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    BucketObject bucketObject = new BucketObject();
                    bucketObject.FileName = $"File{i + 1}.xml";
                    bucketObject.Id = (i + 1).ToString();
                    bucketObjects.Add(bucketObject);

                }
                await Task.Delay(10);

            }

            return bucketObjects;
        }

        public async Task<IEnumerable<BucketObject>> GetBucketObjectsFake()
        {
            //var options = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //};
            //var uri = "https://u6xz8l7ivk.execute-api.us-east-1.amazonaws.com/Prod/api/bucket";
            //List <BucketObject> bucketObjects = new List<BucketObject>();
            //var result = await new HttpClient().GetStringAsync(uri);

            //bucketObjects =  JsonSerializer.Deserialize<List<BucketObject>>(result,options);
            //return bucketObjects.AsEnumerable();
            //return Task.FromResult(bucketObjects.AsEnumerable());


            string bucketName = "rajan-test-bucket-2";
            List<BucketObject> bucketObjects = new List<BucketObject>();

            for (int i = 0; i < 5; i++)
            {
                BucketObject bucketObject = new BucketObject();
                bucketObject.FileName = $"File{i + 1}.xml";
                bucketObject.Id = (i + 1).ToString();
                bucketObjects.Add(bucketObject);

            }
            await Task.Delay(10);
            return bucketObjects;



            //// Create a client
            //AmazonS3Client client = new AmazonS3Client(RegionEndpoint.USEast1);
            //Console.WriteLine("pass1");



            //// Issue call
            //ListBucketsResponse myResponse = await client.ListBucketsAsync();
            //Console.WriteLine("pass2");


            //// View response data
            //Console.WriteLine("Buckets owner - {0}", myResponse.Owner.DisplayName);
            //foreach (S3Bucket bucket in myResponse.Buckets)
            //{
            //    Console.WriteLine("Bucket {0}, Created on {1}", bucket.BucketName, bucket.CreationDate);
            //}

            //var foundBucket = myResponse.Buckets.Select(x => x.BucketName == bucketName);
            //if (foundBucket.Any())
            //{
            //    var req = new ListObjectsV2Request()
            //    {
            //        BucketName = bucketName,
            //        Prefix = "File",

            //    };
            //    int a = 0;

            //    try
            //    {
            //        Console.WriteLine("pass3");

            //        var bucketObjects = await client.ListObjectsV2Async(req);

            //        Console.WriteLine("pass4");


            //        bucketObjects.S3Objects.ForEach(obj => fileNames.Add(new BucketObject() {

            //            FileName = obj.Key,
            //            Id = obj.ETag
                    
            //        }));
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' getting list of objects.");

            //    }

                
            //}


            //return fileNames;
        }

        public Task<BucketObject> UpdateBucketObject(BucketObject product)
        {
            throw new NotImplementedException();
        }
    }
}
