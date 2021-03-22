using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _stackSize;

        public Sprite GetIcon()
        {
            return _icon;
        }
        public int GetStackSize()
        {
            return _stackSize;
        }
    }
}