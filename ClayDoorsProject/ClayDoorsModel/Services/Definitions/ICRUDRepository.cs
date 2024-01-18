using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    public interface ICRUDRepository<ModelType, IdType> 
        : ICRUDService<ModelType, IdType>
        where ModelType : IModel<IdType>
    {
    }
}
