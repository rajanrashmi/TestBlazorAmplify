using Data;

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
        public Task<BucketObject> AddBucketObject(BucketObject product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBucketObject(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BucketObject>> GetBucketObjects()
        {
            throw new NotImplementedException();
        }

        public Task<BucketObject> UpdateBucketObject(BucketObject product)
        {
            throw new NotImplementedException();
        }
    }
}
