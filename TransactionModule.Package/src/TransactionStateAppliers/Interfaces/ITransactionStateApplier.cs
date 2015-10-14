namespace TransactionModule.TransactionStateAppliers.Interfaces
{
    public interface ITransactionStateApplier<TContext>
    {
        void Apply(TContext context);
    }
}