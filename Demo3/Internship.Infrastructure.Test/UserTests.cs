using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Internship.Infrastructure.Test
{
    public class UserTests
    {
        private readonly UserRepository _userRepo;
        private readonly DataContext _memContext;

        public UserTests()
        {
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "tmainternship")
                .Options;

            _memContext = new DataContext(dbOptions);
            _userRepo = new UserRepository(_memContext);
        }

        [Fact]
        public void Get_Existing_User()
        {
            var existingUser = Builders.GetUserDefault();

            _memContext.Users.Add(existingUser);
            _memContext.SaveChanges();

            var testWithId = _userRepo.GetOne(existingUser.UserId);
            Assert.Equal(existingUser.UserId, testWithId.UserId);

            var testWithPassword = _userRepo.GetUser(existingUser.Email, "zazaza");
            Assert.Equal(existingUser.UserId, testWithPassword.UserId);
        }
    }
}
