using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Entities
{
    public class GroupMembership
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public long EntityId { get; set; }
        public int EntityTypeId { get; set; }
        [NotMapped]
        public EntityType EntityType { get { return (EntityType)EntityTypeId; } }
    }
}