using R3MUS.Devpack.SSO.IntelMap.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Entities
{
    public abstract class MemberEntity : Entity
    {
        public abstract EntityType EntityType { get; }
    }
}