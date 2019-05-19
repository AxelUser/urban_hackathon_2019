using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public static class UserTable
    {
        public static User Get(string mail)
        {
            using (var connector = new BDConnector())
            {
                return connector.GetListFromJsons<User>("UserIfo", "UserInfo", $"ID = \"{mail}\"").FirstOrDefault();
            }
        }

        public static bool Contatins(string mail)
        {
            return Get(mail) != null;
        }

        public static List<User> GetAll()
        {
            using (var connector = new BDConnector())
            {
                return connector.GetListFromJsons<User>("UserIfo", "UserInfo");
            }
        }

        public static void Add(params User[] users)
        {
            using (var connector = new BDConnector())
            {
                foreach (var user in users)
                    connector.Add("UserIfo", Tuple.Create("ID", user.Email),
                        Tuple.Create("UserInfo", JsonConvert.SerializeObject(user)));
            }
        }

        public static void Update(string mail, User user)
        {
            using (var connector = new BDConnector())
            {
                connector.Update("UserIfo", "UserInfo", JsonConvert.SerializeObject(user), $"ID = \"{mail}\"");
            }
        }
    }
}