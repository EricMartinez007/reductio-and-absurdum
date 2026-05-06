public class Product
{
    public int Id { get; set;}
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime DateAdded { get; set; }
    public int ProductTypeId { get; set; }
    public int DaysOnShelf
    {
        get
        {
            return (DateTime.Now - DateAdded).Days;
        }
    }
}