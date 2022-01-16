using System;
using System.Collections;
using System.Collections.Generic;
using AlienArena.Itens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.Inventory
{
    [RequireComponent(typeof(Button))]
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameText;

        public Item StoredItem { get; private set; }
        private Button _button => GetComponent<Button>();

        
        public void SetItem(Item item, Action<ItemSlot> callback = null)
        {
            StoredItem = item;
            itemImage.sprite = item.sprite;
            itemImage.preserveAspect = true;
            itemNameText.text = item.name;
            
            _button.onClick.AddListener( delegate { callback?.Invoke(this); });
        }
    }
}
