using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using R3MUS.Devpack.SSO.IntelMap.Enums;

namespace R3MUS.Devpack.SSO.IntelMap.Entities
{
    public class Corporation : MemberEntity
    {
        public override EntityType EntityType
        {
            get
            {
                return EntityType.Corporation;
            }
        }
    }
}