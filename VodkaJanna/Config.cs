﻿using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaJanna
{
    public static class Config
    {
        private const string MenuName = "VodkaJanna";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaJanna");
            Menu.AddLabel("Created by Haker");
            Menu.AddLabel("Feel free to send me any suggestions you might have.");
            Modes.Initialize();
            Misc.Initialize();
            Drawing.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu MenuModes;

            static Modes()
            {
                MenuModes = Config.Menu.AddSubMenu("Modes");

                Combo.Initialize();
                MenuModes.AddSeparator();

                Harass.Initialize();
                MenuModes.AddSeparator();

                LaneClear.Initialize();
                MenuModes.AddSeparator();

                JungleClear.Initialize();
                MenuModes.AddSeparator();

                LastHit.Initialize();
                MenuModes.AddSeparator();

                Flee.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Combo()
                {
                    MenuModes.AddGroupLabel("Combo");
                    _useQ = MenuModes.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("comboUseE", new CheckBox("Use E (on self)", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Harass()
                {
                    MenuModes.AddGroupLabel("Harass");
                    _useQ = MenuModes.Add("harassUseQ", new CheckBox("Use Q", false));
                    _useW = MenuModes.Add("harassUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("harassUseE", new CheckBox("Use E (on self)", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _minQTargets;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int MinQTargets
                {
                    get { return _minQTargets.CurrentValue; }
                }

                static LaneClear()
                {
                    MenuModes.AddGroupLabel("LaneClear");
                    _useQ = MenuModes.Add("laneUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("laneUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("laneUseE", new CheckBox("Use E (on self)"));
                    _minQTargets = MenuModes.Add("minQTargetsLC", new Slider("Minimum targets for Q", 4, 1, 10));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useW;

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }


                static LastHit()
                {
                    MenuModes.AddGroupLabel("LastHit");
                    _useW = MenuModes.Add("lasthitUseW", new CheckBox("Use W", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _minQTargets;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int MinQTargets
                {
                    get { return _minQTargets.CurrentValue; }
                }

                static JungleClear()
                {
                    MenuModes.AddGroupLabel("JungleClear");
                    _useQ = MenuModes.Add("jungleUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("jungleUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("jungleUseE", new CheckBox("Use E (on self)"));
                    _minQTargets = MenuModes.Add("minQTargetsJC", new Slider("Minimum targets for Q", 2, 1, 10));
                }

                public static void Initialize()
                {
                }


            }

            public static class Flee
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                static Flee()
                {
                    MenuModes.AddGroupLabel("Flee");
                    _useQ = MenuModes.Add("fleeUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("fleeUseW", new CheckBox("Use W"));
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class Misc
        {
            private static readonly Menu MenuMisc;
            private static readonly CheckBox _interrupterQ;
            private static readonly CheckBox _interrupterR;
            private static readonly CheckBox _antigapcloserQ;
            private static readonly CheckBox _antigapcloserR;
            private static readonly CheckBox _autoR;
            private static readonly CheckBox _potion;
            private static readonly Slider _potionMinHP;
            private static readonly Slider _potionMinMP;
            private static readonly Slider _autoRMinHP;
            private static readonly Slider _autoRMinEnemies;

            public static bool InterrupterUseQ
            {
                get { return _interrupterQ.CurrentValue; }
            }
            public static bool InterrupterUseR
            {
                get { return _interrupterR.CurrentValue; }
            }
            public static bool AntigapcloserUseQ
            {
                get { return _antigapcloserQ.CurrentValue; }
            }
            public static bool AntigapcloserUseR
            {
                get { return _antigapcloserR.CurrentValue; }
            }
            public static bool AutoR
            {
                get { return _autoR.CurrentValue; }
            }
            public static int AutoRMinHP
            {
                get { return _autoRMinHP.CurrentValue; }
            }
            public static int AutoRMinEnemies
            {
                get { return _autoRMinEnemies.CurrentValue; }
            }
            public static bool Potion
            {
                get { return _potion.CurrentValue; }
            }
            public static int potionMinHP
            {
                get { return _potionMinHP.CurrentValue; }
            }
            public static int potionMinMP
            {
                get { return _potionMinMP.CurrentValue; }
            }

            static Misc()
            {
                MenuMisc = Config.Menu.AddSubMenu("Misc");
                MenuMisc.AddGroupLabel("Interrupter");
                _interrupterQ = MenuMisc.Add("interrupterUseQ", new CheckBox("Use Q to interrupt spells"));
                _interrupterR = MenuMisc.Add("interrupterUseR", new CheckBox("Use R to interrupt spells", false));
                MenuMisc.AddGroupLabel("Anti Gapcloser");
                _antigapcloserQ = MenuMisc.Add("antigapcloserUseQ", new CheckBox("Use Q against gapclosers"));
                _antigapcloserR = MenuMisc.Add("antigapcloserUseR", new CheckBox("Use R against gapclosers", false));
                MenuMisc.AddGroupLabel("Auto R usage");
                _autoR = MenuMisc.Add("autoR", new CheckBox("Use R automatically when ally is low HP", false));
                _autoRMinHP = MenuMisc.Add("autoRMinHP", new Slider("Minimum ally HP % to ult", 15));
                _autoRMinEnemies = MenuMisc.Add("autoRMinEnemies", new Slider("Minimum enemies around to ult", 1, 0, 5));
                MenuMisc.AddGroupLabel("Auto pot usage");
                _potion = MenuMisc.Add("potion", new CheckBox("Use potions"));
                _potionMinHP = MenuMisc.Add("potionminHP", new Slider("Minimum Health % to use potion", 30));
                _potionMinMP = MenuMisc.Add("potionMinMP", new Slider("Minimum Mana % to use potion", 20));
            }

            public static void Initialize()
            {
            }
        }

        public static class Drawing
        {
            private static readonly Menu MenuDrawing;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawQMax;
            private static readonly CheckBox _drawW;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }
            public static bool DrawQMax
            {
                get { return _drawQMax.CurrentValue; }
            }
            public static bool DrawW
            {
                get { return _drawW.CurrentValue; }
            }
            public static bool DrawE
            {
                get { return _drawE.CurrentValue; }
            }
            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
            }

            static Drawing()
            {
                MenuDrawing = Config.Menu.AddSubMenu("Drawing");
                _drawQ = MenuDrawing.Add("drawQ", new CheckBox("Draw Q"));
                _drawQMax = MenuDrawing.Add("drawQMax", new CheckBox("Draw Max Q Range"));
                _drawW = MenuDrawing.Add("drawW", new CheckBox("Draw W"));
                _drawE = MenuDrawing.Add("drawE", new CheckBox("Draw E"));
                _drawR = MenuDrawing.Add("drawR", new CheckBox("Draw R"));
            }

            public static void Initialize()
            {
            }
        }
    }
}
