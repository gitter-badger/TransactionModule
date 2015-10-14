using TransactionModule.Interfaces;

namespace TransactionModule.TransactionStateAppliers.Interfaces
{
    public interface ICancelTransactionStateApplier<TTransaction>: IDefiniteTransactionStateApplier<TTransaction>
        where TTransaction : ITransaction
    {
         
    }
}