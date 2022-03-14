namespace Cgxarrie.FluStMac.Tests.DummyClasses;

public abstract class BaseInvoiceStateMachine : FluentStateMachine<Invoice, InvoiceStatus>
{
    public BaseInvoiceStateMachine(Invoice invoice) : base(invoice)
    {
        WithTransition()
            .From(InvoiceStatus.WaitingForApproval)
            .On(x => x.ReceiveSignature())
            .To(InvoiceStatus.WaitingForApproval);

        WithTransition()
            .From(InvoiceStatus.WaitingForSignature)
            .On(x => x.ReceiveSignature())
            .To(InvoiceStatus.Approved);
    }
}

public class SalesAgentInvoiceStateMachine : BaseInvoiceStateMachine
{
    public SalesAgentInvoiceStateMachine(Invoice invoice) : base(invoice)
    {
        WithTransition()
            .From(InvoiceStatus.Draft)
            .On(x => x.SendForApproval())
            .To(InvoiceStatus.WaitingForApproval);
    }
}

public class SalesManagerInvoiceStateMachine : BaseInvoiceStateMachine
{
    public SalesManagerInvoiceStateMachine(Invoice invoice) : base(invoice)
    {
        WithTransition()
            .From(InvoiceStatus.WaitingForApproval)
            .On(x => x.Approve())
            .When(x => x.NeedsSignature && x.HasReceivedSignature)
            .To(InvoiceStatus.Approved);

        WithTransition()
            .From(InvoiceStatus.WaitingForApproval)
            .On(x => x.Approve())
            .When(x => x.NeedsSignature && !x.HasReceivedSignature)
            .To(InvoiceStatus.WaitingForSignature);

        WithTransition()
            .From(InvoiceStatus.WaitingForApproval)
            .On(x => x.Approve())
            .When(x => !x.NeedsSignature)
            .To(InvoiceStatus.Approved);

        WithTransition()
            .From(InvoiceStatus.WaitingForApproval)
            .On(x => x.Reject())
            .To(InvoiceStatus.Rejected);
    }
}