namespace ClayDoorsModel.Models
{
    public interface IDoor
    {
        public int Id { get; }

        public string Location { get; set; }

        public string Description { get; set; }
    }
}
