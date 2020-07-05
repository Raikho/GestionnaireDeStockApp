namespace DataTransfertObject.DataGridView
{
    public class InvoiceView
    {
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double TotalDiscount { get; set; }
        public double FinalTotalPrice { get; set; }
    }
}
