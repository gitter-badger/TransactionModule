using System.Linq;
using TransactionModule.Interfaces;
using TransactionModule.Validators.Context;
using TransactionModule.Validators.Interfaces;

namespace TransactionModule.Validators
{
    public class AnonymousTransactionSubjectValidator<TTransaction> : IAnonymousTransactionParticipantValidator<TTransaction> 
        where TTransaction : class, ITransaction
    {
        public bool IsValid(DefiniteTransactionParticipantValidatorContext context)
        {
            return context.Id == null || !context.Id.Any();
        }
    }
}