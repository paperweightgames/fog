namespace Inventory
{
    public class Slot
    {
        private readonly Item _item;
        private int _quantity;

        public bool IsEmpty()
        {
            return _item == null;
        }
        public Slot()
        {
            _item = null;
            _quantity = 0;
        }

        public Slot(Item item, int quantity)
        {
            _item = item;
            _quantity = quantity;
        }

        public Item GetItem()
        {
            return _item;
        }

        public void SetQuantity(int quantity)
        {
            _quantity = quantity;
        }
        
        public int GetQuantity()
        {
            return _quantity;
        }
    }
}