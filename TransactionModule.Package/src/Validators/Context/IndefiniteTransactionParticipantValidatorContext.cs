namespace TransactionModule.Validators.Context
{
    public class IndefiniteTransactionParticipantValidatorContext: DefiniteTransactionParticipantValidatorContext
    {
        public IndefiniteTransactionParticipantValidatorContext(int type, string id)
            :base(id)
        {
            Type = type;
        }

        public int Type { get; set; }
    }
}