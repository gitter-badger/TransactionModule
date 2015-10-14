using System;
using System.Collections.Generic;
using System.Linq;
using TransactionModule.Enums;
using TransactionModule.Factories.FactoryMethods.Interfaces;
using TransactionModule.Interfaces;
using TransactionModule.Repositories.Interfaces;
using TransactionModule.Strategies.Interfaces;
using TransactionModule.TransactionStateAppliers.Context;
using TransactionModule.TransactionStateAppliers.Interfaces;
using TransactionModule.Validators.Context;
using TransactionModule.Validators.Interfaces;

namespace TransactionModule
{
    public class TransactionModule<TTransaction> : ITransactionModule<TTransaction>
        where TTransaction : class, ITransaction, new()
    {
        private readonly ITransactionFactoryMethod<TTransaction> _transactionFactoryMethod; 
        private readonly ITransactionRepository<TTransaction> _transactionRepository;
        private readonly IIndefiniteTransactionParticipantValidator _indefiniteTransactionSubjectValidator;
        private readonly ITransactionAmountValidator _transactionAmountValidator;
        private readonly IIndefiniteTransactionStateApplier<TTransaction> _indefiniteTransactionStateApplier;
        private readonly ITransactionSubjectValidator<TTransaction> _transactionSubjectValidator;

        // ReSharper disable once StaticMemberInGenericType
        private static readonly Dictionary<Type, object> Lockers;

        static TransactionModule()
        {
            Lockers = new Dictionary<Type, object>();
        }

        public TransactionModule(ITransactionRepository<TTransaction> transactionRepository, IIndefiniteTransactionParticipantValidator indefiniteTransactionSubjectValidator, ITransactionAmountValidator transactionAmountValidator, IIndefiniteTransactionStateApplier<TTransaction> indefiniteTransactionStateApplier, ITransactionFactoryMethod<TTransaction> transactionFactoryMethod, ITransactionSubjectValidator<TTransaction> transactionSubjectValidator)
        {
            _transactionRepository = transactionRepository;
            _indefiniteTransactionSubjectValidator = indefiniteTransactionSubjectValidator;
            _transactionAmountValidator = transactionAmountValidator;
            _indefiniteTransactionStateApplier = indefiniteTransactionStateApplier;
            _transactionFactoryMethod = transactionFactoryMethod;
            _transactionSubjectValidator = transactionSubjectValidator;

            if (!Lockers.ContainsKey(typeof (TTransaction)))
            {
                Lockers.Add(typeof(TTransaction), new object());
            }
        }

        public virtual TTransaction Create(int senderType, string senderId, int receiverType, string receiverId, int subjectType, string subject, double amount, object transactionFactoryMethodContext = null)
        {
            var transaction = _transactionFactoryMethod.Create(transactionFactoryMethodContext);

            do
            {
                transaction.Id = Guid.NewGuid();
            } while (_transactionRepository.GetAll().Any(x => x.Id == transaction.Id));

            transaction.SenderType = senderType;
            transaction.SenderId = senderId;
            transaction.ReceiverType = receiverType;
            transaction.ReceiverId = receiverId;
            transaction.SubjectType = subjectType;
            transaction.Subject = subject;
            transaction.Amount = amount;
            transaction.CreatedDate = DateTime.UtcNow;
            _transactionRepository.Add(transaction);
            ChangeState(transaction, (int)TransactionState.Processing);

            return transaction;
        }

        public void ChangeState(Guid id, TransactionState state)
        {
            var transaction = _transactionRepository.Get(id);

            if (transaction == null)
            {
                throw new ArgumentException();
            }

            ChangeState(transaction, (int) state);
        }

        protected void ValidateTransactionParticipant(int transactionParticipantType, string transactionParticipantId)
        {
            if (!_indefiniteTransactionSubjectValidator.IsValid(new IndefiniteTransactionParticipantValidatorContext(transactionParticipantType, transactionParticipantId)))
            {
                throw new ArgumentException("Transation participant is not valid");
            }
        }

        protected void ValidateTransactionSubject(int subjectType, string subject)
        {
            if (!_transactionSubjectValidator.IsValid(new TransactionSubjectValidatorContext(subjectType, subject)))
            {
                throw new ArgumentException("Transaction subject is not valid");
            }
        }

        protected void ValidateAmount(double amount)
        {
            if (!_transactionAmountValidator.IsValid(amount))
            {
                throw new ArgumentException();
            }
        }

        protected void ChangeState(TTransaction transaction, int state)
        {
            if (transaction.State == state)
                return;

            var oldState = transaction.State;

            lock (Lockers[typeof(TTransaction)])
            {
                RollbackTransactionState(transaction, oldState);

                ValidateTransactionParticipant(transaction.SenderType, transaction.SenderId);
                ValidateTransactionParticipant(transaction.ReceiverType, transaction.ReceiverId);
                ValidateAmount(transaction.Amount);
                ValidateTransactionSubject(transaction.SubjectType, transaction.Subject);

                try
                {
                    RollforwardTransactionState(transaction, state);
                }
                catch (Exception)
                {
                    if (transaction.State == state)
                    {
                        RollbackTransactionState(transaction, state);
                    }

                    RollforwardTransactionState(transaction, oldState);

                    throw;
                }

                _transactionRepository.Commit(transaction);
            }
        }

        protected void RollbackTransactionState(TTransaction transaction, int state)
        {
            _indefiniteTransactionStateApplier.Apply(new IndefiniteTransactionStateApplierContext<TTransaction>(transaction, -1, state));
            transaction.State = 0;
        }

        protected void RollforwardTransactionState(TTransaction transaction, int state)
        {
            _indefiniteTransactionStateApplier.Apply(new IndefiniteTransactionStateApplierContext<TTransaction>(transaction, 1, state));
            transaction.State = state;
        }
    }
}