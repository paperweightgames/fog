using Inventory;
using UnityEngine;

namespace Ui
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Container _container;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Transform _slots;

        private void Awake()
        {
            _container.Updated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            // Delete current slots.
            foreach (Transform child in _slots)
                Destroy(child.gameObject);
            // Create new children.
            var slots = _container.GetSlots();
            foreach (var slot in slots)
            {
                var newSlot = Instantiate(_slotPrefab, _slots);
                if (slot.IsEmpty())
                    continue;
                newSlot.GetComponent<Slot>().SetSlot(slot);
            }
        }
    }
}