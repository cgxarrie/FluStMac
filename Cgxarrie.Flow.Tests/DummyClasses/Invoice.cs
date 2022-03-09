namespace Cgxarrie.Flow.Tests.DummyClasses
{
    using System;

    public class Invoice
    {
        public bool HasBeenApproved { get; private set; } = false;
        public bool HasBeenRejected { get; private set; } = false;
        public bool HasBeenSentForApproval { get; private set; } = false;
        public bool HasReceivedSignature { get; private set; } = false;

        public Guid Id { get; set; } = Guid.NewGuid();
        public bool NeedsSignature { get; set; } = false;

        public void Approve()
        {
            HasBeenApproved = true;
        }

        public void ReceiveSignature()
        {
            HasReceivedSignature = true;
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