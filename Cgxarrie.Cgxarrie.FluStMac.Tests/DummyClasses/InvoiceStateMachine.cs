namespace Cgxarrie.FluStMac.Tests.DummyClasses;

public class InvoiceStateMachine : FluentStateMachine<Invoice, InvoiceStatus>
{
    public InvoiceStateMachine(Invoice invoice) : base(invoice, InvoiceStatus.Created)
    {
        WithTransition()
            .From(InvoiceStatus.Created)
            .On(x => x.SendForApproval())
            .To(InvoiceStatus.WaitingForApproval);

        WithTransition()
            .From(InvoiceStatus.WaitingForApproval)
            .On(x => x.ReceiveSignature())
            .To(InvoiceStatus.WaitingForApproval);

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

        WithTransition()
            .From(InvoiceStatus.WaitingForSignature)
            .On(x => x.ReceiveSignature())
            .To(InvoiceStatus.Approved);
    }
}