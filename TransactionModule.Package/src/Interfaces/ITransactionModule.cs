using System;
using TransactionModule.Enums;
using TransactionModule.Exceptions;

namespace TransactionModule.Interfaces
{
    public interface ITransactionModule<out TTransaction>
        where TTransaction: class, ITransaction
    {
        /// <exception cref="ArgumentException"></exception>
        TTransaction Create(int senderType, string senderId, int receiverType, string receiverId, int subjectType, string subject, double amount, object transactionFactoryMethodContext = null);

        /// <exception cref="TransactionSubjectIsNotValidException"></exception>>
        /// <exception cref="WithdrawalException"></exception>
        void ChangeState(Guid id, TransactionState state);
    }
}