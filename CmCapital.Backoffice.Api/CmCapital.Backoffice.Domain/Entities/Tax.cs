namespace CmCapital.Backoffice.Domain.Entities;

public sealed class Tax 
{
    public Guid Id { get; set; }
    public decimal InitialValue { get; set; }
    public decimal FinalValue { get; set; }
    public decimal Percentage { get; set; }
    public DateTime CreateAt { get; set; }
}