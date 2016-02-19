using System.Collections.Generic;
using System.Linq;
using System;

namespace Machine_Application {
    public class Wallet {
        private SortedDictionary<int, int> Nominalled;
        private List<int> Nominals;
        
        // конструктор (пустой кошелёк)
        public Wallet(List<int> nominals) {
            Nominalled = new SortedDictionary<int, int>();
            Nominals = nominals;
            foreach (int n in nominals)
                Nominalled.Add(n, 0);
        }

        // конструктор
        public Wallet(List<int> nominals, List<int> coins) {
            Nominalled = new SortedDictionary<int, int>();
            Nominals = nominals;
            for (int i = 0; i < nominals.Count; i++)
                Nominalled.Add(nominals[i], coins[i]);
        }

        // проверить наличие указанного числа монет
        public bool CheckCoins(Wallet DX) {
            foreach (int x in DX.GetNominals()) {
                if (Nominalled[x] - DX.Get(x) < 0)
                    return false;
            }
            return true;
        }

        // добавить монеты
        public void AddCoins(Wallet DX) {
            foreach (int x in DX.GetNominals())
                Nominalled[x] += DX.Get(x);
        }

        // забрать монеты
        public void RemoveCoins(Wallet DX)  {
            if (!CheckCoins(DX)) {
                string s = "Количество требуемых момент превышает количество имеющихся!";
                throw new InvalidOperationException(s);
            }

            foreach (int x in DX.GetNominals())
                Nominalled[x] -= DX.Get(x);
        }

        // узнать количество монет данного номинала
        public int Get(int key) {
            return Nominalled[key];
        }

        // получить список номиналов
        public List<int> GetNominals() {
            return Nominals;
        }

        // получить список с числом монет каждого номинала
        public List<int> GetCurrentCoins() {
            List<int> Coins = new List<int>(Nominals.Count);
            foreach (KeyValuePair<int, int> x in Nominalled)
                Coins.Add(x.Value);
            return Coins;
        }

        // посчитать количество денег в кошельке
        public int GetMoney() {
            int money = 0;
            foreach (KeyValuePair<int, int> pair in Nominalled)
                money += pair.Key * pair.Value;
            return money;
        }

        // преобразовать массив в кошелёк
        public Wallet ToWallet(List<int> coins) {
            return new Wallet(Nominals, coins);
        }
    }
}
