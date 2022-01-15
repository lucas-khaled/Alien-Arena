using UnityEngine;

namespace AlienArena.Itens
{
    public abstract class Item : ScriptableObject
    {
        public Sprite sprite;
        public Material material;
    
        public abstract void HandleEquip(SpriteRenderer renderer);

        protected void Base_HandleEquip(SpriteRenderer renderer)
        {
            renderer.sprite = sprite;
            renderer.material = material;
        } 
    }
}
