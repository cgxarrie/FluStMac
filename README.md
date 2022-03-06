# flow
Easy handle permitted actions on classes dependig on status

 - Flow class must inherit FlowBase< TStatus>
 - TStatus is the type defining the statuses of the workflow
 - All permitted actions by status must be added AddPermittedActions (overriden from abstract base)
 - All action methods must start with command ValidatePermittedAction()
 - When action is not permitted in current status a ActionNotPermittedException is thrown.

## Example
Given an Invoice, the following worklow should be followed: 

|Action| Status  |
|--|--|
|1. Create  | Draft  |
|2. Send for approval  | Waiting for approval  |
|2.1 Approve  | Approved  |
|2.2 Reject  | Rejected  |

### Declare Invoice statuses
```csharp
public enum InvoiceStatus
{
	Draft = 0,
    WaitingForApproval = 1,
    Approved = 2,
    Rejected = 3
}
```
### Declare Invoice class
```csharp
public enum Invoice : FlowBase<InvoiceStatus> // Declare the type of the Status
{
	public Invoice : base(InvoiceStatus.Draft) //specify the default status
	{
	}
	
	public Guid Id {get; set;}
	public string Code {get; set;}	
	public Guid CustimerId {get; set;}	
	// All other invoice properties

	//Declare all permitted actions per status (overriden from base class)
	//In the example:
	//	In status Draft, SendForApproval is permitted
	//	In status WaitingForApproval, Approve and Reject are permitted
	//	In status Approved no action is permitted
	//	In status Rejected no action is permitted
	protected override void AddPermittedActions()
	{
		AddAction(InvoiceStatus.Draft, nameof(SendForApproval));
        AddAction(InvoiceStatus.WaitingForApproval, nameof(Approve));
        AddAction(InvoiceStatus.WaitingForApproval, nameof(Reject));	
	}

	public void Approve()
	{
		ValidatePermittedAction(); //Check if this Approve can be executed in the current status.
		// Do all needed actions 
		ChangeStatus(InvoiceStatus.Approved);
	}	
	
	public void Reject()
	{
		ValidatePermittedAction(); //Check if this Reject can be executed in the current status.

		// Do all needed actions 
		ChangeStatus(InvoiceStatus.Rejected);
	}	
	public void SendForApproval()
	{
		ValidatePermittedAction();//Check if this SendForApproval can be executed in the current status.

		// Do all needed actions 
		ChangeStatus(InvoiceStatus.WaitingForApproval);
	}	
}
```