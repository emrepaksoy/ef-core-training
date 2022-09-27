using AutoMapperDemo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperDemo.API.Services
{
    public class UserRepository : IUserRepository
    {
        DbContext c;
        DbSet<User> users;
        public UserRepository(DbContext context)
        {
            c = context;
            users = c.Set<User>();
        }

        public User CreateUser(User user)
        {
           users.Add(user);
            c.SaveChanges();
            return user;
        }

        public List<User> GetAllUser()
        {

         return (from u in users select u).ToList();
            //return users.ToList();

        }

        public User GetUserById(int id)
        {
            var user = (from u in users where u.Id == id select u).FirstOrDefault();
            //var user = users.FirstOrDefault(u => u.Id == guid);
            return user;
        }
    }
}
