﻿using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaWarwick.Config.ModesMenu.Flee;

namespace VodkaWarwick.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            if (Settings.UseE && E.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1)
            {
                E.Cast();
                Debug.WriteChat("Casting E in Flee");
            }
        }
    }
}
