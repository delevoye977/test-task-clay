using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsModel.Services
{
    public class DoorUserService : IDoorUserService
    {
        private readonly IDoorUsersRepository doorUserRepository;
        public DoorUserService(IDoorUsersRepository doorUserRepository) 
        {
            this.doorUserRepository = doorUserRepository;
        }

        public IDoorUser? GetUser(string username)
        {
            return doorUserRepository.GetUser(username);
        }
    }
}
