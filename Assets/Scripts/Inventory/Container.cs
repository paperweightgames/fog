using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Container : MonoBehaviour
    {
        [SerializeField] private int _capacity;
        private readonly List<Slot> _slots = new List<Slot>();

        private int GetSpace()
        {
            return _capacity - _slots.Count;
        }
        
        public void AddItem(Item item, int amount)
        {
            // Add item to existing slots.
            foreach (var slot in _slots)
            {
                // Stop if no more items to add.
                if (amount <= 0)
                    return;
                // Skip if items don't match.
                if (slot.GetItem() != item)
                    continue;
                var slotQuantity = slot.GetQuantity();
                var space = item.GetStackSize() - slot.GetQuantity();
                // Skip the item if no space left.
                if (space < 0)
                    continue;
                // Add the items to the slot.
                var amountToAdd = Mathf.Max(space, amount);
                slot.SetQuantity(slotQuantity + amountToAdd);
                amount -= amountToAdd;
            }
            // Add item to new slots.
            while (amount > 0 || GetSpace() > 0)
            {
                // Create a new slot.
                var quantity = Mathf.Max(item.GetStackSize(), amount);
                amount -= quantity;
                var newSlot = new Slot(item, quantity);
                _slots.Add(newSlot);
            }
        }

        public void RemoveItem(Item item, int amount)
        {
            // Go through the slots in the container.
            for (var s = 0; s < _slots.Count; s++)
            {
                // Stop if no more items to remove.
                if (amount <= 0)
                    return;
                // Skip if items don't match.
                if (_slots[s].GetItem() != item)
                    continue;
                var quantity = _slots[s].GetQuantity();
                // Remove slot if quantity is too small.
                if (quantity <= amount)
                {
                    amount -= quantity;
                    _slots.RemoveAt(s);
                    s--;
                }
                // Remove from quantity otherwise.
                else
                {
                    _slots[s].SetQuantity(quantity - amount);
                }
            }
        }
    }
}