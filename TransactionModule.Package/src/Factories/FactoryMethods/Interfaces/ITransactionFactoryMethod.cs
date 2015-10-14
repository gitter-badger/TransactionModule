namespace TransactionModule.Factories.FactoryMethods.Interfaces
{
    public interface ITransactionFactoryMethod<out TTransaction> : ITransactionModuleFactoryMethod<TTransaction, object>
    {
         
    }
}