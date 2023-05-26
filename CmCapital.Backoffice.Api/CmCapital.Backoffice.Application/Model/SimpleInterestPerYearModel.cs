namespace CmCapital.Backoffice.Application.Model
{
    public sealed class SimpleInterestPerYearModel
    {
        public Guid ProductId { get; set; }
        public string Amount { get; set; } = default!;
        public string Percentage { get; set; } = default!;
        public string Honorarium { get; set; } = default!;
    }
}