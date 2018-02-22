using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Entities
{
    public abstract class Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
    }
}