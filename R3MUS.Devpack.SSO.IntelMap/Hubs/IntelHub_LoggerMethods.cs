using R3MUS.Devpack.SSO.IntelMap.Entities;
using R3MUS.Devpack.SSO.IntelMap.Enums;
using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Hubs
{
    public partial class IntelHub
    {
        private static Dictionary<string, List<string>> _loggers = new Dictionary<string, List<string>>();

        public void ReportIntel(LogDataModel request)
        {
            Clients.Group(request.Group).pingIntel(StoreLogLines(request.LogLines));
        }

        public void JoinGroup(string groupName)
        {
            if (!_loggers.Any(w => w.Key == groupName))
            {
                _loggers.Add(groupName, new List<string>());
            }
            if (!_loggers[groupName].Any(w => w == Context.ConnectionId))
            {
                try
                {
                    _loggers[groupName].Add(Context.ConnectionId);
                }
                catch { }
                try
                {
                    Groups.Add(Context.ConnectionId, groupName);
                }
                catch { }
            }
            try
            {
                Clients.Group(groupName).pingGroupLoggerCount(_loggers[groupName].Count());
            }
            catch { }
        }

        public void LeaveGroups(List<string> groupNames)
        {
            groupNames.ForEach(groupName =>
            {
                _loggers[groupName].Remove(Context.ConnectionId);
                Clients.Group(groupName).pingGroupLoggerCount(_loggers[groupName].Count());
            });
        }

        public void SendLogFileNames(string connectionId)
        {
            var groups = _databaseContext.Groups.Where(w => !w.Disabled).ToList();
            var result = new List<GroupChannelName>();
            groups.ForEach(f => result.Add(new GroupChannelName()
            {
                Group = f.Name,
                Channels = _databaseContext.Channels.Where(w => w.GroupId == f.Id).Select(s => s.Name).ToList()
            }));
            Clients.Client(connectionId).fuckOff();
            Clients.Client(connectionId).sendLogFileNames(result);
        }

        private IEnumerable<LogLine> StoreLogLines(IEnumerable<LogLine> logLines)
        {
            if(_databaseContext.LogLines.Any())
            {
                var lastMessage = _databaseContext.LogLines.Max(w => w.LogDateTime);
                logLines = logLines.Where(w => w.LogDateTime > lastMessage).Distinct();
            }
            logLines = logLines.Where(w => !w.Message.ToUpper().Contains("CLR")
                && !w.Message.ToUpper().Contains("CLEAR")
                && !w.Message.ToUpper().Contains("STATUS"));
            _databaseContext.LogLines.AddRange(logLines);
            _databaseContext.SaveChanges();
            return logLines;
        }

        private string GetGroupName(LogDataModel request)
        {
            var group = new Group();
            if (_databaseContext.GroupMemberships.Any(w => w.EntityTypeId == (int)EntityType.Corporation && w.EntityId == request.CorporationId))
            {
                group.Id = _databaseContext.GroupMemberships.First(w => w.EntityTypeId == (int)EntityType.Corporation
                    && w.EntityId == request.CorporationId).GroupId;
            }
            else if (request.AllianceId.HasValue && _databaseContext.GroupMemberships.Any(w => w.EntityTypeId == (int)EntityType.Alliance && w.EntityId == request.AllianceId))
            {
                group.Id = _databaseContext.GroupMemberships.First(w => w.EntityTypeId == (int)EntityType.Alliance
                    && w.EntityId == request.AllianceId).GroupId;
            }
            group = _databaseContext.Groups.First(w => w.Id == group.Id && !w.Disabled);
            return group.Name;
        }
    }
}