﻿using WCell.RealmServer.AI;
using WCell.RealmServer.Entities;

namespace WCell.RealmServer.Spells.Auras.Mod
{
    /// <summary>
    /// Forces target to run away in fear.
    /// </summary>
    public class FearHandler : AuraEffectHandler
    {
        protected override void Apply()
        {
            if (Owner is Character)
            {
                var chr = (Character)Owner;
                chr.SetMover(Owner, false);
            }
            else if (Owner is NPC)
            {
                var npc = (NPC)Owner;
                npc.Brain.State = BrainState.Idle;
            }

            // TODO: Make unit run away instead of being stuck.

            Owner.IsInfluenced = true;
        }

        protected override void Remove(bool cancelled)
        {
            if (Owner is Character)
            {
                var chr = Owner as Character;
                chr.SetMover(Owner, true);
            }
            else if (Owner is NPC)
            {
                var npc = Owner as NPC;
                npc.Brain.EnterDefaultState();
            }

            Owner.IsInfluenced = false;
        }
    }
}
