namespace Cgxarrie.Flow.Tests.DummyClasses
{
    using System;

    public class Invoice
    {
        public bool HasBeenApproved { get; private set; } = false;
        public bool HasBeenRejected { get; private set; } = false;
        public bool HasBeenSentForApproval { get; private set; } = false;
        public Guid Id { get; set; } = Guid.NewGuid();

        public void Approve()
        {
            HasBeenApproved = true;
        }

        public void Reject()
        {
            HasBeenRejected = true;
        }

        public void SendForApproval()
        {
            HasBeenSentForApproval = true;
        }
    }
}