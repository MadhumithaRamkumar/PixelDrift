using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PixelDrift.Models
{
    [Serializable]
    [DataContract]
    public class User_loginDBRep : pixeldrift_dbEntities1
    {
        [DataMember]
        public string User_Id { get; set; }
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string First_Name { get; set; }
        [DataMember]
        public string Last_Name { get; set; }
        [DataMember]
        public string Email_ID { get; set; }
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public int tab_Id { get; set; }
    
        public List<User_login> GetUser_Logins()
        {
            return User_login.ToList();
        }
    }
}
