#region LICENSE

// Copyright 2014 - 2014 Support
// FiddleSticks.cs is part of Support.
// Support is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// Support is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with Support. If not, see <http://www.gnu.org/licenses/>.

#endregion

#region

using System;
using LeagueSharp;
using LeagueSharp.Common;

#endregion

namespace Support.Plugins
{
    public class FiddleSticks : PluginBase
    {
        public FiddleSticks()
        {
            Q = new Spell(SpellSlot.Q, 575);
            W = new Spell(SpellSlot.W, 575);
            E = new Spell(SpellSlot.E, 750);
            R = new Spell(SpellSlot.R, 800);
        }

        public override void OnUpdate(EventArgs args)
        {
            if (ComboMode)
            {
                if (Q.CastCheck(Target, "Combo.Q"))
                {
                    Q.CastOnUnit(Target, UsePackets);
                }

                if (E.CastCheck(Target, "Combo.E"))
                {
                    E.CastOnUnit(Target, UsePackets);
                }
            }

            if (HarassMode)
            {
                if (E.CastCheck(Target, "Harass.E"))
                {
                    E.CastOnUnit(Target, UsePackets);
                }
            }
        }

        public override void OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (gapcloser.Sender.IsAlly)
                return;

            if (Q.CastCheck(gapcloser.Sender, "Gapcloser.Q"))
            {
                Q.CastOnUnit(gapcloser.Sender, UsePackets);
            }
        }

        public override void OnPossibleToInterrupt(Obj_AI_Base unit, InterruptableSpell spell)
        {
            if (spell.DangerLevel < InterruptableDangerLevel.High || unit.IsAlly)
                return;

            if (Q.CastCheck(unit, "Interrupt.Q"))
            {
                Q.CastOnUnit(unit, UsePackets);
                return;
            }

            if (E.CastCheck(unit, "Interrupt.E"))
            {
                E.CastOnUnit(unit, UsePackets);
            }
        }

        public override void ComboMenu(Menu config)
        {
            config.AddBool("Combo.Q", "使用 Q", true);
            config.AddBool("Combo.E", "使用 E", true);
        }

        public override void HarassMenu(Menu config)
        {
            config.AddBool("Harass.E", "使用 E", true);
        }

        public override void InterruptMenu(Menu config)
        {
            config.AddBool("Gapcloser.Q", "使用 Q 防突进ㄧ", true);

            config.AddBool("Interrupt.Q", "使用 Q 打断", true);
            config.AddBool("Interrupt.E", "使用 E 打断", true);
        }
    }
}