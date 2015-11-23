﻿using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaXinZhao.Config.MiscMenu;
using SettingsModes = VodkaXinZhao.Config.ModesMenu;
using SettingsDrawing = VodkaXinZhao.Config.DrawingMenu;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao
{
    public static class Events
    {
        static Item Tiamat;
        static Item Hydra;

        private static float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        static Events()
        {
            Tiamat = new Item(ItemId.Tiamat_Melee_Only, 250);
            Hydra = new Item(ItemId.Ravenous_Hydra_Melee_Only, 250);

            Interrupter.OnInterruptableSpell += InterrupterOnOnInterruptableSpell;
            Orbwalker.OnAttack += OrbwalkerOnOnAttack;
            Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            Drawing.OnDraw += OnDraw;
        }

        private static void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            // Use Q
            // No sense in checking if Q is off cooldown or enemy died
            if (!SpellManager.Q.IsReady() || target.IsDead)
            {
                return;
            }
            // Check if we should use Q to attack heroes
            if ((SettingsModes.Combo.UseQ && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (Orbwalker.LaneClearAttackChamps && SettingsModes.LaneClear.UseE &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)))
            {
                if (target is AIHeroClient && PlayerMana >= SettingsMana.MinQMana)
                {
                    Debug.WriteChat("Casting Q, because attacking enemy in Combo or Harras");
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    return;
                }
            }
            // Check if we should use E to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (target is Obj_AI_Minion && PlayerMana >= SettingsMana.MinQMana)
                {
                    if (SettingsModes.JungleClear.UseQ && target.Team == GameObjectTeam.Neutral)
                    {
                        Debug.WriteChat("Casting Q, because attacking monster in Jungle Clear");
                        SpellManager.Q.Cast();
                        Orbwalker.ResetAutoAttack();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                        return;
                    }
                    else if (SettingsModes.LaneClear.UseQ && target.IsEnemy)
                    {
                        Debug.WriteChat("Casting Q, because attacking minion in Lane Clear");
                        SpellManager.Q.Cast();
                        Orbwalker.ResetAutoAttack();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                        return;
                    }
                }

            }

            // Tiamat/Hydra usage
            if ((SettingsModes.Combo.UseItems && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (SettingsModes.LaneClear.UseItems && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) ||
                (SettingsModes.JungleClear.UseItems && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)))
            {
                if (Item.HasItem(Tiamat.Id) && Item.CanUseItem(Tiamat.Id) && !target.IsDead && target.Distance(Player.Instance) < Tiamat.Range - 80)
                {
                    Debug.WriteChat("Using Tiamat.");
                    Tiamat.Cast();
                    return;
                }
                else if (Item.HasItem(Hydra.Id) && Item.CanUseItem(Hydra.Id) && !target.IsDead && target.Distance(Player.Instance) < Hydra.Range - 80)
                {

                    Debug.WriteChat("Using Hydra.");
                    Hydra.Cast();
                    return;
                }
            }
        }

        private static void OrbwalkerOnOnAttack(AttackableUnit target, EventArgs args)
        {
            // Use W
            // No sense in checking if W is off cooldown
            if (!SpellManager.W.IsReady())
            {
                return;
            }
            // Check if we should use E to attack heroes
            if ((SettingsModes.Combo.UseW && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (Orbwalker.LaneClearAttackChamps && SettingsModes.LaneClear.UseW &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)))
            {
                if (target is AIHeroClient && PlayerMana >= SettingsMana.MinWMana)
                {
                    Debug.WriteChat("Casting W, because attacking enemy in Combo or Harras");
                    SpellManager.W.Cast();
                    return;
                }
            }
            // Check if we should use E to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (target is Obj_AI_Minion && PlayerMana >= SettingsMana.MinWMana)
                {
                    if (SettingsModes.JungleClear.UseW && target.Team == GameObjectTeam.Neutral)
                    {
                        Debug.WriteChat("Casting W, because attacking monster in JungleClear");
                        SpellManager.W.Cast();
                    }
                    else if (SettingsModes.LaneClear.UseW && target.IsEnemy)
                    {
                        Debug.WriteChat("Casting W, because attacking minion in LaneClear");
                        SpellManager.W.Cast();
                    }
                }

            }
        }

        public static void Initialize()
        {

        }

        private static void OnDraw(EventArgs args)
        {
            if (SettingsDrawing.DrawE)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.E.IsReady()))
                {
                    Circle.Draw(Color.LightBlue, SpellManager.E.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawR)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.R.IsReady()))
                {
                    Circle.Draw(Color.Yellow, SpellManager.R.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawIgnite && SpellManager.HasIgnite())
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.Ignite.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.Ignite.Range, Player.Instance.Position);
                }
            }
        }


        private static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy || !(sender is AIHeroClient) || Player.Instance.IsRecalling())
            {
                Debug.WriteChat("Detected Interruptable spell from {0}, but didn't meet criteria.", sender.Name);
                return;
            }
            Debug.WriteChat("Interruptable Spell from {0}", ((AIHeroClient)sender).ChampionName);
            if (SettingsMisc.InterrupterUseR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(sender))
            {
                Debug.WriteChat("Interrupting with R, Target: {0}, Distance: {1}", ((AIHeroClient)sender).ChampionName, "" + sender.Distance(Player.Instance));
                SpellManager.R.Cast();
            }
        }
    }
}