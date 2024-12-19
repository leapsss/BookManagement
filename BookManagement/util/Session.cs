
/*该方法类用来
 * 设置
   存储
   清空
当前登录用户的id
具体调用方法如下：*/


using BookManagement.entity;

namespace BookManagement.util
{
    public static class Session
    {

        // 用于存储当前登录用户的ID
        private static int? _currentUserId;
        private static User? _user = new User();

        /// <summary>
        /// 设置当前登录的用户ID
        /// </summary>
        public static void SetCurrentUserId(int userId)
        {
            _currentUserId = userId;
        }

        /// <summary>
        /// 获取当前登录的用户ID
        /// </summary>
        public static int? GetCurrentUserId()
        {
            return _currentUserId;
        }

        public static void SetCurrentUser(User user)
        {
            _user = user;
        }

        public static User? GetCurrentUser()
        {
            return _user;
        }

        /// <summary>
        /// 清除当前用户ID（用户登出时调用）
        /// </summary>
        public static void Clear()
        {
            _currentUserId = null;
            _user = null;
        }

    }
}
