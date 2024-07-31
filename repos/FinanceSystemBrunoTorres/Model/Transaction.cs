namespace FinanceSystemBrunoTorres.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime? Date { get; set; }
    }
}
