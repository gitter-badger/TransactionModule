using TransactionModule.Interfaces;
using TransactionModule.Strategies.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;
using TransactionModule.TransactionStateAppliers.Interfaces;

namespace TransactionModule.TransactionStateAppliers
{
    public class HoldTransactionStateApplier<TTransaction>: IHoldTransactionStateApplier<TTransaction> 
        where TTransaction : ITransaction
    {
        private readonly IOperateWalletStrategy<TTransaction> _operateWalletStrategy;

        public HoldTransactionStateApplier(IOperateWalletStrategy<TTransaction> operateWalletStrategy)
        {
            _operateWalletStrategy = operateWalletStrategy;
        }

        public void Apply(DefiniteTransactionStateApplierContext<TTransaction> context)
        {
            _operateWalletStrategy.Operate(context.Transaction.SenderType, context.Transaction.SenderId, -context.Transaction.Amount * context.StateModifier);
        }
    }
}