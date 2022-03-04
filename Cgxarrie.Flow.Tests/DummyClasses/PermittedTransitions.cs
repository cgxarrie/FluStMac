namespace Cgxarrie.Flow.Tests.DummyClasses
{
    public class PermittedTransitions : Transitions<MyFlowStatus>
    {
        public PermittedTransitions()
        {
            Add(MyFlowStatus.None, MyFlowStatus.Created);
            Add(MyFlowStatus.Created, MyFlowStatus.WaitingForApproval);
            Add(MyFlowStatus.WaitingForApproval, MyFlowStatus.Approved);
            Add(MyFlowStatus.WaitingForApproval, MyFlowStatus.Rejected);
        }
    }
}