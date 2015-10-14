using System;

namespace TransactionModule.Interfaces
{
    public interface ITransaction
    {
        Guid Id { get; set; }

        int SenderType { get; set; }

        string SenderId { get; set; }

        int ReceiverType { get; set; }

        string ReceiverId { get; set; }

        int SubjectType { get; set; }

        string Subject { get; set; }

        int State { get; set; }

        double Amount { get; set; }

        DateTime CreatedDate { get; set; }
    }
}