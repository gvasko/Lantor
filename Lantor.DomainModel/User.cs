using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }

        private static User NullUser = new User();

        public static User GetNullUser() { return NullUser; }

        public User(int id = 1)
        {
            Id = id;
            Name = "";
            UserName = "";
            Email = "";
            ExternalId = "";
        }

        public User(string name, string userName, string email, string externalId)
        {
            Name = name;
            UserName = userName;
            Email = email;
            ExternalId = externalId;
        }
    }
}
