using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services;
using ClayDoorsModel.Services.Definitions;
using Moq;

namespace ClayDoorsModel.Test.Services
{
    public class DoorsServiceTests
    {

        private static void GenericUnlockDoorTest(int doorId, string username, DoorUnlockResult expectedResult, Mock<IDoorsRepository> doorsRepositoryMock, Mock<IDoorUserService> doorUsersServiceMock)
        {
            var doorsService = new DoorsService(
                            doorsRepositoryMock.Object,
                            doorUsersServiceMock.Object);

            var result = doorsService.UnlockDoor(doorId, username);

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Result);
            Mock.Verify(doorsRepositoryMock);
        }

        private static bool DoorUnlockLogEquals(IDoorUnlockLog log, DoorUnlockResult result, int? doorId, string? username)
        {
            return log.Id == null
                && log.ActionResult == result
                && log.DoorId == doorId
                && string.Equals(log.Username, username);
        }

        [Fact]
        public void UnlockDoor_AnyDoorNullUsername_Unauthorized()
        {
            int doorId = 333;
            string username = null;
            DoorUnlockResult expectedResult = DoorUnlockResult.Unauthorized;

            var doorsRepositoryMock = new Mock<IDoorsRepository>(MockBehavior.Strict);
            doorsRepositoryMock
                .Setup(d => d.LogUnlock(
                    It.Is<IDoorUnlockLog>(x => DoorUnlockLogEquals(x, expectedResult, doorId, username))))
                .Verifiable();

            var doorUsersServiceMock = new Mock<IDoorUserService>(MockBehavior.Strict);

            GenericUnlockDoorTest(doorId, username, expectedResult, doorsRepositoryMock, doorUsersServiceMock);
        }

        [Fact]
        public void UnlockDoor_AnyDoorNotExistingUsername_UserNotFound()
        {
            int doorId = 333;
            string username = "not existing";
            DoorUnlockResult expectedResult = DoorUnlockResult.UserNotFound;

            var doorsRepositoryMock = new Mock<IDoorsRepository>();
            doorsRepositoryMock
                .Setup(d => d.LogUnlock(
                    It.Is<IDoorUnlockLog>(x => DoorUnlockLogEquals(x, expectedResult, doorId, username))))
                .Verifiable();

            var doorUsersServiceMock = new Mock<IDoorUserService>();
            doorUsersServiceMock
                .Setup(d => d.GetUser(username))
                .Returns((IDoorUser)null);

            GenericUnlockDoorTest(doorId, username, expectedResult, doorsRepositoryMock, doorUsersServiceMock);
        }

        [Fact]
        public void UnlockDoor_AnyDoorNotExistingEmptyUsername_UserNotFound()
        {
            int doorId = 333;
            string username = string.Empty;
            DoorUnlockResult expectedResult = DoorUnlockResult.UserNotFound;

            var doorsRepositoryMock = new Mock<IDoorsRepository>();
            doorsRepositoryMock
                .Setup(d => d.LogUnlock(
                    It.Is<IDoorUnlockLog>(x => DoorUnlockLogEquals(x, expectedResult, doorId, username))))
                .Verifiable();

            var doorUsersServiceMock = new Mock<IDoorUserService>();
            doorUsersServiceMock
                .Setup(d => d.GetUser(username))
                .Returns((IDoorUser)null);

            GenericUnlockDoorTest(doorId, username, expectedResult, doorsRepositoryMock, doorUsersServiceMock);
        }

        [Fact]
        public void UnlockDoor_NotExistingDoorValidUsername_DoorNotFound()
        {
            int doorId = -666;
            string username = "valid";
            var mockUser = new Mock<IDoorUser>(MockBehavior.Strict);
            DoorUnlockResult expectedResult = DoorUnlockResult.DoorNotFound;

            var doorsRepositoryMock = new Mock<IDoorsRepository>();
            doorsRepositoryMock
                .Setup(d => d.LogUnlock(
                    It.Is<IDoorUnlockLog>(x => DoorUnlockLogEquals(x, expectedResult, doorId, username))))
                .Verifiable();
            doorsRepositoryMock
                .Setup(d => d.GetDoor(doorId))
                .Returns((IDoor)null);

            var doorUsersServiceMock = new Mock<IDoorUserService>();
            doorUsersServiceMock
                .Setup(d => d.GetUser(username))
                .Returns(mockUser.Object);

            GenericUnlockDoorTest(doorId, username, expectedResult, doorsRepositoryMock, doorUsersServiceMock);
        }

        [Fact]
        public void UnlockDoor_ExistingDoorValidUsernameWithoutRights_Unauthorized()
        {
            int doorId = 777;
            var mockDoor = new Mock<IDoor>(MockBehavior.Strict);
            string username = "valid no rights";
            var mockUser = new Mock<IDoorUser>(MockBehavior.Strict);
            DoorUnlockResult expectedResult = DoorUnlockResult.Unauthorized;

            mockDoor
                .Setup(d => d.CanBeUnlockedBy(mockUser.Object))
                .Returns(false);

            var doorsRepositoryMock = new Mock<IDoorsRepository>();
            doorsRepositoryMock
                .Setup(d => d.LogUnlock(
                    It.Is<IDoorUnlockLog>(x => DoorUnlockLogEquals(x, expectedResult, doorId, username))))
                .Verifiable();
            doorsRepositoryMock
                .Setup(d => d.GetDoor(doorId))
                .Returns(mockDoor.Object);

            var doorUsersServiceMock = new Mock<IDoorUserService>();
            doorUsersServiceMock
                .Setup(d => d.GetUser(username))
                .Returns(mockUser.Object);

            GenericUnlockDoorTest(doorId, username, expectedResult, doorsRepositoryMock, doorUsersServiceMock);
        }


        [Fact]
        public void UnlockDoor_ExistingDoorValidUsernameWithRights_Success()
        {
            int doorId = 777;
            var mockDoor = new Mock<IDoor>(MockBehavior.Strict);
            string username = "valid righty";
            var mockUser = new Mock<IDoorUser>(MockBehavior.Strict);
            DoorUnlockResult expectedResult = DoorUnlockResult.Success;

            mockDoor
                .Setup(d => d.CanBeUnlockedBy(mockUser.Object))
                .Returns(true);

            var doorsRepositoryMock = new Mock<IDoorsRepository>();
            doorsRepositoryMock
                .Setup(d => d.LogUnlock(
                    It.Is<IDoorUnlockLog>(x => DoorUnlockLogEquals(x, expectedResult, doorId, username))))
                .Verifiable();
            doorsRepositoryMock
                .Setup(d => d.GetDoor(doorId))
                .Returns(mockDoor.Object);

            var doorUsersServiceMock = new Mock<IDoorUserService>();
            doorUsersServiceMock
                .Setup(d => d.GetUser(username))
                .Returns(mockUser.Object);

            GenericUnlockDoorTest(doorId, username, expectedResult, doorsRepositoryMock, doorUsersServiceMock);
        }
    }
}
