using DataTransfertObject;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class DiscountManager
    {
        public int SetTheDiscountId(Invoice ticket)
        {
            int value = 0;
            foreach (var producLine in ticket.ProductLines)
            {
                if (producLine.Discounts.Count == 0)
                    value = 0;
                else
                {
                    value = producLine.Discounts.Last().DiscountJoinId + 1;
                }
            }
            return value;
        }

        public Discount SetADiscount(Invoice ticket, ObservableCollection<Discount> totalDiscountsList)
        {
            Discount discount = null;
            foreach (var producLine in ticket.ProductLines)
            {
                foreach (var discountToAdd in producLine.Discounts)
                {
                    discount = discountToAdd;
                    totalDiscountsList.Add(discount);
                }
            }
            return discount;
        }
    }
}
