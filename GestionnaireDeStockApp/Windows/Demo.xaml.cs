using DataLayer;
using DataTransfertObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GestionnaireDeStockApp.Windows
{
    /// <summary>
    /// Logique d'interaction pour Demo.xaml
    /// </summary>
    public partial class Demo : Window
    {
        private ObservableCollection<ProductLine> _invoiceLines;
        private Invoice _invoice;

        public Demo()
        {
            InitializeComponent();

            LoadFirstInvoice();
            DataContext = _invoice;
        }

        private void LoadFirstInvoice()
        {
            using (var dbCtx = new StockContext())
            {
                _invoice = dbCtx.Invoices.Include("ProductLines.Product").Single(c => c.InvoiceId == 1);
                _invoiceLines = new ObservableCollection<ProductLine>(_invoice.ProductLines);
            }
        }
    }
}
