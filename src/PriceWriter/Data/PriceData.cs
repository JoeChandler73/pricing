namespace PriceWriter.Data;

public class PriceData
{
    public int Id { get; set; }
    
    public string Symbol { get; set; }
    
    public decimal Mid { get; set; }
    
    public DateTime Timestamp { get; set; }
}