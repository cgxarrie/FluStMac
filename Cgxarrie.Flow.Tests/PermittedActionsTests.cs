namespace Cgxarrie.Flow.Tests
{
    using Cgxarrie.Flow.Tests.DummyClasses;
    using FluentAssertions;
    using Xunit;

    public class PermittedActionsTests
    {
        [Theory]
        [InlineData(MyFlowStatus.Approved, "Create", false)]
        [InlineData(MyFlowStatus.Approved, "Approve", false)]
        [InlineData(MyFlowStatus.Approved, "Reject", false)]
        public void ActionsFromApprovedShouldBeHaveExpetedPermission(MyFlowStatus status, string action, bool expectedPermitted)
        {
            // Arrange
            var pa = new PermittedActions();

            // Act
            var permitted = pa.Permitted(status, action);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.Created, "qaz", false)]
        [InlineData(MyFlowStatus.Created, "SendForApproval", true)]
        public void ActionsFromCreatedShouldBeHaveExpetedPermission(MyFlowStatus status, string action, bool expectedPermitted)
        {
            // Arrange
            var pa = new PermittedActions();

            // Act
            var permitted = pa.Permitted(status, action);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.None, "qaz", false)]
        [InlineData(MyFlowStatus.None, "Create", true)]
        public void ActionsFromNoneShouldBeHaveExpetedPermission(MyFlowStatus status, string action, bool expectedPermitted)
        {
            // Arrange
            var pa = new PermittedActions();

            // Act
            var permitted = pa.Permitted(status, action);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.Rejected, "Create", false)]
        [InlineData(MyFlowStatus.Rejected, "Approve", false)]
        [InlineData(MyFlowStatus.Rejected, "Reject", false)]
        public void ActionsFromRejectedShouldBeHaveExpetedPermission(MyFlowStatus status, string action, bool expectedPermitted)
        {
            // Arrange
            var pa = new PermittedActions();

            // Act
            var permitted = pa.Permitted(status, action);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.WaitingForApproval, "Create", false)]
        [InlineData(MyFlowStatus.WaitingForApproval, "Approve", true)]
        [InlineData(MyFlowStatus.WaitingForApproval, "Reject", true)]
        public void ActionsFromWaitingForApprovalShouldBeHaveExpetedPermission(MyFlowStatus status, string action, bool expectedPermitted)
        {
            // Arrange
            var pa = new PermittedActions();

            // Act
            var permitted = pa.Permitted(status, action);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }
    }
}