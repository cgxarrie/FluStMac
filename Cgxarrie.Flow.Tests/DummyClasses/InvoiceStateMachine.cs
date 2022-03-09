namespace Cgxarrie.Flow.Tests.DummyClasses
{
    public class InvoiceStateMachine : StateMachineBase<Invoice, MyFlowStatus>
    {
        public InvoiceStateMachine(Invoice invoice) : base(invoice, MyFlowStatus.Created)
        {
        }

        protected override void DefineTransitions()
        {
            AddTransition(MyFlowStatus.Created, x => x.SendForApproval(), MyFlowStatus.WaitingForApproval);
            AddTransition(MyFlowStatus.WaitingForApproval, x => x.Approve(), MyFlowStatus.Approved);
            AddTransition(MyFlowStatus.WaitingForApproval, x => x.Reject(), MyFlowStatus.Rejected);
        }
    }
}