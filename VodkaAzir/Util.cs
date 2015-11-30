﻿using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace VodkaAzir
{
    public static class Util
    {

        public static Slider CreateHitChanceSlider(string identifier, string displayName, HitChance defaultValue, Menu menu)
        {
            var slider = menu.Add(identifier, new Slider(displayName, 5, 0, 8));
            var hcNames = new[]
            {"Unknown", "Impossible", "Collision", "Low", "AveragePoint", "Medium", "High", "Dashing", "Immobile"};
            slider.CurrentValue = (int)defaultValue;
            slider.DisplayName = hcNames[slider.CurrentValue];
            slider.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = hcNames[changeArgs.NewValue];
                };
            return slider;
        }

        public static HitChance GetHitChanceSliderValue(Slider slider)
        {
            if (slider == null)
            {
                return HitChance.Impossible;
            }
            var currVal = slider.CurrentValue;
            return (HitChance)currVal;
        }
    }
}
