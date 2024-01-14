using ClayDoorsProject.Models;
using ClayDoorsProject.Repositories;

namespace ClayDoorsProject.Services
{
    public class DoorsReadService : IDoorsReadService
    {
        private readonly IDoorsRepository doorsRepository;

        public DoorsReadService(IDoorsRepository doorsRepository) 
        {
            this.doorsRepository = doorsRepository;
        }

        public async Task<IEnumerable<Door>> GetDoors()
        {
            return await doorsRepository.GetAllDoors();
        }
    }
}
