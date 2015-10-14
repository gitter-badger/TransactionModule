using TransactionModule.Interfaces;
using TransactionModule.Validators.Context;

namespace TransactionModule.Validators.Interfaces
{
    public interface ITransactionSubjectValidator<TTransaction>: IValidator<TransactionSubjectValidatorContext>
        where TTransaction: class, ITransaction
    {
         
    }
}