namespace Cgxarrie.Flow.Tests
{
    using Cgxarrie.Flow.Tests.DummyClasses;
    using FluentAssertions;
    using Xunit;

    public class PermittedTransitionsTests
    {
        [Theory]
        [InlineData(MyFlowStatus.Approved, MyFlowStatus.None, false)]
        [InlineData(MyFlowStatus.Approved, MyFlowStatus.Created, false)]
        [InlineData(MyFlowStatus.Approved, MyFlowStatus.WaitingForApproval, false)]
        [InlineData(MyFlowStatus.Approved, MyFlowStatus.Approved, false)]
        [InlineData(MyFlowStatus.Approved, MyFlowStatus.Rejected, false)]
        public void ActionsFromApprovedShouldBeHaveExpetedPermission(MyFlowStatus fromStatus, MyFlowStatus toStatus, bool expectedPermitted)
        {
            // Arrange
            var pt = new PermittedTransitions();

            // Act
            var permitted = pt.Permitted(fromStatus, toStatus);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.Created, MyFlowStatus.None, false)]
        [InlineData(MyFlowStatus.Created, MyFlowStatus.Created, false)]
        [InlineData(MyFlowStatus.Created, MyFlowStatus.WaitingForApproval, true)]
        [InlineData(MyFlowStatus.Created, MyFlowStatus.Approved, false)]
        [InlineData(MyFlowStatus.Created, MyFlowStatus.Rejected, false)]
        public void ActionsFromCreatedShouldBeHaveExpetedPermission(MyFlowStatus fromStatus, MyFlowStatus toStatus, bool expectedPermitted)
        {
            // Arrange
            var pt = new PermittedTransitions();

            // Act
            var permitted = pt.Permitted(fromStatus, toStatus);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.None, MyFlowStatus.None, false)]
        [InlineData(MyFlowStatus.None, MyFlowStatus.Created, true)]
        [InlineData(MyFlowStatus.None, MyFlowStatus.WaitingForApproval, false)]
        [InlineData(MyFlowStatus.None, MyFlowStatus.Approved, false)]
        [InlineData(MyFlowStatus.None, MyFlowStatus.Rejected, false)]
        public void ActionsFromNoneShouldBeHaveExpetedPermission(MyFlowStatus fromStatus, MyFlowStatus toStatus, bool expectedPermitted)
        {
            // Arrange
            var pt = new PermittedTransitions();

            // Act
            var permitted = pt.Permitted(fromStatus, toStatus);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.Rejected, MyFlowStatus.None, false)]
        [InlineData(MyFlowStatus.Rejected, MyFlowStatus.Created, false)]
        [InlineData(MyFlowStatus.Rejected, MyFlowStatus.WaitingForApproval, false)]
        [InlineData(MyFlowStatus.Rejected, MyFlowStatus.Approved, false)]
        [InlineData(MyFlowStatus.Rejected, MyFlowStatus.Rejected, false)]
        public void ActionsFromRejectedShouldBeHaveExpetedPermission(MyFlowStatus fromStatus, MyFlowStatus toStatus, bool expectedPermitted)
        {
            // Arrange
            var pt = new PermittedTransitions();

            // Act
            var permitted = pt.Permitted(fromStatus, toStatus);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }

        [Theory]
        [InlineData(MyFlowStatus.WaitingForApproval, MyFlowStatus.None, false)]
        [InlineData(MyFlowStatus.WaitingForApproval, MyFlowStatus.Created, false)]
        [InlineData(MyFlowStatus.WaitingForApproval, MyFlowStatus.WaitingForApproval, false)]
        [InlineData(MyFlowStatus.WaitingForApproval, MyFlowStatus.Approved, true)]
        [InlineData(MyFlowStatus.WaitingForApproval, MyFlowStatus.Rejected, true)]
        public void ActionsFromWaitingForApprovalShouldBeHaveExpetedPermission(MyFlowStatus fromStatus, MyFlowStatus toStatus, bool expectedPermitted)
        {
            // Arrange
            var pt = new PermittedTransitions();

            // Act
            var permitted = pt.Permitted(fromStatus, toStatus);

            // Assert
            permitted.Should().Be(expectedPermitted);
        }
    }
}