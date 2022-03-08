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
            var stateMachine = new MyStateMachine();
            stateMachine.SendForApproval();

            // Act
            stateMachine.Approve();

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.Approved);
        }

        [Fact]
        public void NewInstanceShouldReturnBeInDefaultStatus()
        {
            // Arrange
            // Act
            var stateMachine = new MyStateMachine();

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.Created);
        }

        [Fact]
        public void RejectInStatusWaitingForApprovalShouldBePermitted()
        {
            // Arrange
            var stateMachine = new MyStateMachine();
            stateMachine.SendForApproval();

            // Act
            stateMachine.Reject();

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.Rejected);
        }

        [Fact]
        public void SendForApprovalFromCreatedShouldBePermitted()
        {
            // Arrange
            var stateMachine = new MyStateMachine();

            // Act
            stateMachine.SendForApproval();

            // Assert
            stateMachine.Status.Should().Be(MyFlowStatus.WaitingForApproval);
        }

        [Fact]
        public void SendForApprovalFromSendForApprovalShouldNotBePermitted()
        {
            // Arrange
            var stateMachine = new MyStateMachine();
            stateMachine.SendForApproval();

            // Act
            var act = () => stateMachine.SendForApproval();

            // Assert
            var exc = Assert.Throws<ActionNotPermittedException>(act);
            exc.Message.Should().Be("Action SendForApproval not permitted in status WaitingForApproval");
        }
    }
}