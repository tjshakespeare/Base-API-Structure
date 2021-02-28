using System.Collections.Generic;
using Base_API_Structure.Models;

namespace Base_API_Structure.Data
{
    // A Mock implementation of the database repository to quickly join components together without worrying about the backend data source initially
    public class MockDatabaseRepository : IDatabaseRepository
    {
        public void Create(BaseModel baseModel)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(BaseModel baseModel)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BaseModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public BaseModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Update(BaseModel baseModel)
        {
            throw new System.NotImplementedException();
        }
    }
}