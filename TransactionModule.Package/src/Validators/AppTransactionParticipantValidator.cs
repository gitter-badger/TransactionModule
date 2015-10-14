
using System.Linq;
using TransactionModule.Validators.Context;
using TransactionModule.Validators.Interfaces;

namespace TransactionModule.Validators
{
    public class AppTransactionParticipantValidator: IAppTransactionParticipantValidator
    {
        public bool IsValid(DefiniteTransactionParticipantValidatorContext context)
        {
            return context.Id == null || !context.Id.Any();
        }
    }
}