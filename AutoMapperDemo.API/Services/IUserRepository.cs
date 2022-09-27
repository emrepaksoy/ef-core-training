using AutoMapperDemo.API.Entities;

namespace AutoMapperDemo.API.Services
{
    public interface IUserRepository 
    {
        User CreateUser(User user);
        List<User> GetAllUser();
        User GetUserById(int id);
    }
}
