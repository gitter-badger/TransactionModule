using TransactionModule.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;
using TransactionModule.TransactionStateAppliers.Interfaces;

namespace TransactionModule.TransactionStateAppliers
{
    public class CancelTransactionStateApplier<TTransaction>: ICancelTransactionStateApplier<TTransaction>
        where TTransaction : ITransaction
    {
        public void Apply(DefiniteTransactionStateApplierContext<TTransaction> context)
        {
            
        }
    }
}