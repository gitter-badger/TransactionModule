namespace TransactionModule.Validators.Context
{
    public class DefiniteTransactionParticipantValidatorContext
    {
        public DefiniteTransactionParticipantValidatorContext(string id)
        {
            Id = id;
        }

        public string Id { get; set; } 
    }
}