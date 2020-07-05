using DataTransfertObject;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class ProductLineManager
    {
        public int SetTheProductLineId(Invoice ticket)
        {
            int value;
            if (ticket.ProductLines.Count == 0)
                value = 0;
            else
            {
                value = ticket.ProductLines.Last().ProductLineId + 1;
            }
            return value;
        }

        public ProductLine SetAProductLine(Invoice ticket, ObservableCollection<ProductLine> productLinesList)
        {
            ProductLine productLine = null;
            foreach (var productLineToSearch in ticket.ProductLines)
            {
                productLine = productLineToSearch;
                productLinesList.Add(productLine);
            }
            return productLine;
        }
    }
}
