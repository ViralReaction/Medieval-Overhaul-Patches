﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MO_Patches
{
    public class MayRequireCEAttribute : MayRequireAttribute
    {
        public MayRequireCEAttribute() : base("ceteam.combatextended")
        {
        }
    }
}
