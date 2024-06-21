namespace Common.Models
{
    public class MenuItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string MenuItemType { get; set; }
        public string Sentiments { get; set; }
        public decimal AverageRating { get; set; }
    }
}
