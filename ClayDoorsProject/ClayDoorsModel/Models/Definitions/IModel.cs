
namespace ClayDoorsModel.Models.Definitions
{
    public interface IModel<IdType>
    {
        IdType? Id { get; }
    }

    public abstract class GenericModel<IdType> : IModel<IdType>
    {
        public IdType? Id { get; set; }
    }
}
