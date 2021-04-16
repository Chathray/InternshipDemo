using Internship.Infrastructure;

namespace Internship.Application
{
    public interface IUserService : IServiceBase<UserModel, User>
    {
        UserModel Authenticate(string loginEmail, string loginPassword);
        int CountByIndex(int index);
    }
}