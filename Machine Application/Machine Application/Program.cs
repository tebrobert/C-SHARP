using System.Collections.Generic;
using System.Linq;

namespace Machine_Application {
    class Program {
        static Pair<Machine, Customer> LoadConfig() {
            Machine Machine;
            Customer Customer;
            List<int> nominals = new List<int> { 1, 2, 5, 10 };
            List<int> coins = new List<int> { 10, 10, 10, 7 };
            List<MenuItem> menu = new List<MenuItem> {
                new MenuItem("кексы", 50, 4),
                new MenuItem("печенье", 10, 3),
                new MenuItem("вафли", 30, 10),
            };

            Machine = new Machine(new Wallet(nominals), menu);
            Customer = new Customer(new Wallet(nominals, coins));
            return new Pair<Machine, Customer>(Machine, Customer);
        }
        static void Main(string[] args) {
            Pair<Machine, Customer> Pair = LoadConfig();
            StateBox StateBox = new StateBox(Pair);
            StateBox.Launch();
        }
    }
}
