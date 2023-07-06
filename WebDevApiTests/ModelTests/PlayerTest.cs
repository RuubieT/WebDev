using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Models;

namespace WebDevApiTests.ModelTests
{
    public class PlayerTest
    {
        Player player;

        [SetUp]
        public void Setup()
        {
            player = new Player
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "player",
                Email = "TestMail",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword"),
                UserName = "TestUsername",
                Chips = 0,
                PokerTableId = Guid.NewGuid(),
            };
        }

        [Test]
        public void CreatePlayer(){
            Assert.IsNotNull(player);
            Assert.That(player.Id, Is.Not.EqualTo(Guid.NewGuid().ToString()));
            Assert.That(player.FirstName, Is.EqualTo("Test"));
        }

        [Test]
        public void PlayerCanReturnPlayerDto()
        {
            var result = player.GetPlayerDto();
            Assert.IsNotNull(result);
            Assert.That(player.Id, Is.EqualTo(result.Id.ToString()));
            Assert.IsNotNull(result.FirstName);
            Assert.IsNotNull(result.LastName);
            Assert.IsNotNull(result.Email);
        }

        [Test]
        public void PlayerCanReturnLeaderboardDto()
        {
            var result = player.GetLeaderBoardDto();
            Assert.IsNotNull(result);
            Assert.That(player.UserName, Is.EqualTo(result.Username));
            Assert.That(player.Chips, Is.EqualTo(result.Chips));
        }

        [Test]
        public void CreateLeaderBoardDto()
        {
            var dto = new GetLeaderBoardDto();
            Assert.IsNull(dto.Username);
            Assert.That(dto.Chips, Is.EqualTo(0));

            var newDto = new GetLeaderBoardDto { Chips = 12 };
            Assert.IsNotNull(newDto.Chips);
            Assert.IsNull(newDto.Username);

        }
        [Test]
        public void CreateGetPlayerDto()
        {
            var dto = new GetPlayerDto();
            Assert.IsNull(dto.PokerTableId);

            var newDto = new GetPlayerDto{Username = "TEST", FirstName = "TEST", LastName = "TEST"};
            Assert.IsNotNull(newDto.Username);
            Assert.That(newDto.FirstName, Is.EqualTo("TEST"));
        }
        [Test]
        public void CreatePostPlayerDto()
        {
            var dto = new PostPlayerDto();
            Assert.IsNull(dto.Username);

            var newDto = new PostPlayerDto{Username = "TEST"};
            Assert.That(newDto.Username, Is.EqualTo("TEST"));
        }
        [Test]
        public void CreatePostPlayerUsernameDto()
        {
            var dto = new PostPlayerDto();
            Assert.IsNull(dto.Username);

            var newDto = new PostPlayerUsernameDto{Email = "TEST", Username = "TEST"};
            Assert.That(newDto.Username, Is.EqualTo("TEST"));
            Assert.IsNotNull(newDto.Email);
        }
        [Test]
        public void CreatePutUsernameDto()
        {
            var dto = new PutUserNameDto();
            Assert.IsNull(dto.UserName);

            var newDto = new PutUserNameDto { UserName = "New" };
            Assert.IsNotNull(newDto.UserName);
        }
    }
}
