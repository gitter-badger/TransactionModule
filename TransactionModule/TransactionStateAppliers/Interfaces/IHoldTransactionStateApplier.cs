using TransactionModule.Interfaces;

namespace TransactionModule.TransactionStateAppliers.Interfaces
{
    public interface IHoldTransactionStateApplier<TTransaction>: IDefiniteTransactionStateApplier<TTransaction>
        where TTransaction : ITransaction
    {
         
    }
}