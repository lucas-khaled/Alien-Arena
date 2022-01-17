using System;
using AlienArena.Arena;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.UI
{
    [RequireComponent(typeof(Button))]
    public class ChallengeSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text enemyCountText;
        [SerializeField] private GameObject completedGameObject;
        [SerializeField] private GameObject selectedGameObject;

        public int ChallengeIndex { get; private set; }

        private Challenge _challenge;
        private Button _button => GetComponent<Button>();

        public void SetChallenge(Challenge challenge, int index, Action<ChallengeSlot> callback = null)
        {
            _challenge = challenge;
            
            rewardText.SetText("Reward: "+_challenge.coinsReward+" Coins");
            nameText.SetText(_challenge.challengeName);
            enemyCountText.SetText("Total enemies: "+_challenge.GetEnemiesTotalCount());
            
            completedGameObject.SetActive(_challenge.completed);

            ChallengeIndex = index;
            
            _button.onClick.AddListener(delegate { callback?.Invoke(this); });
        }

        public void SetSelection()
        {
            selectedGameObject.SetActive(true);
        }

        public void UnsetSelection()
        {
            selectedGameObject.SetActive(false);
        }
    }
}