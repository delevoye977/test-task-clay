using ClayDoorsDatabase.Entities;
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClayDoorsDatabase.Repositories
{
    public abstract class GenericCRUDRepository<ModelType, IdType>
        : ICRUDRepository<ModelType, IdType>
        where ModelType : GenericModel<IdType>
    {
        protected IDatabaseContext<AbstractEntity<ModelType, IdType>, ModelType, IdType> Context { get; set; }

        public GenericCRUDRepository(IDatabaseContext<AbstractEntity<ModelType, IdType>, ModelType, IdType> ctx)
        {
            this.Context = ctx;
        }

        public async Task<ModelType?> Get(IdType? id)
        {
            var entity = await ExecuteGetQuery(id);
            return entity?.MapToModel();
        }

        protected async Task<AbstractEntity<ModelType, IdType>?> ExecuteGetQuery(IdType? id)
        {
            return await Context.DbSet.FirstOrDefaultAsync(x => x.Id != null && x.Id.Equals(id));
        }

        public async Task<IEnumerable<ModelType>> GetAll()
        {
            var all = await ExecuteGetAllQuery();
            return all.Select(x => x.MapToModel()).ToList();
        }

        protected async Task<List<AbstractEntity<ModelType, IdType>>> ExecuteGetAllQuery()
        {
            return await Context.DbSet.ToListAsync();
        }

        public async Task<ModelType?> Update(ModelType model)
        {
            if (model.Id == null) return model;
            if (Context.DbSet.FirstOrDefaultAsync() == null) return model;

            var entity = ExecuteUpdateQuery(model);
            await Context.SaveAsync();
            return entity.Entity.MapToModel();
        }

        public async Task<bool> Delete(IdType id)
        {
            if (id == null) return false;
            var entity = await Context.DbSet.FirstOrDefaultAsync();
            if (entity == null) return false;

            Context.DbSet.Remove(entity);
            await Context.SaveAsync();
            return true;
        }

        protected EntityEntry<AbstractEntity<ModelType, IdType>> ExecuteUpdateQuery(ModelType model)
        {
            return Context.DbSet.Update(MapToEntity(model));
        }

        protected abstract AbstractEntity<ModelType, IdType> MapToEntity(ModelType model);
    }
}
