namespace Cgxarrie.Flow.Tests
{
    using Cgxarrie.Flow.Exceptions;
    using Cgxarrie.Flow.Tests.DummyClasses;
    using FluentAssertions;
    using Xunit;

    public class FlowTests
    {
        [Fact]
        public void ApproveInStatusWaitingForApprovalShouldBePermitted()
        {
            // Arrange
            var flow = new MyFlow();
            flow.SendForApproval();

            // Act
            flow.Approve();

            // Assert
            flow.Status.Should().Be(MyFlowStatus.Approved);
        }

        [Fact]
        public void BackFromSendForApprovalShouldNotBePermitted()
        {
            // Arrange
            var flow = new MyFlow();
            flow.SendForApproval();

            // Act
            var act = () => flow.BackToCreated();

            // Assert
            var exc = Assert.Throws<TransitionNotPermittedException>(act);
            exc.Message.Should().Be("Transition not permitted from WaitingForApproval to Created");
        }

        [Fact]
        public void NewInstanceShouldReturnBeInDefaultStatus()
        {
            // Arrange
            // Act
            var flow = new MyFlow();

            // Assert
            flow.Status.Should().Be(MyFlowStatus.Created);
        }

        [Fact]
        public void RejectInStatusWaitingForApprovalShouldBePermitted()
        {
            // Arrange
            var flow = new MyFlow();
            flow.SendForApproval();

            // Act
            flow.Reject();

            // Assert
            flow.Status.Should().Be(MyFlowStatus.Rejected);
        }

        [Fact]
        public void SendForApprovalFromCreatedShouldBePermitted()
        {
            // Arrange
            var flow = new MyFlow();

            // Act
            flow.SendForApproval();

            // Assert
            flow.Status.Should().Be(MyFlowStatus.WaitingForApproval);
        }

        [Fact]
        public void SendForApprovalFromSendForApprovalShouldNotBePermitted()
        {
            // Arrange
            var flow = new MyFlow();
            flow.SendForApproval();

            // Act
            var act = () => flow.SendForApproval();

            // Assert
            var exc = Assert.Throws<ActionNotPermittedException>(act);
            exc.Message.Should().Be("Action SendForApproval not permitted in status WaitingForApproval");
        }
    }
}