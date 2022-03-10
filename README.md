# FluStMac - Fluent State Machine
A Fluent finite state machine
Provides an easy to configure and use state mahine base class

## Features
- Fluent addition of transitions
- Transitions based on current status and requested action
- Conditional transitions based on current status, requested action, and a condition to be met by the element handled by the state machine.

## How to use
- Declare the collection of possible status to be handled by the machine, of type **TStatus**
- Declare the element to be handled by the machine of type **T**
- Declare the machine inheriting FluentStateMachine<T, TStatus>
- Declare transitions in constructor of the machine
- Transitions are evaluated in the order they are added. The first transition found matching the current request will provide the new status
- If the requested action is not permitted in the current Status a **ActionNotPermittedException** is thrown
- If no condition can be met to get the next status a **TransitionNotFoundException** is thorwn

## Example
We will simulate an Invoice workflow, declaring the following statuses
- Created
- Waiting for approval
- Approved
- Rejected
- Waiting for signature

the folloing actions
- Send For Approval
- Approve
- Reject
- Receive signature

and the following use cases
- In satus Created, On sent for approval, change to Waiting for approval
- In status Waiting for approval, On Receive signature, stay in Waiting for approval
- In status Waiting for approval, On Reject, change to Rejected
- In status Waiting for approval, On Approve, 
	- When signature is needed and not received,  change to Waiting for signature
	- Else, change to Approved.

### Declare Invoice statuses
```csharp
public enum InvoiceStatus
{
	Created = 0,
    WaitingForApproval = 1,
    Approved = 2,
    Rejected = 3,
    WaitingForSignature = 4
}
```
### Declare Invoice class
```csharp
public enum Invoice
{
	public Guid Id {get; set;} = Guid.NewGuid();
	public bool NeedsSignature { get; set; } = false;
	
	// flags to change the state of the instance
	public bool HasBeenApproved { get; private set; } = false;
    public bool HasBeenRejected { get; private set; } = false;
    public bool HasBeenSentForApproval { get; private set; } = false;
    public bool HasReceivedSignature { get; private set; } = false;
    
	// Actions
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
```
### Declare the state machine
```csharp
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
```