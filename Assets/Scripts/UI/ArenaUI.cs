using System;
using System.Collections.Generic;
using AlienArena.Arena;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlienArena.UI
{
    public class ArenaUI : MonoBehaviour
    {
        [SerializeField] private ChallengeSlot slotsPrefab;
        [SerializeField] private Transform slotsContent;

        private ArenaSettings _openedArena;
        private List<ChallengeSlot> slotList = new List<ChallengeSlot>();
        private ChallengeSlot _selectedSlot;

        public void StartArenaChallenge()
        {
            Time.timeScale = 1;
            _openedArena.SetActualChallenge(_selectedSlot.ChallengeIndex);
            SceneManager.LoadScene("Arena");
        }

        public void OpenArena(ArenaSettings settings)
        {
            if(_selectedSlot != null)
                _selectedSlot.UnsetSelection();
            if(_openedArena == settings) return;
            
            _openedArena = settings;
            CreateSlots();
        }


        private void Start()
        {
            GamePauseUIController.instance.onArenaOpened += OpenArena;
        }

        private void CreateSlots()
        {
            slotList.Clear();
            Challenge[] challenges = _openedArena.challenges;
            for (int i = 0; i < challenges.Length; i++)
            {
                ChallengeSlot slot = Instantiate(slotsPrefab, slotsContent);
                slot.SetChallenge(challenges[i], i, SelectedSlot);
                slotList.Add(slot);
            }
        }

        private void SelectedSlot(ChallengeSlot slot)
        {
            if(_selectedSlot != null)
                _selectedSlot.UnsetSelection();
            
            slot.SetSelection();
            _selectedSlot = slot;
        }
    }
}