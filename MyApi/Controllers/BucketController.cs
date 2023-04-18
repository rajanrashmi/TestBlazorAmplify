using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Amazon;
using System.Net.Sockets;
using Data;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private readonly ILogger<BucketController> _logger;

        public BucketController(ILogger<BucketController> logger)
        {
            _logger = logger;
        }
        // GET: api/<BucketController>
        [HttpGet]
        public async Task<IEnumerable<BucketObject>> Get()
        {
            BucketData bucketData = new BucketData(_logger);
            return await bucketData.GetBucketObjects();
        }

        [HttpGet("rajan")]
        public async Task<IEnumerable<BucketObject>> GetFake()
        {
            BucketData bucketData = new BucketData(_logger);
            return await bucketData.GetBucketObjectsFake();
        }

        [HttpGet("content")]
        public async Task<IActionResult>  GetS3ObjectContent(string key)
        {
            BucketData bucketData = new BucketData(_logger);
            var stream = await bucketData.GetBucketObjectContent(key);
            if (stream == null)
                return NotFound(); 

           // StreamReader reader = new StreamReader(stream);// for testing
           // string text = reader.ReadToEnd();

            var result =File(stream, "application/octet-stream", key);
            return result;
        }


        // GET api/<BucketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "some value";
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
