using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Players
{
    class Inventory
    {
        InventoryItem[] items;

        public Inventory(int maxItems) {
            items = new InventoryItem[maxItems];
            for (int i = 0; i < items.Length; i++) {
                items[i] = new InventoryItem();
            }
        }

        public InventoryItem this[int index] {
            get { 
                return items[index - 1]; }
        }

        public int Length {
            get { return items.Length; }
        }

    }
}
