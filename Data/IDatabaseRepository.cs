using System.Collections.Generic;
using Base_API_Structure.Models;

namespace Base_API_Structure.Data
{
    public interface IDatabaseRepository 
    {
        bool SaveChanges();
        void Create(BaseModel baseModel);
        void Update(BaseModel baseModel);
        void Delete(BaseModel baseModel);

        IEnumerable<BaseModel> GetAll();

        BaseModel GetById(int id);


    }
}