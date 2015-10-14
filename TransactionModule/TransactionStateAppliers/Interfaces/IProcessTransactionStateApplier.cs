using TransactionModule.Interfaces;

namespace TransactionModule.TransactionStateAppliers.Interfaces
{
    public interface IProcessTransactionStateApplier<TTransaction>: IDefiniteTransactionStateApplier<TTransaction>
        where TTransaction : ITransaction
    {
         
    }
}