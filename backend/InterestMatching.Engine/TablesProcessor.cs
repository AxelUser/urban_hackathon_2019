using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public static class UserTable
    {
        public static User Get(string mail)
        {
            using (var connector = new BDConnector())
                return connector.GetListFromJsons<User>("UserIfo", "UserInfo", $"ID = \"{mail}\"").FirstOrDefault();
            }

        public static bool Contatins(string mail)
        {
            return Get(mail) != null;
        }

        public static List<User> GetAll()
        {
            using (var connector = new BDConnector())
                return connector.GetListFromJsons<User>("UserIfo", "UserInfo");
            }

        public static void Add(params User[] users)
        {
            using (var connector = new BDConnector())
                foreach (var user in users)
                    connector.Add("UserIfo", Tuple.Create("ID", user.Email),
                        Tuple.Create("UserInfo", JsonConvert.SerializeObject(user)));
            }

        public static void Update(string mail, User user)
        {
            using (var connector = new BDConnector())
                connector.Update("UserIfo", "UserInfo", JsonConvert.SerializeObject(user), $"ID = \"{mail}\"");
            }
        }

    public static class MeetingsTable
    {
        public static List<Meeting> GetAll()
        {
            var meetings = new List<Meeting>();
            foreach (var id in GetIDs())
            {
                meetings.Add(GetById(id));
    }

            return meetings;
        }

        public static List<int> GetIDs()
        {
            using (var connector = new BDConnector())
                return connector.GetIntList("Meetings", "ID");
        }

        public static Meeting GetById(int id)
        {
            using (var connector = new BDConnector())
            {
                var condition = $"ID = {id}";
                var meeting = new Meeting()
                {
                    FirstUserId = connector.GetFirstString("Meetings", "ID", condition),
                    SecondUserId = connector.GetFirstString("Meetings", "FirstUserId", condition),
                    Date = connector.GetFirstDataTime("Meetings", "Date", condition),
                    FirstUserSetRate = connector.GetFirstOrDefaultBool("Meetings", "FirstUserSetRate", condition),
                    SecondUserSetRate = connector.GetFirstOrDefaultBool("Meetings", "SecondUserSetRate", condition)
                };

                return Contatins(meeting) ? meeting : null;
            }
        }

        public static bool Contatins(int id)
        {
            using (var connector = new BDConnector())
                return connector.GetFirstString("Meetings", "FirstUserID", $"ID = {id}") != null
                    &&connector.GetFirstString("Meetings", "SecondUserID", $"ID = {id}") != null;
        }

        private static bool Contatins(Meeting meeting)
        {
            return meeting.FirstUserId != null
                       && meeting.SecondUserId != null;
        }

        public static void Add(params Meeting[] meetings)
        {
            using (var connector = new BDConnector())
                foreach (var meeting in meetings)
                    connector.Add("Meetings", 
                        Tuple.Create("ID", meeting.Id.ToString()), 
                        Tuple.Create("FirstUserId", meeting.FirstUserId),
                        Tuple.Create("SecondUserId", meeting.SecondUserId),
                        Tuple.Create("Date", meeting.Date.ToString("yyyy-MM-dd HH:mm:ss")),
                        Tuple.Create("FirstUserSetRate", meeting.FirstUserSetRate ? "1" : "0"),
                        Tuple.Create("SecondUserSetRate", meeting.SecondUserSetRate ? "1" : "0"));
        }
    }
}
