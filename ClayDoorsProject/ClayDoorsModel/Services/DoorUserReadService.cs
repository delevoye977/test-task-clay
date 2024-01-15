using ClayDoorsModel.Models;

namespace ClayDoorsModel.Services
{
    public class DoorUserReadService : IDoorUserReadService
    {
        private readonly IDoorUsersRepository doorUserRepository;
        public DoorUserReadService(IDoorUsersRepository doorUserRepository) 
        {
            this.doorUserRepository = doorUserRepository;
        }

        public IDoorUser? GetUser(string username)
        {
            return doorUserRepository.GetUser(username);
        }
    }
}
