using System;

namespace Machine_Application {
    public class MenuItem {
        private string Name;
        private int Price;
        private int Count;
        
        public MenuItem(string name, int price, int count) {
            Name = name;
            Price = price;
            Count = count;
        }
        
        public string GetName() {
            return Name;
        }
        
        public int GetPrice() {
            return Price;
        }
        
        public bool CheckEmpty() {
            return Count == 0;
        }
        
        public void Dec() {
            if (CheckEmpty()) {
                string s = "Товары отсутствуют, нельзя уменьшить их количество!";
                throw new InvalidOperationException(s);
            }
            Count--;
        }
        
        public MenuItem GetUserCopy() {
            return new MenuItem(Name, Price, 0);
        }
    }
}
