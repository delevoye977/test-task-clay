using ClayDoorsDatabase.Entities;
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClayDoorsDatabase.Repositories
{
    public abstract class GenericCRUDRepository<ModelType, IdType>
        : ICRUDRepository<ModelType, IdType>
        where ModelType : IModel<IdType>
    {
        protected readonly Func<DbSet<AbstractEntity<ModelType, IdType>>> GetDbSet;
        protected readonly Func<Task<int>> SaveAsync;

        protected GenericCRUDRepository(Func<DbSet<AbstractEntity<ModelType, IdType>>> getDbSetFunc, Func<Task<int>> saveAsyncFunc)
        {
            this.GetDbSet = getDbSetFunc;
            this.SaveAsync = saveAsyncFunc;
        }

        public async Task<ModelType?> Get(IdType? id)
        {
            var entity = await ExecuteGetQuery(id);
            return MapToModel(entity);
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

        protected abstract ModelType? MapToModel(AbstractEntity<ModelType, IdType>? entity);

    }
}
