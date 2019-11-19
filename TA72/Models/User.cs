using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class User
    {
        public int id { get; set; }
        public String name { get; set; }
        public String displayName { get; set; }

        public User(int id, string name, string displayName)
        {
            this.id = id;
            this.name = name;
            this.displayName = displayName;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   id == user.id &&
                   name == user.name &&
                   displayName == user.displayName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name, displayName);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    
}
