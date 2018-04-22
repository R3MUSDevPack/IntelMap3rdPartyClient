using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Hubs
{
    public partial class IntelHub
    {
        private static Dictionary<string, List<string>> _users = new Dictionary<string, List<string>>();

        public void SendMeUserCount()
        {
            SendUserCounts(GetGroupName());
        }

        private void SendUserCounts(string groupName)
        {
            Clients.Group(groupName).pingGroupUserCount(_users[groupName].Count());
            Clients.Group(groupName).pingLoggerCount(_loggers.ContainsKey(groupName) ? _loggers[groupName].Count() : 0);
            Clients.All.pingUserCount(_users.Sum(s => s.Value.Count()));
        }
        
        public void SendMeHistory()
        {
            var cutoffTime = DateTime.Now.AddMinutes(-3);
            var groupName = _users.First(w => w.Value.Contains(Context.User.Identity.Name)).Key;
            Clients.Caller.pingIntel(_databaseContext.LogLines.Where(w => w.LogDateTime > cutoffTime
                && w.Group == groupName));
        }

        private void SendSystemBeepData(string groupName)
        {
            var group = _databaseContext.Groups.First(w => w.Name == groupName);
            var systemGroups = _databaseContext.SystemGroups.Where(w => w.GroupId == group.Id);
            var beeps = _databaseContext.Beeps.Where(w => systemGroups.Select(s => s.BeepId).Contains(w.Id)).ToList();

            var beepData = new List<BeepData>();
            beeps.ForEach(f => beepData.Add(new BeepData() { Noise = f.Noise, SystemNames = systemGroups.Where(w => w.BeepId == f.Id).Select(s => s.SystemName) }));

            Clients.Caller.SendSystemBeepData(beepData);
        }

        private void JoinGroup()
        {
            var groupName = GetGroupName();

            if (groupName != null && _databaseContext.Groups.Any(w => w.Name == groupName && !w.Disabled))
            {
                Groups.Add(Context.ConnectionId, groupName);
                if (!_users.ContainsKey(groupName))
                {
                    _users.Add(groupName, new List<string>());
                }
                if (!_users[groupName].Contains(Context.User.Identity.Name))
                {
                    _users[groupName].Add(Context.User.Identity.Name);
                    SendSystemBeepData(groupName);
                    SendUserCounts(groupName);
                }
            }
            else
            {
                SendLogFileNames(Context.ConnectionId);
            }
        }

        private void LeaveGroup()
        {
            var groupName = GetGroupName();
            if (groupName != null)
            {
                _users[groupName].Remove(Context.User.Identity.Name);
                Groups.Remove(Context.ConnectionId, groupName);
                SendUserCounts(groupName);
            }
        }
    }
}