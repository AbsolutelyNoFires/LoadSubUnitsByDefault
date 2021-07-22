using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Aurora;
using Aurora.Properties;
using Lib;
using HarmonyLib;

namespace SubGFByDefault
{
    public class SubGFByDefault : AuroraPatch.Patch
    {
        public override string Description => "Load All Sub-Units defaults to ticked with this mod active.";
        public override IEnumerable<string> Dependencies => new[] { "Lib" };

        protected override void Loaded(Harmony harmony)
        {
            var lib = GetDependency<Lib.Lib>("Lib");
            lib.RegisterEventHandler(AuroraType.FleetWindowForm, "Shown", GetType().GetMethod("ShownHandler", AccessTools.all));
        }

        private static void ShownHandler(object sender, EventArgs e)
        {
            var subgf = UIManager.GetControlByName<CheckBox>((Form)sender, "chkLoadSubUnits");
            subgf.Checked = true;
        }
    }
}
