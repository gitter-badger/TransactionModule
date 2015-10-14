using TransactionModule.Interfaces;

namespace TransactionModule.Validators.Interfaces
{
    public interface IAnonymousTransactionParticipantValidator<TTransaction>: IDefiniteTransactionParticipantValidator
        where TTransaction: class, ITransaction
    {
         
    }
}