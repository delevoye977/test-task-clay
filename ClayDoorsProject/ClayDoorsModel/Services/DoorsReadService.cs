using ClayDoorsModel.Models;

namespace ClayDoorsModel.Services
{
    public class DoorsReadService : IDoorsReadService
    {
        private readonly IDoorsRepository doorsRepository;

        public DoorsReadService(IDoorsRepository doorsRepository) 
        {
            this.doorsRepository = doorsRepository;
        }
        
        public async Task<IEnumerable<IDoor>> GetDoors() //Wishing covariant return types makes it to C# soon
        {
            return await doorsRepository.GetAllDoors();
        }
    }
}
