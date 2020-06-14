using System;
using System.Collections.Generic;
using System.Text;

namespace CCL.Security.Identity
{
    public abstract class User
    {
        public User(int userId, string name, int postofficeId, string userType)
        {
            UserId = userId;
            Name = name;
            postofficeID = postofficeId;
            UserType = userType;
        }
        public int UserId { get; }
        public string Name { get; }
        public int postofficeID { get; }
        protected string UserType { get; }
    }
}
