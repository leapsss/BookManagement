using BookManagement.entity;
using BookManagement.mapper;
namespace BookManagement.service
{
    public class UserService
    {
        private readonly UserMapper userMapper;
        public UserService() {
            userMapper = new UserMapper();
        }

        public List<User> GetUsers()
        {
           return userMapper.GetUsers();
        }
        public  User GetUserById(string id)
        {
            return userMapper.GetUserById(id);
        }

        public  void UpdateUser(User user)
        {
            userMapper.UpdateUser(user);
        }
        public void DeleteUser(string id)
        {
            userMapper.DeleteUser(id);
        }
        public void AddUser(User user)
        {
            userMapper.AddUser(user);
        }
    }


}
