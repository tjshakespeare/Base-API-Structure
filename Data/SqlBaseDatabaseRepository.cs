using System;
using System.Collections.Generic;
using System.Linq;
using Base_API_Structure.Models;

namespace Base_API_Structure.Data
{
    public class SqlBaseDatabaseRepository : IDatabaseRepository
    {
        public readonly BaseDatabaseContext _dbContext;
        public SqlBaseDatabaseRepository(BaseDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Create(BaseModel baseModel)
        {
            if(baseModel == null) throw new ArgumentNullException(nameof(baseModel));
            _dbContext.Add(baseModel);
        }

        public void Delete(BaseModel baseModel)
        {
            if(baseModel == null) throw new ArgumentNullException(nameof(baseModel));
            _dbContext.Remove(baseModel);
        }

        public IEnumerable<BaseModel> GetAll()
        {
            return _dbContext.BaseModels.ToList();
        }

        public BaseModel GetById(int id)
        {
            return _dbContext.BaseModels.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public void Update(BaseModel baseModel)
        {
            // Implementation not required, handled by DB Context and Entity Framework with SQL. Still kept and called incase we use a different database type where coding is required in the implementation class
        }
    }
}