namespace ClayDoorsModel.Models
{
    public class DoorUser : IDoorUser
    {
        public int Id { get; }

        public string Username { get; }

        public DoorUser(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
