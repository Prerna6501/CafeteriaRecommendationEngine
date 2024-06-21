namespace ServerSide.Entity
{
    public class MenuItemType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MenuItem> Items { get; set; }
    }
}
