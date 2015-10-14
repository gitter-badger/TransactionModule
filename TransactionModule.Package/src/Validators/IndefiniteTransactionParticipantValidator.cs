using System;
using TransactionModule.Constants;
using TransactionModule.Enums;
using TransactionModule.Interfaces;
using TransactionModule.Validators.Context;
using TransactionModule.Validators.Interfaces;

namespace TransactionModule.Validators
{
    public class IndefiniteTransactionParticipantValidator<TTransaction>: IIndefiniteTransactionParticipantValidator
        where TTransaction : class, ITransaction
    {
        private readonly IAppTransactionParticipantValidator _appTransactionParticipantValidator;
        private readonly ITransactionParticipantValidator<TTransaction> _transactionParticipantValidator;

        public IndefiniteTransactionParticipantValidator(IAppTransactionParticipantValidator appTransactionParticipantValidator, ITransactionParticipantValidator<TTransaction> transactionParticipantValidator)
        {
            _appTransactionParticipantValidator = appTransactionParticipantValidator;
            _transactionParticipantValidator = transactionParticipantValidator;
        }

        public bool IsValid(IndefiniteTransactionParticipantValidatorContext context)
        {
            bool result;

            switch (context.Type)
            {
                case TransactionParticipantType.App:
                    result = _appTransactionParticipantValidator.IsValid(context);
                    break;

                default:
                    result = _transactionParticipantValidator.IsValid(context);
                    break;
            }

            return result;
        }
    }
}