using TransactionModule.Enums;
using TransactionModule.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;
using TransactionModule.TransactionStateAppliers.Interfaces;

namespace TransactionModule.TransactionStateAppliers
{
    public class IndefiniteTransactionStateApplier<TTransaction>: IIndefiniteTransactionStateApplier<TTransaction>
        where TTransaction : ITransaction
    {
        private readonly IProcessTransactionStateApplier<TTransaction> _processTransactionStateApplier;
        private readonly IHoldTransactionStateApplier<TTransaction> _holdTransactionStateApplier;
        private readonly ICompleteTransactionStateApplier<TTransaction> _completeTransactionStateApplier;
        private readonly ICancelTransactionStateApplier<TTransaction> _cancelTransactionStateApplier;

        public IndefiniteTransactionStateApplier(IProcessTransactionStateApplier<TTransaction> processTransactionStateApplier, IHoldTransactionStateApplier<TTransaction> holdTransactionStateApplier, ICompleteTransactionStateApplier<TTransaction> completeTransactionStateApplier, ICancelTransactionStateApplier<TTransaction> cancelTransactionStateApplier)
        {
            _processTransactionStateApplier = processTransactionStateApplier;
            _holdTransactionStateApplier = holdTransactionStateApplier;
            _completeTransactionStateApplier = completeTransactionStateApplier;
            _cancelTransactionStateApplier = cancelTransactionStateApplier;
        }

        public void Apply(IndefiniteTransactionStateApplierContext<TTransaction> context)
        {
            if (context.State == 0)
            {
                return;
            }

            switch ((TransactionState) context.State)
            {
                case TransactionState.Processing:
                    _processTransactionStateApplier.Apply(context);
                    break;

                case TransactionState.Hold:
                    _holdTransactionStateApplier.Apply(context);
                    break;

                case TransactionState.Completed:
                    _completeTransactionStateApplier.Apply(context);
                    break;

                case TransactionState.Canceled:
                    _cancelTransactionStateApplier.Apply(context);
                    break;

                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}