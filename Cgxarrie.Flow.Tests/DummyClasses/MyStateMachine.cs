namespace Cgxarrie.Flow.Tests.DummyClasses
{
    public class MyStateMachine : StateMachineBase<MyFlowStatus>
    {
        public MyStateMachine() : base(MyFlowStatus.Created)
        {
        }

        public void Approve()
        {
            ValidatePermittedAction();
            MoveNext();
        }

        public void Reject()
        {
            ValidatePermittedAction();
            MoveNext();
        }

        public void SendForApproval()
        {
            ValidatePermittedAction();
            MoveNext();
        }

        protected override void DefineTransitions()
        {
            AddTransition(MyFlowStatus.Created, nameof(SendForApproval), MyFlowStatus.WaitingForApproval);
            AddTransition(MyFlowStatus.WaitingForApproval, nameof(Approve), MyFlowStatus.Approved);
            AddTransition(MyFlowStatus.WaitingForApproval, nameof(Reject), MyFlowStatus.Rejected);
        }
    }
}