namespace Cgxarrie.Flow.Tests.DummyClasses
{
    public class MyFlow : FlowBase<MyFlowStatus>
    {
        public MyFlow() : base(MyFlowStatus.Created)
        {
        }

        public void Approve()
        {
            ValidatePermittedAction();
            ChangeStatus(MyFlowStatus.Approved);
        }

        public void Reject()
        {
            ValidatePermittedAction();
            ChangeStatus(MyFlowStatus.Rejected);
        }

        public void SendForApproval()
        {
            ValidatePermittedAction();
            ChangeStatus(MyFlowStatus.WaitingForApproval);
        }

        protected override void AddPermittedActions()
        {
            AddAction(MyFlowStatus.Created, nameof(SendForApproval));
            AddAction(MyFlowStatus.WaitingForApproval, nameof(Approve));
            AddAction(MyFlowStatus.WaitingForApproval, nameof(Reject));
        }
    }
}