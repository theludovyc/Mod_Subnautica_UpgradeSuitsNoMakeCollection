using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Straitjacket.Harmony;
using UnityEngine;

namespace UpgradeSuitsNoMakeCollection
{
    [HarmonyPatch(typeof(RadiatePlayerInRange))]
    [HarmonyPatch("Radiate")]
    class Patch_RadiatePlayerInRange
    {
        static float radiateRadius_tmp;

        static bool Prefix(RadiatePlayerInRange __instance)
        {
            radiateRadius_tmp = __instance.radiateRadius;

            __instance.radiateRadius = -1f;

            return true;
        }

		static bool checkItem(TechType tt)
		{
			return Inventory.main.equipment.GetCount(tt) > 0;
		}

		static bool checkHelmet()
		{
			if (checkItem(TechType.RadiationHelmet) || checkItem(TechType.Rebreather))
			{
				return true;
			}
			return false;
		}

		static bool checkSuit()
		{
			if (checkItem(TechType.RadiationSuit) || checkItem(TechType.ReinforcedDiveSuit))
			{
				return true;
			}
			return false;
		}

		static bool checkGloves()
		{
			if (checkItem(TechType.RadiationGloves) || checkItem(TechType.ReinforcedGloves))
			{
				return true;
			}
			return false;
		}

        static void Postfix(RadiatePlayerInRange __instance)
        {
			__instance.radiateRadius = radiateRadius_tmp;

			var tracker = __instance.GetComponent<PlayerDistanceTracker>();

			bool flag = GameModeUtils.HasRadiation() && (NoDamageConsoleCommand.main == null || !NoDamageConsoleCommand.main.GetNoDamageCheat());
			
			float distanceToPlayer = tracker.distanceToPlayer;
			
			if (distanceToPlayer <= __instance.radiateRadius && flag && __instance.radiateRadius > 0f)
			{
				float num = Mathf.Clamp01(1f - distanceToPlayer / __instance.radiateRadius);
				
				float num2 = num;

				if (checkHelmet())
				{
					num -= num2 * 0.23f * 2f;
				}

				if (checkSuit())
				{
					num -= num2 * 0.5f;
				}
				
				if (checkGloves())
				{
					num -= num2 * 0.23f;
				}
				
				num = Mathf.Clamp01(num);
				
				Player.main.SetRadiationAmount(num);
			}
		}
    }
}
