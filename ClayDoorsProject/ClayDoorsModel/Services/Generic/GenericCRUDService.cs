
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsModel.Services.Generic
{
    public abstract class GenericCRUDService<ModelType, IdType> 
        : ICRUDService<ModelType, IdType>
        where ModelType : IModel<IdType>
    {
        protected ICRUDRepository<ModelType, IdType> CrudRepository { get; }
        
        protected GenericCRUDService(ICRUDRepository<ModelType, IdType> repository) 
        {
            this.CrudRepository = repository;
        }


        public async Task<IEnumerable<ModelType>> GetAll()
        {
            return await this.CrudRepository.GetAll();
        }

        public async Task<ModelType?> Get(IdType? id)
        {
            if (id == null) return default;

            return await CrudRepository.Get(id);
        }

        public async Task<ModelType?> Update(ModelType model)
        {
            if (model == null)
                return default;

            var gotModel = await this.CrudRepository.Get(model.Id);
            if (gotModel == null)
                return default;

            return await this.CrudRepository.Update(model);
        }

        public async Task<bool> Delete(IdType id)
        {
            if (id == null)
                return default;

            var gotModel = await this.CrudRepository.Get(id);
            if (gotModel == null)
                return default;

            return await CrudRepository.Delete(id);
        }
    }
}
