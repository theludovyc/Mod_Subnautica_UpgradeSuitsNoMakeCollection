using System;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace UpgradeSuitsNoMakeCollection
{
    class Mod
    {
        public static void Load()
        {
            try
            {
                HarmonyInstance.Create("subnautica.upgradesuitsnomakecollection.mod").PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
