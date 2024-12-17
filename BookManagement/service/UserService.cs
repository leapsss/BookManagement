using BookManagement.entity;
using BookManagement.mapper;
namespace BookManagement.service
{
    public class UserService
    {
        public UserService() { }

        public List<User> GetUsers()
        {
           return UserMapper.GetUsers();
        }
        public  User GetUserById(string id)
        {
            return UserMapper.GetUserById(id);
        }

        public  void UpdateUser(User user)
        {
            UserMapper.UpdateUser(user);
        }
        public void DeleteUser(string id)
        {
            UserMapper.DeleteUser(id);
        }
        public void AddUser(User user)
        {
            UserMapper.AddUser(user);
        }
    }


}
