namespace TransactionModule.Strategies.Interfaces
{
    public interface IOperateWalletStrategy<TTransaction>
    {
        void Operate(int participantType, string participantId, double amount);
    }
}