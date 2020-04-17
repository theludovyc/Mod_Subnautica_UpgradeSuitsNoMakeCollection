using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Crafting;
using Straitjacket.Harmony;
using Harmony;

namespace UpgradeSuitsNoMakeCollection
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Awake")]
    public class Patch_Player
    {
        static void Postfix(CrashHome __instance)
        {
            //rebreather
            var rebreatherTechData = CraftDataHandler.GetTechData(TechType.Rebreather);

            rebreatherTechData.Ingredients.Add(new Ingredient(TechType.RadiationHelmet, 1));

            CraftDataHandler.SetTechData(TechType.Rebreather, rebreatherTechData);

            //reinforcedSuit
            var reinforcedSuitTechData = CraftDataHandler.GetTechData(TechType.ReinforcedDiveSuit);

            reinforcedSuitTechData.Ingredients.Add(new Ingredient(TechType.RadiationSuit, 1));
            reinforcedSuitTechData.Ingredients.Add(new Ingredient(TechType.RadiationGloves, 1));

            CraftDataHandler.SetTechData(TechType.ReinforcedDiveSuit, reinforcedSuitTechData);
        }
    }
}
