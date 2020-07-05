using DataTransfertObject.DataGridView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class InvoiceViewManager
    {
        public int SetTheInvoiceViewId(ObservableCollection<InvoiceView> invoiceViewsList)
        {
            int value;
            if (invoiceViewsList.Count == 0)
                value = 0;
            else
            {
                value = invoiceViewsList.Last().InvoiceId + 1;
            }

            return value;
        }

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
