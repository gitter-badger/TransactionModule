using TransactionModule.Interfaces;

namespace TransactionModule.TransactionStateAppliers.Context
{
    public class DefiniteTransactionStateApplierContext<TTransaction>
        where TTransaction: ITransaction
    {
        public DefiniteTransactionStateApplierContext(TTransaction transaction, int stateModifier)
        {
            Transaction = transaction;
            StateModifier = stateModifier;
        }
        
        public TTransaction Transaction { get; set; }

        public int StateModifier { get; set; }
    }
}