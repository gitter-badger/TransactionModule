using System;
using System.Linq;
using TransactionModule.Interfaces;

namespace TransactionModule.Repositories.Interfaces
{
    public interface ITransactionRepository<TTransaction>
        where TTransaction: ITransaction
    {
        TTransaction Get(Guid id);

        IQueryable<TTransaction> GetAll(); 

        void Add(TTransaction transaction);

        void Commit(TTransaction transaction);
    }
}