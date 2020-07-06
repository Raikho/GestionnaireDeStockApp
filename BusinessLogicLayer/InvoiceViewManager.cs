using DataTransfertObject.DataGridView;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer
{
    public class InvoiceViewManager
    {
        public InvoiceView SetAProductLine(IEnumerable<InvoiceView> join, ObservableCollection<InvoiceView> invoiceViewsList)
        {
            InvoiceView invoiceView = null;
            foreach (var invoiceViewToSearch in join)
            {
                invoiceView = invoiceViewToSearch;
                invoiceViewsList.Add(invoiceView);
            }
            return invoiceView;
        }
    }
}
