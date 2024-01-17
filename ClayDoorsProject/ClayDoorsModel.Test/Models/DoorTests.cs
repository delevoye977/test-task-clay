
using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using Moq;

namespace ClayDoorsModel.Test.Models
{
    public class DoorTests
    {
        [Fact]
        public void CanBeUnlockedBy_NullUser_False()
        {
            var permissions = Enumerable.Empty<IDoorUserPermission>();
            var door = new Door(
                1, "somewhere", "located in a building", permissions);
            IDoorUser user = null;

            var result = door.CanBeUnlockedBy(user);

            Assert.False(result);
        }

        [Fact]
        public void CanBeUnlockedBy_NoPermissionsSet_ValidUser_False()
        {
            var permissions = Enumerable.Empty<IDoorUserPermission>();
            var door = new Door(
                1, "somewhere", "located in a building", permissions);
            var userMock = new Mock<IDoorUser>();

            var result = door.CanBeUnlockedBy(userMock.Object);

            Assert.False(result);
        }

        [Fact]
        public void CanBeUnlockedBy_ValidUserWithoutPermission_False()
        {
            var permissions = new List<IDoorUserPermission>()
            {
                new Mock<IDoorUserPermission>().Object,
                new Mock<IDoorUserPermission>().Object,
                new Mock<IDoorUserPermission>().Object,
            };
            var door = new Door(
                1, "somewhere", "located in a building", permissions);
            var userMock = new Mock<IDoorUser>(MockBehavior.Strict);
            userMock
                .Setup(u => u.HasPermission(It.IsAny<IDoorUserPermission>()))
                .Returns(false)
                .Verifiable();

            var result = door.CanBeUnlockedBy(userMock.Object);

            Assert.False(result);
            Mock.Verify(userMock);
        }

        [Fact]
        public void CanBeUnlockedBy_ValidUserWithSomePermissionsOkSomeNotOk_False()
        {
            var perm1 = new Mock<IDoorUserPermission>();
            var perm2 = new Mock<IDoorUserPermission>();
            var perm3 = new Mock<IDoorUserPermission>();
            var permissions = new List<IDoorUserPermission>()
                { perm1.Object, perm2.Object, perm3.Object };
            var door = new Door(
                1, "somewhere", "located in a building", permissions);
            var userMock = new Mock<IDoorUser>(MockBehavior.Strict);
            userMock
                .Setup(u => u.HasPermission(perm1.Object))
                .Returns(false);
            userMock
                .Setup(u => u.HasPermission(perm2.Object))
                .Returns(true);
            userMock
                .Setup(u => u.HasPermission(perm3.Object))
                .Returns(false);

            var result = door.CanBeUnlockedBy(userMock.Object);

            Assert.False(result);
        }

        [Fact]
        public void CanBeUnlockedBy_ValidUserWithAllPermissionsOk_True()
        {
            var perm1 = new Mock<IDoorUserPermission>();
            var perm2 = new Mock<IDoorUserPermission>();
            var perm3 = new Mock<IDoorUserPermission>();
            var permissions = new List<IDoorUserPermission>()
                { perm1.Object, perm2.Object, perm3.Object };
            var door = new Door(
                1, "somewhere", "located in a building", permissions);
            var userMock = new Mock<IDoorUser>(MockBehavior.Strict);
            userMock
                .Setup(u => u.HasPermission(perm1.Object))
                .Returns(true);
            userMock
                .Setup(u => u.HasPermission(perm2.Object))
                .Returns(true);
            userMock
                .Setup(u => u.HasPermission(perm3.Object))
                .Returns(true);

            var result = door.CanBeUnlockedBy(userMock.Object);

            Assert.True(result);
        }
    }
}
