using ClayDoorsDatabase.Entities;
using ClayDoorsModel.Models.Definitions;
using Microsoft.EntityFrameworkCore;

namespace ClayDoorsDatabase.Repositories
{
    public interface IDatabaseContext<EntitiesType, ModelType, IdType>
        where EntitiesType : AbstractEntity<ModelType, IdType>
        where ModelType : IModel<IdType>
    {
        DbSet<EntitiesType> DbSet { get; }

        Task<int> SaveAsync();
    }
}
