using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _quantity;

        public void SetSlot(global::Inventory.Slot slot)
        {
            // Set the icon.
            var item = slot.GetItem();
            _icon.sprite = item.GetIcon();
            _icon.enabled = true;
            // Set the quantity.
            var quantityText = $"{slot.GetQuantity()}/{item.GetStackSize()}";
            _quantity.text = quantityText;
            _quantity.enabled = true;
        }
    }
}
