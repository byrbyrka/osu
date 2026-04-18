// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Game.Configuration;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModPerfect : ModPerfect
    {
        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(OsuModSpunOut)).ToArray();

        [SettingSource("Require spinner MAX bonus", "Fail if MAX bonus is not achieved on spinners.")]
        public BindableBool RequireSpinnerMax { get; } = new BindableBool();

        public override bool Ranked => base.Ranked && RequireSpinnerMax.IsDefault;

        protected override bool FailCondition(HealthProcessor healthProcessor, JudgementResult result)
        {
            if (base.FailCondition(healthProcessor, result))
                return true;

            if (RequireSpinnerMax.Value)
            {
                if (result.HitObject is SpinnerBonusTick && !result.IsHit)
                    return true;
            }

            return false;
        }
    }
}
