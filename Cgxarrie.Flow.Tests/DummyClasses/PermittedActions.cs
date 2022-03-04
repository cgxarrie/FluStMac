namespace Cgxarrie.Flow.Tests.DummyClasses
{
    using Cgxarrie.Flow.Collections;

    public class PermittedActions : Actions<MyFlowStatus>
    {
        public PermittedActions()
        {
            Add(MyFlowStatus.None, "Create");
            Add(MyFlowStatus.Created, "SendForApproval");
            Add(MyFlowStatus.WaitingForApproval, "Approve");
            Add(MyFlowStatus.WaitingForApproval, "Reject");
        }
    }
}