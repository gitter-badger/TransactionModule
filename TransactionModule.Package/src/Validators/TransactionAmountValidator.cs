using TransactionModule.Validators.Interfaces;

namespace TransactionModule.Validators
{
    public class TransactionAmountValidator: ITransactionAmountValidator
    {
        public bool IsValid(double amount)
        {
            return amount > 0;
        }
    }
}