using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Strudle.Controllers
{
    public class SessionController : ApiController
    {        
        public static List<Session> SessionList;

        // GET api/Session/
        public List<Session> Get()
        {
            return SessionList;
        }

        // GET api/Session/id
        public Session Get(Guid id)
        {
            var find = SessionList.Find(session => session.Id == id);

            return find ?? new Session();
        }

        // GET api/Session/create/id
        // Skapar en ny session med UserId som parameter
        [HttpPost]
        [ActionName("Create")]
        public Guid CreateSession(int id, SessionInformation info)
        {
            if (SessionList == null)
            { 
                SessionList = new List<Session>();
            }

            var session = new Session
                {
                    Id = Guid.NewGuid(),
                    State = Session.Status.Created,
                    Name = info.Name,
                    ConnectedUsers = new List<User>() { new User() {Id = id}},
                    DataDefinition = null,
                    UserData = null
                };

            SessionList.Add(session);
            return session.Id;
        }

        // POST api/Session/Data/id
        // Skapar upp datadefinitionen för sessionen
        [HttpPost]
        [ActionName("Data")]
        public JObject CreateDataDefinition(Guid id, DataDefinition df)
        {
            var find = SessionList.Find(session => session.Id == id);

            if (find != null)
            {                
                find.DataDefinition = JObject.Parse(df.Data);

                var defaultDataForUsers = new List<JObject>();
                foreach (var connectedUser in find.ConnectedUsers)
                {
                    var parsedDefinition = JObject.Parse(df.Data);
                    parsedDefinition.Add("Id", connectedUser.Id);
                    defaultDataForUsers.Add(parsedDefinition);
                }

                find.State = Session.Status.InitData;
                find.UserData = defaultDataForUsers;
            }

            return null;
        }

        // POST api/Session/Connect/id
        [HttpPost]
        [ActionName("Connect")]
        public string ConnectToSession(Guid id, User user)
        {
            var find = SessionList.Find(session => session.Id == id); 

            if (find != null) 
            {
                find.ConnectedUsers.Add(user); 
                return "SUCCES MFS"; 
            }

            return "FALSE";
        }

        // POST api/Session/createSignalR/id
        [HttpPost]
        [ActionName("createSignalR")]
        public bool HostSignalRHub(Guid id, User user)
        {
            var find = SessionList.Find(session => session.Id == id);

            if (find != null)
            {
                find.State = Session.Status.HostingSignalR;
            }

            //TODO: skapa upp en signalR hub, returnera url/sätt/metod för att ansluta till den
            return true;
        }
    }

    public class SessionInformation
    {
        public String Name;
    }

    public class Session
    {
        public enum Status { Created, InitData, HostingSignalR };
        public Guid Id;
        public String Name;
        public Status State;
        public JObject DataDefinition;
        public List<JObject> UserData;
        public List<User> ConnectedUsers;
    }
    
    public class User
    {
        public int Id;
    }

    public class DataDefinition
    {
        public string Data;
    }
}
