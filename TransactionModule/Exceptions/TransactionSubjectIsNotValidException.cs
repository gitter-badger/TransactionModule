using System;

namespace TransactionModule.Exceptions
{
    public class TransactionSubjectIsNotValidException: Exception
    {
        private const string DefaultMessage = "Некорректный субъект транзакции";

         public TransactionSubjectIsNotValidException() : base(DefaultMessage) { }

        public TransactionSubjectIsNotValidException(string message) : base(message) { }

        public TransactionSubjectIsNotValidException(Exception innerException) : base(DefaultMessage, innerException) { }

        public TransactionSubjectIsNotValidException(string message, Exception innerException) : base(message, innerException) { }
    }
}