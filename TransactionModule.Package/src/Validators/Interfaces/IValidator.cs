namespace TransactionModule.Validators.Interfaces
{
    public interface IValidator<in TContext>
    {
        bool IsValid(TContext context);
    }
}