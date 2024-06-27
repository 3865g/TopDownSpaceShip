
using Scripts.Logic.EnemySpawners;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Logic
{
    public class RewardsManager : MonoBehaviour
    {
        public List<Rewards> rewardList = new List<Rewards>();

        public void RegisterEnemy(int groupId)
        {
            if (!rewardList.Any(x => x.GroupId == groupId))
            {
                rewardList.Add(new Rewards { GroupId = groupId, EnemyCount = 1 });
            }
            else
            {
                UpdateElement(groupId, 1);
            }


        }

        public void UpdateElement(int groupid, int enemyCount)
        {
            var element = rewardList.FirstOrDefault(x => x.GroupId == groupid);
            element.EnemyCount = element.EnemyCount + enemyCount;

            if (element.EnemyCount == 0)
            {
                SendReward();
            }
        }

        public void UpdateEnemyCount(int groupId)
        {
            UpdateElement(groupId, -1);

        }

        public void SendReward()
        {
            Debug.LogError("SendReward");
        }
    }
}


