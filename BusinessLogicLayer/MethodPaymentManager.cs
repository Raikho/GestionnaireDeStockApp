using DataLayer;
using DataTransfertObject;

namespace BusinessLogicLayer
{
    public class MethodPaymentManager
    {
        CashRegisterManager CashRegisterManager = new CashRegisterManager();

        public MethodPayment SavePaymentMethod()
        {
            using (var dbContext = new StockContext())
            {
                var methodPayments = dbContext.MethodPayments;

                var newMethodPayment = new MethodPayment()
                {
                    CB = CashRegisterManager._methodPayment.CB,
                    Money = CashRegisterManager._methodPayment.Money,
                    Cheque = CashRegisterManager._methodPayment.Cheque
                };
                methodPayments.Add(newMethodPayment);
                dbContext.SaveChanges();
                return newMethodPayment;
            }
        }
    }
}
