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
        
        protected override void Loaded(Harmony harmony)
        {
            Type fleetwindow = AuroraAssembly.GetType("fs");
            IEnumerable<MethodInfo> alltheas = fleetwindow.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(a => a.Name == "a");
            HarmonyMethod subGFbydefaultPFMeth = new HarmonyMethod(GetType().GetMethod("SubGFPostfix", AccessTools.all));
            MethodInfo methodtopostfix = null;

            foreach (MethodInfo thismethod in alltheas)
            {
                if (thismethod.GetParameters().Length == 0)
                {
                    if (thismethod.ReturnType == typeof(void)) {
                        methodtopostfix = thismethod;
                    }
                }
            }

            if (methodtopostfix != null)
            {
                LogInfo("Now patching fs.cs fleet window");
                harmony.Patch(methodtopostfix, postfix: subGFbydefaultPFMeth);
            }
            else
            {
                LogInfo("Didn't patch fs.cs fleet window as methodtopostfix is null");
            }



     
            
        }

        private static void SubGFPostfix(Form __instance)
        {
            CheckBox SubGF = __instance.Controls.Find("chkLoadSubUnits", true)[0] as CheckBox;
            SubGF.Checked = true;
        }
    }
}
