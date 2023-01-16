using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication8.Entities;

namespace WebApplication8.Models
{
    public class respUsers
    {
        public respUsers(users users)
        {
            id = users.id;
            login = users.login;
            password = users.password;
        }
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }   
    }
}