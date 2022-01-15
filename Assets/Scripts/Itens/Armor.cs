using UnityEngine;

namespace AlienArena.Itens
{
    [CreateAssetMenu(fileName = "Armor", menuName = "AlienArena/Itens/Armor")]
    public class Armor : Item
    {
        public ArmorType type;
        public float life;
        public float velocity;
        public float energy;
        
        public override void HandleEquip(SpriteRenderer renderer)
        {
            Base_HandleEquip(renderer);
        }
    }

    public enum ArmorType
    {
        Head,
        Torso,
        RightArm,
        LeftArm
    }
}