namespace TransactionModule.Strategies.Interfaces
{
    public interface ICompleteTransactionCallback<TTransaction>
    {
        void Call(TTransaction transaction, bool isCompleted);
    }
}