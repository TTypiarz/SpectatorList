using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Hints;
using NorthwoodLib.Pools;
using SpectatorList.EventHandlers;

namespace SpectatorList.Patches;

[HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
public class HintRedirectPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

        newInstructions.InsertRange(newInstructions.FindLastIndex(x => x.opcode == OpCodes.Ldarg_0), new List<CodeInstruction>
        {
            new (OpCodes.Ldarg_0), 
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerEventsHandler), nameof(PlayerEventsHandler.ShowHint))),
            new (OpCodes.Ret)
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}