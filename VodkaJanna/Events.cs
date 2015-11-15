﻿using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaJanna.Config.MiscMenu;
using SettingsModes = VodkaJanna.Config.ModesMenu;

namespace VodkaJanna
{
    public static class Events
    {
        static Events()
        {
            Interrupter.OnInterruptableSpell += InterrupterOnOnInterruptableSpell;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            Drawing.OnDraw += OnDraw;
        }

        public static void Initialize()
        {

        }

        private static void OnDraw(EventArgs args)
        {
            if (Config.DrawingMenu.DrawQ)
            {
                Circle.Draw(Color.Cyan, SpellManager.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawingMenu.DrawQMax)
            {
                Circle.Draw(Color.Cyan, 1700, Player.Instance.Position);
            }
            if (Config.DrawingMenu.DrawW)
            {
                Circle.Draw(Color.Magenta, SpellManager.W.Range, Player.Instance.Position);
            }
            if (Config.DrawingMenu.DrawE)
            {
                Circle.Draw(Color.White, SpellManager.E.Range, Player.Instance.Position);
            }
            if (Config.DrawingMenu.DrawR)
            {
                Circle.Draw(Color.Yellow, SpellManager.E.Range, Player.Instance.Position);
            }
        }

        private static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy)
            {
                return;
            }
            Debug.WriteChat("Interruptable Spell from {0}", sender.Name);
            if (SettingsMisc.InterrupterUseQ && SpellManager.Q.IsReady() && sender.IsEnemy && SpellManager.Q.IsInRange(sender))
            {
                Debug.WriteChat("Interrupting with Q, Target: {0}, Distance: {1}", sender.Name, ""+sender.Distance(Player.Instance));
                SpellManager.Q.Cast(sender);
                Core.DelayAction(() => { SpellManager.Q.Cast(sender); }, 10);
            }
            if (SettingsMisc.InterrupterUseR && SpellManager.R.IsReady() && sender.IsEnemy && SpellManager.R.IsInRange(sender))
            {
                Debug.WriteChat("Interrupting with R, Target: {0}, Distance: {1}", sender.Name, "" + sender.Distance(Player.Instance));
                SpellManager.R.Cast();
            }
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloserEventArgs)
        {
            if (!sender.IsEnemy)
            {
                return;
            }
            if (SettingsMisc.AntigapcloserUseQ && SpellManager.Q.IsReady() && gapcloserEventArgs.End.Distance(Player.Instance) < 200)
            {
                Debug.WriteChat("AntiGapcloser with Q, Target: {0}, Distance: {1}, GapcloserSpell: {2}", sender.Name, "" + sender.Distance(Player.Instance), gapcloserEventArgs.SpellName);
                SpellManager.Q.Cast(gapcloserEventArgs.End);
                Core.DelayAction(() => { SpellManager.Q.Cast(gapcloserEventArgs.End); }, 10);
            }
            if (SettingsMisc.AntigapcloserUseR && !SpellManager.R.IsOnCooldown && SpellManager.R.IsInRange(sender))
            {
                Debug.WriteChat("AntiGapcloser with R, Target: {0}, Distance: {1}, GapcloserSpell: {2}", sender.Name, "" + sender.Distance(Player.Instance), gapcloserEventArgs.SpellName);
                SpellManager.R.Cast();
            }
        }

        private static void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            // No sense in checking if E is off cooldown
            if (SpellManager.E.IsOnCooldown)
            {
                return;
            }
            // Check if we should use E to attack heroes
            if ((SettingsModes.Combo.UseE && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (SettingsModes.Harass.UseE && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) ||
                (Orbwalker.LaneClearAttackChamps && SettingsModes.LaneClear.UseE &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)))
            {
                if (target is AIHeroClient && SpellManager.E.IsReady())
                {
                    Debug.WriteChat("Casting E, because attacking enemy in Combo or Harras");
                    SpellManager.E.Cast(Player.Instance);
                    return;
                }
            }
            // Check if we should use E to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (SpellManager.E.IsReady())
                {
                    if (target is Obj_AI_Minion && target.Team == GameObjectTeam.Neutral && SettingsModes.JungleClear.UseE)
                    {
                        Debug.WriteChat("Casting E, because attacking monster in JungleClear");
                        SpellManager.E.Cast(Player.Instance);
                    }
                    else if (target is Obj_AI_Minion && target.IsEnemy && SettingsModes.LaneClear.UseE)
                    {
                        Debug.WriteChat("Casting E, because attacking minion in LaneClear");
                        SpellManager.E.Cast(Player.Instance);
                    }
                }

            }
        }
    }
}
