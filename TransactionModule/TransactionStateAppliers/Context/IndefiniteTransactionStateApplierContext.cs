using TransactionModule.Interfaces;

namespace TransactionModule.TransactionStateAppliers.Context
{
    public class IndefiniteTransactionStateApplierContext<TTransaction>: DefiniteTransactionStateApplierContext<TTransaction>
        where TTransaction : ITransaction
    {
        public IndefiniteTransactionStateApplierContext(TTransaction transaction, int stateModifier, int state)
            : base(transaction, stateModifier)
        {
            State = state;
        }

        public int State { get; set; }
    }
}