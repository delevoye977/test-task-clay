using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsDatabase.Entities
{
    public abstract class AbstractEntity<ModelType, IdType>
        where ModelType : IModel<IdType>
    {
        public IdType? Id { get; set; }

        public abstract ModelType MapToModel();
    }
}
