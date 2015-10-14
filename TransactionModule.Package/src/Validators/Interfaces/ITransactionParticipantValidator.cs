using TransactionModule.Interfaces;

namespace TransactionModule.Validators.Interfaces
{
    public interface ITransactionParticipantValidator<TTransaction>: IIndefiniteTransactionParticipantValidator
        where TTransaction: class, ITransaction
    {
         
    }
}