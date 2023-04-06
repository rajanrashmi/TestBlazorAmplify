using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Amazon;
using System.Net.Sockets;
using Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        // GET: api/<BucketController>
        [HttpGet]
        public async Task<IEnumerable<BucketObject>> Get()
        {
            BucketData bucketData = new BucketData();
            return await bucketData.GetBucketObjects();
        }

        [HttpGet("rajan")]
        public async Task<IEnumerable<BucketObject>> GetFake()
        {
            BucketData bucketData = new BucketData();
            return await bucketData.GetBucketObjectsFake();
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
