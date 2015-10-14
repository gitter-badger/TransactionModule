namespace TransactionModule.Factories.FactoryMethods.Interfaces
{
    public interface ITransactionModuleFactoryMethod<out T>
    {
        T Create();
    }

    public interface ITransactionModuleFactoryMethod<out TObject, in TContext>
    {
        TObject Create(TContext context);
    }
}