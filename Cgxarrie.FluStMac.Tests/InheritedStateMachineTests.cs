namespace Cgxarrie.FluStMac.Tests
{
    using Cgxarrie.FluStMac.Exceptions;
    using Cgxarrie.FluStMac.Tests.DummyClasses;
    using FluentAssertions;
    using Xunit;

    public class InheritedStateMachineTests
    {
        [Fact]
        public void SalesAgentApproveInStatusWaitingForApprovalShouldThrowException()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new SalesAgentInvoiceStateMachine(invoice);
            stateMachine.Do(x => x.SendForApproval());

            // Act
            var act = () => stateMachine.Do(x => x.SendForApproval());

            // Assert
            var exc = Assert.Throws<ActionNotPermittedException>(act);
            exc.Message.Should().Be("Action SendForApproval not permitted in status WaitingForApproval");
        }

        [Fact]
        public void SalesAgentReceiveSignatureShouldBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            invoice.NeedsSignature = true;
            var stateMachine = new SalesAgentInvoiceStateMachine(invoice);
            stateMachine.Do(x => x.SendForApproval());

            // Act
            stateMachine.Do(x => x.ReceiveSignature());

            // Assert
            invoice.Status.Should().Be(InvoiceStatus.WaitingForApproval);
            invoice.HasBeenSentForApproval.Should().BeTrue();
            invoice.HasReceivedSignature.Should().BeTrue();
            invoice.HasBeenApproved.Should().BeFalse();
            invoice.HasBeenRejected.Should().BeFalse();
        }

        [Fact]
        public void SalesAgentSendForApprovalFromDraftShouldBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new SalesAgentInvoiceStateMachine(invoice);

            // Act
            stateMachine.Do(x => x.SendForApproval());

            // Assert
            invoice.Status.Should().Be(InvoiceStatus.WaitingForApproval);
        }

        [Fact]
        public void SalesManagerApproveInStatusWaitingForApprovalNeedSignatureNotSignedShouldSendToWaitingForSignature()
        {
            // Arrange
            var invoice = new Invoice();
            invoice.NeedsSignature = true;

            var salesAgentStateMachine = new SalesAgentInvoiceStateMachine(invoice);
            salesAgentStateMachine.Do(x => x.SendForApproval());

            var stateMachine = new SalesManagerInvoiceStateMachine(invoice);

            // Act
            stateMachine.Do(x => x.Approve());

            // Assert
            invoice.Status.Should().Be(InvoiceStatus.WaitingForSignature);
            invoice.HasBeenSentForApproval.Should().BeTrue();
            invoice.HasReceivedSignature.Should().BeFalse();
            invoice.HasBeenApproved.Should().BeTrue();
            invoice.HasBeenRejected.Should().BeFalse();
        }

        [Fact]
        public void SalesManagerReceiveSignatureShouldBePermitted()
        {
            // Arrange
            var invoice = new Invoice();
            invoice.NeedsSignature = true;

            var salesAgentstateMachine = new SalesAgentInvoiceStateMachine(invoice);
            salesAgentstateMachine.Do(x => x.SendForApproval());

            var stateMachine = new SalesManagerInvoiceStateMachine(invoice);

            // Act
            stateMachine.Do(x => x.ReceiveSignature());

            // Assert
            invoice.Status.Should().Be(InvoiceStatus.WaitingForApproval);
            invoice.HasBeenSentForApproval.Should().BeTrue();
            invoice.HasReceivedSignature.Should().BeTrue();
            invoice.HasBeenApproved.Should().BeFalse();
            invoice.HasBeenRejected.Should().BeFalse();
        }

        [Fact]
        public void SalesManagerSendForApprovalFromDraftShouldThrowException()
        {
            // Arrange
            var invoice = new Invoice();
            var stateMachine = new SalesManagerInvoiceStateMachine(invoice);

            // Act
            var act = () => stateMachine.Do(x => x.SendForApproval());

            // Assert
            var exc = Assert.Throws<ActionNotPermittedException>(act);
            exc.Message.Should().Be("Action SendForApproval not permitted in status Draft");
        }
    }
}