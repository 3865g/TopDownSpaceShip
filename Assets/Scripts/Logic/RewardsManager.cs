
using Scripts.UI.Services.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Logic
{
    public class RewardsManager : MonoBehaviour
    {
        public List<Rewards> rewardList = new List<Rewards>();

        private IWindowService _windowService;

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

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
                SendReward(groupid);
            }
        }

        public void UpdateEnemyCount(int groupId)
        {
            UpdateElement(groupId, -1);

        }

        public void SendReward(int groupid)
        {
            if (groupid < 10)
            {
                FillingAwards();

                Debug.LogError("Send simple Reward");
            }
            else
            {
                Debug.LogError("Send boss Reward");
            }
        }

        public void FillingAwards()
        {
            Time.timeScale = 0f;
            Debug.LogError(Time.timeScale.ToString());
            _windowService.Open(WindowId.Rewards);
        }
    }
}


