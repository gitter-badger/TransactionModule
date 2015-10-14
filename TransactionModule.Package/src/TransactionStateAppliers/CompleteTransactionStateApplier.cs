using TransactionModule.Interfaces;
using TransactionModule.Strategies.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;
using TransactionModule.TransactionStateAppliers.Interfaces;

namespace TransactionModule.TransactionStateAppliers
{
    public class CompleteTransactionStateApplier<TTransaction>: ICompleteTransactionStateApplier<TTransaction> 
        where TTransaction : ITransaction
    {
        private readonly IOperateWalletStrategy<TTransaction> _operateWalletStrategy;
        private readonly ICompleteTransactionCallback<TTransaction> _completeTransactionCallback; 

        public CompleteTransactionStateApplier(IOperateWalletStrategy<TTransaction> operateWalletStrategy, ICompleteTransactionCallback<TTransaction> completeTransactionCallback)
        {
            _operateWalletStrategy = operateWalletStrategy;
            _completeTransactionCallback = completeTransactionCallback;
        }

        public void Apply(DefiniteTransactionStateApplierContext<TTransaction> context)
        {
            _operateWalletStrategy.Operate(context.Transaction.SenderType, context.Transaction.SenderId, -context.Transaction.Amount * context.StateModifier);
            _operateWalletStrategy.Operate(context.Transaction.ReceiverType, context.Transaction.ReceiverId, context.Transaction.Amount * context.StateModifier);
            _completeTransactionCallback.Call(context.Transaction, context.StateModifier > 0);
        }
    }
}