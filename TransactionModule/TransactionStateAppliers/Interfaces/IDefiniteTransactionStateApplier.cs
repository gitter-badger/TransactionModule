using TransactionModule.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;

namespace TransactionModule.TransactionStateAppliers.Interfaces
{
    public interface IDefiniteTransactionStateApplier<TTransaction>: ITransactionStateApplier<DefiniteTransactionStateApplierContext<TTransaction>>
        where TTransaction : ITransaction
    {
         
    }
}