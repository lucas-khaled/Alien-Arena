using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlienArena.Itens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.UI
{
    public class DescriptionUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text energyText;
        [SerializeField] private TMP_Text lifeText;
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text velocityText;
        [SerializeField] private TMP_Text bulletsText;
        [SerializeField] private TMP_Text fireRateText;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text priceText;

        private List<TMP_Text> allTexts = new List<TMP_Text>();
        
        public void SetDescription(Item item)
        {
            ClearDescription();
            
            Type weaponType = typeof(Weapon);
            Type itemType = item.GetType();
            
            if (itemType == weaponType || itemType.IsSubclassOf(weaponType))
            {
                Weapon weapon = (Weapon) item;
                
                damageText.gameObject.SetActive(true);
                damageText.SetText("Damage: "+weapon.damage);

                bulletsText.gameObject.SetActive(true);
                bulletsText.SetText("Bullets: "+weapon.bullets);
                
                fireRateText.gameObject.SetActive(true);
                fireRateText.SetText("Fire Rate: "+weapon.fireRate);
            }
            else
            {
                Armor armor = (Armor) item;

                energyText.gameObject.SetActive(true);
                energyText.SetText("Energy: "+armor.energy);

                lifeText.gameObject.SetActive(true);
                lifeText.SetText("Life: "+armor.life);
                
                velocityText.gameObject.SetActive(true);
                velocityText.SetText("Velocity: "+armor.velocity);
            }

            itemImage.sprite = item.sprite;
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.preserveAspect = true;
            
            itemNameText.gameObject.SetActive(true);
            itemNameText.SetText(item.name);
            
            priceText.gameObject.SetActive(true);
            priceText.SetText("Price: "+item.price);
        }

        public void ClearDescription()
        {
            foreach (var text in allTexts)
            {
                text.SetText(String.Empty);
                text.gameObject.SetActive(false);
            }

            itemImage.sprite = null;
            itemImage.color = new Color(0,0,0,0);
        }

        private void Awake()
        {
            foreach (var field in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).
                Where(x => x.FieldType == typeof(TMP_Text)))
            {
                TMP_Text text = (TMP_Text) field.GetValue(this);
                allTexts.Add(text); 
            }
        }
    }
}