namespace Cgxarrie.Flow.Tests
{
    using Cgxarrie.Flow.Exceptions;
    using Cgxarrie.Flow.Tests.DummyClasses;
    using FluentAssertions;
    using Xunit;

    public class StateMachineTests
    {
        [Fact]
        public void ApproveInStatusWaitingForApprovalShouldBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new InvoiceStateMachine(invoice);
            stateMachine.Do(x => x.SendForApproval());

            // Act
            stateMachine.Do(x => x.Approve());

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.Approved);
            invoice.HasBeenSentForApproval.Should().BeTrue();
            invoice.HasBeenApproved.Should().BeTrue();
            invoice.HasBeenRejected.Should().BeFalse();
        }

        [Fact]
        public void NewInstanceShouldReturnBeInDefaultStatus()
        {
            // Arrange
            var invoice = new Invoice();

            // Act
            var stateMachine = new InvoiceStateMachine(invoice);

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.Created);
            invoice.HasBeenSentForApproval.Should().BeFalse();
            invoice.HasBeenApproved.Should().BeFalse();
            invoice.HasBeenRejected.Should().BeFalse();
        }

        [Fact]
        public void RejectInStatusWaitingForApprovalShouldBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new InvoiceStateMachine(invoice);
            stateMachine.Do(x => x.SendForApproval());

            // Act
            stateMachine.Do(x => x.Reject());

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.Rejected);
            invoice.HasBeenSentForApproval.Should().BeTrue();
            invoice.HasBeenApproved.Should().BeFalse();
            invoice.HasBeenRejected.Should().BeTrue();
        }

        [Fact]
        public void SendForApprovalFromCreatedShouldBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new InvoiceStateMachine(invoice);

            // Act
            stateMachine.Do(x => x.SendForApproval());

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.WaitingForApproval);
        }

        [Fact]
        public void SendForApprovalFromSendForApprovalShouldNotBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new InvoiceStateMachine(invoice);
            stateMachine.Do(x => x.SendForApproval());

            // Act
            var act = () => stateMachine.Do(x => x.SendForApproval());

            // Assert
            var exc = Assert.Throws<ActionNotPermittedException>(act);
            exc.Message.Should().Be("Action SendForApproval not permitted in status WaitingForApproval");
        }
    }
}