namespace TransactionModule.Enums
{
    public enum TransactionState
    {
        Processing = 1,
        Hold = 2,
        Completed = 4,
        Canceled = 8
    }
}