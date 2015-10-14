using TransactionModule.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;

namespace TransactionModule.TransactionStateAppliers.Interfaces
{
    public interface IIndefiniteTransactionStateApplier<TTransaction>: ITransactionStateApplier<IndefiniteTransactionStateApplierContext<TTransaction>>
        where TTransaction : ITransaction
    {
         
    }
}