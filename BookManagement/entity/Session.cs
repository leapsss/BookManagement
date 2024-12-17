using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*该方法类用来
 * 设置
   存储
   清空
当前登录用户的id
具体调用方法如下：*/


namespace BookManagement.entity
{
    public static class Session
    {

        // 用于存储当前登录用户的ID
        private static string? _currentUserId;

        /// <summary>
        /// 设置当前登录的用户ID
        /// </summary>
        public static void SetCurrentUserId(string userId)
        {
            _currentUserId = userId;
        }

        /// <summary>
        /// 获取当前登录的用户ID
        /// </summary>
        public static string? GetCurrentUserId()
        {
            return _currentUserId;
        }

        /// <summary>
        /// 清除当前用户ID（用户登出时调用）
        /// </summary>
        public static void Clear()
        {
            _currentUserId = null;
        }
    }
}
