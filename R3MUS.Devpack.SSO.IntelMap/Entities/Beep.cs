﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Entities
{
    public class Beep
    {
        public int Id { get; set; }
        public string Noise { get; set; }
        public ICollection<SystemGroup> SystemGroups { get; set; }
    }
}