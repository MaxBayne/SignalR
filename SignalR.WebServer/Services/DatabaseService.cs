using SignalR.WebServer.Models;

namespace SignalR.WebServer.Services
{
    public interface IDatabaseService
    {
        IDictionary<string,UserModel> Users { get; set; }

        void AddUser(UserModel user);
        void UpdateUser(UserModel user);
        void RemoveUser(UserModel user);
        void RemoveUser(string connectionId);

        UserModel FindUserByConnectionId(string connectionId);
        UserModel FindUserByEmail(string email);
        UserModel FindUserByName(string name);
    }

    public class DatabaseService: IDatabaseService
    {
        public IDictionary<string, UserModel> Users { get; set; }

        public DatabaseService()
        {
            Users = new Dictionary<string, UserModel>();
        }

        public void AddUser(UserModel user)
        {
            Users.Add(user.ConnectionId, user);
        }

        public void UpdateUser(UserModel user)
        {
            if (Users.ContainsKey(user.ConnectionId))
            {
                Users[user.ConnectionId]= user;
            }
        }

        public void RemoveUser(UserModel user)
        {
            if (Users.ContainsKey(user.ConnectionId))
            {
                Users.Remove(user.ConnectionId);
            }
        }

        public void RemoveUser(string id)
        {
            if (Users.ContainsKey(id))
            {
                Users.Remove(id);
            }
        }

        public void RemoveUserByConnectionId(string connectionId)
        {
            var user = FindUserByConnectionId(connectionId);
            RemoveUser(user);
        }

        public UserModel FindUserByConnectionId(string connectionId)
        {
            return (from userModel in Users where userModel.Value.ConnectionId == connectionId select userModel.Value).FirstOrDefault()!;
        }

        public UserModel FindUserByEmail(string email)
        {
            return (from userModel in Users where userModel.Value.Email == email select userModel.Value).FirstOrDefault()!;
        }

        public UserModel FindUserByName(string name)
        {
            return (from userModel in Users where userModel.Value.Name == name select userModel.Value).FirstOrDefault()!;
        }

    }
}
