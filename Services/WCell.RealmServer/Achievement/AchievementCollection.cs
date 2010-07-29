﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using WCell.Constants.Achievements;
using WCell.RealmServer.Entities;

namespace WCell.RealmServer.Achievement
{
    // AchievementCollection class which each Character has an instance of
    /// <summary>
    /// Represents the Player's Achievements.
    /// </summary>
    public class AchievementCollection
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

		internal Dictionary<AchievementEntryId, AchievementRecord> m_completedAchievements = new Dictionary<AchievementEntryId, AchievementRecord>();
		internal Dictionary<AchievementEntryId, AchievementProgressRecord> m_achivement_progress = new Dictionary<AchievementEntryId, AchievementProgressRecord>();
        internal Character m_owner;
        
        public AchievementCollection(Character chr)
        {
            m_owner = chr;
        }

        public bool HasCompleted(AchievementEntryId achievementEntry)
        {
            return m_completedAchievements.ContainsKey(achievementEntry);
        }

        #region Props
        /// <summary>
        /// Returns the Achievement's Owner.
        /// </summary>
        public Character Owner
        {
            get { return m_owner; }
        }

        /// <summary>
        /// Returns the amount of achievements in the list.
        /// </summary>
        public int AchievementsCount
        {
            get { return m_completedAchievements.Count; }
        }

        /// <summary>
        /// Sets or overrides an existing achievement record;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AchievementRecord this[AchievementEntryId key]
        {
            get
            {
                AchievementRecord achievementRecord;
                m_completedAchievements.TryGetValue(key, out achievementRecord);
                return achievementRecord;
            }
            set
            {
                if (m_completedAchievements.ContainsKey(key))
                    Remove(key);
                Add(value);
            }
        }
        #endregion

        #region Add / Set

		public void Add(AchievementRecord achievementEntryId)
		{
			m_completedAchievements.Add(achievementEntryId.AchievementEntryId, achievementEntryId);
		}

        #endregion

        #region Remove

		public bool Remove(AchievementEntryId achievementEntry)
		{
			AchievementRecord achievementRecord;
			if(m_completedAchievements.TryGetValue(achievementEntry, out achievementRecord))
			{
				Remove(achievementRecord);
				return true;
			}
			return false;
		}

		public void Remove(AchievementRecord achievementRecord)
		{
			m_completedAchievements.Remove(achievementRecord.AchievementEntryId);
		}

        #endregion

		public void Load()
		{
			foreach (var mCompletedAchievement in AchievementRecord.Load(Owner.EntityId.Low))
			{
				var achievement = AchievementMgr.Get(mCompletedAchievement.AchievementEntryId);
				if(achievement!= null)
				{
					if (m_completedAchievements.ContainsKey(achievement.ID))
					{
						log.Warn("Character {0} had Achievement {1} more than once.", m_owner, achievement.ID);
					}
					else
					{
						m_completedAchievements.Add(achievement.ID, mCompletedAchievement);
					}
				}
				else
				{
					log.Warn("Character {0} has invalid Achievement: {1}", m_owner, mCompletedAchievement.AchievementEntryId);
				}
			}
		}
    }
}