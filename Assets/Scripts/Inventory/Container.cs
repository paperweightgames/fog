using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Container : MonoBehaviour
    {
        [SerializeField] private int _capacity;
        private Slot[] _slots;
        public Action Updated;

        private void Awake()
        {
            // Initialise slots.
            _slots = new Slot[_capacity];
            for (var s = 0; s < _slots.Length; s++)
                _slots[s] = new Slot();
        }

        public int GetCapacity()
        {
            return _capacity;
        }

        public IEnumerable<Slot> GetSlots()
        {
            return _slots;
        }
        private int GetSpace()
        {
            return _capacity - _slots.Length;
        }
        
        public int AddItem(Item item, int amount)
        {
            // Add to existing slots.
            foreach (var slot in _slots)
            {
                // Stop if no more items to add.
                if (amount <= 0)
                    break;
                // Skip if slot is empty or items don't match.
                if (slot.IsEmpty() ||
                    slot.GetItem() != item)
                    continue;
                var slotQuantity = slot.GetQuantity();
                var space = item.GetStackSize() - slotQuantity;
                // Skip the item if no space left.
                if (space <= 0)
                    continue;
                // Add the items to the slot.
                var amountToAdd = Mathf.Min(space, amount);
                amount -= amountToAdd;
                slot.SetQuantity(slotQuantity + amountToAdd);
            }
            // Add to new slots.
            for (var s = 0; s < _slots.Length; s++)
            {
                if (amount <= 0)
                    break;
                var slot = _slots[s];
                if (!slot.IsEmpty())
                    continue;
                var quantity = Mathf.Min(item.GetStackSize(), amount);
                amount -= quantity;
                var newSlot = new Slot(item, quantity);
                _slots[s] = newSlot;
            }
            Updated.Invoke();
            return amount;
        }

        public int RemoveItem(Item item, int amount)
        {
            // Go through the slots.
            for (var s = _slots.Length - 1; s >= 0; s--)
            {
                if (amount <= 0)
                    break;
                var slot = _slots[s];
                if (slot.GetItem() != item)
                    continue;
                var quantity = slot.GetQuantity();
                if (quantity <= amount)
                {
                    _slots[s] = new Slot();
                }
                else
                {
                    slot.SetQuantity(quantity - amount);
                }
                amount -= quantity;
            }
            Updated.Invoke();
            return amount;
        }
    }
}