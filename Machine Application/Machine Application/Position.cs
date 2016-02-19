using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Application {
    public class Position {
        private int Pos;
        private int Last;
        
        public Position(Wallet Wallet) {
            Last = Wallet.GetNominals().Count - 1;
            Pos = Last;
        }
        
        public bool CheckFirst() {
            return Pos == 0;
        }
        
        public bool CheckLast() {
            return Pos == Last;
        }
        
        public void Left() {
            if (CheckFirst()) {
                string s = "Невозможно уменьшить позицию, достигнта минимальная!";
                throw new IndexOutOfRangeException(s);
            }
            Pos--;
        }
        
        public void Right() {
            if (CheckLast()) {
                string s = "Невозможно увеличить позицию, достигнта максимальная!";
                throw new IndexOutOfRangeException(s);
            }
            Pos++;
        }
        
        public int Get() {
            return Pos;
        }
    }
}
