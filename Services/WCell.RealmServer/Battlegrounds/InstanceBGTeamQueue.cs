using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Constants;

namespace WCell.RealmServer.Battlegrounds
{
	public class InstanceBGTeamQueue : BattlegroundTeamQueue
	{
		public InstanceBGTeamQueue(InstanceBattlegroundQueue parentQueue, BattlegroundSide side)
			: base(parentQueue, side)
		{
		}

		public InstanceBattlegroundQueue InstanceQueue
		{
			get { return (InstanceBattlegroundQueue)_parentQueue; }
		}
	}
}