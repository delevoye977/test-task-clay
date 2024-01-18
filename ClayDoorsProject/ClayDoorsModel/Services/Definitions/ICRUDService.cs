
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    /// <summary>
    /// Describes a default Create, Read, Update, Delete service.
    /// </summary>
    /// <typeparam name="ModelType">Type of the model.</typeparam>
    /// <typeparam name="IdType">Type of the Id.</typeparam>
    public interface ICRUDService<ModelType, IdType> 
        where ModelType : IModel<IdType>
    {
        /// <summary>
        /// Gets all Models.
        /// </summary>
        /// <returns>An enumerable of all models, never null.</returns>
        public Task<IEnumerable<ModelType>> GetAll();

        /// <summary>
        /// Gets the Model corresponding to <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of the Model to retrieve.</param>
        /// <returns>The object found, null otherwise.</returns>
        public Task<ModelType?> Get(IdType? id);

        /// <summary>
        /// Updates the Model to correspond to the given <paramref name="model"/>.
        /// </summary>
        /// <param name="model">How the updated model should be.</param>
        /// <returns>The updated model.</returns>
        public Task<ModelType?> Update(ModelType model);

        /// <summary>
        /// Removes the Model with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of the model to delete.</param>
        /// <returns>True if the model was deleted, false otherwise.</returns>
        public Task<bool> Delete(IdType id);
    }
}
