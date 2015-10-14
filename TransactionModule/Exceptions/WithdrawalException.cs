using System;

namespace TransactionModule.Exceptions
{
    [Serializable]
    public class WithdrawalException: Exception
    {
        private const string DefaultExceptionMessage = "Ошибка списания со счета";

        public WithdrawalException() : base(DefaultExceptionMessage) { }

        public WithdrawalException(string message) : base(message) { }

        public WithdrawalException(Exception innerException) : base(DefaultExceptionMessage, innerException) { }

        public WithdrawalException(string message, Exception innerException) : base(message, innerException) { }
    }
}