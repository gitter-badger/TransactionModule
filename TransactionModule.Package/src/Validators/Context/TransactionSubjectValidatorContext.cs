namespace TransactionModule.Validators.Context
{
    public class TransactionSubjectValidatorContext
    {
        public TransactionSubjectValidatorContext(int subjectType, string subject)
        {
            SubjectType = subjectType;
            Subject = subject;
        }

        public int SubjectType { get; set; }

        public string Subject { get; set; }
    }
}