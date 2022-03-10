namespace Cgxarrie.Flow.Tests.DummyClasses
{
    public class InvoiceStateMachine : StateMachineBase<Invoice, InvoiceStatus>
    {
        public InvoiceStateMachine(Invoice invoice) : base(invoice, InvoiceStatus.Created)
        {
        }

        protected override void DefineTransitions()
        {
            AddTransition(InvoiceStatus.Created, x => x.SendForApproval(), InvoiceStatus.WaitingForApproval);
            AddTransition(InvoiceStatus.WaitingForApproval, x => x.ReceiveSignature(), InvoiceStatus.WaitingForApproval);
            AddTransition(InvoiceStatus.WaitingForApproval, x => x.Approve(), x => x.NeedsSignature && x.HasReceivedSignature, InvoiceStatus.Approved);
            AddTransition(InvoiceStatus.WaitingForApproval, x => x.Approve(), x => x.NeedsSignature && !x.HasReceivedSignature, InvoiceStatus.WaitingForSignature);
            AddTransition(InvoiceStatus.WaitingForApproval, x => x.Approve(), x => !x.NeedsSignature, InvoiceStatus.Approved);
            AddTransition(InvoiceStatus.WaitingForSignature, x => x.ReceiveSignature(), InvoiceStatus.Approved);
            AddTransition(InvoiceStatus.WaitingForApproval, x => x.Reject(), InvoiceStatus.Rejected);
        }
    }
}