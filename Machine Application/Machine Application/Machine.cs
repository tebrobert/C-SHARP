using System.Collections.Generic;
using System;

namespace Machine_Application {
    public class Machine
    {
        private Wallet Wallet;
        private int CurrentMoney;
        private List<MenuItem> Menu;
        
        // конструктор
        public Machine(Wallet wallet, List<MenuItem> menu) {
            CurrentMoney = 0;
            Wallet = wallet;
            Menu = menu;
        }
        
        // узнать количество внесённых денег
        public int GetCurrentMoney() {
            return CurrentMoney;
        }

        // внести деньги
        public void WalletAddCoins(Wallet DX) {
            Wallet.AddCoins(DX);
            CurrentMoney += DX.GetMoney();
        }

        // вернуть сдачу
        public Wallet GiveChange() {
            List<int> Nominals = Wallet.GetNominals();
            List<int> Coins = Wallet.GetCurrentCoins();

            List<int> change = new Wallet(Nominals).GetCurrentCoins();

            Position Pos = new Position(Wallet);

            try {
                RecGiveChange(change, Pos, Nominals, Coins);
            }
            catch (OperationCanceledException) {
                Wallet Change = new Wallet(Nominals, change);
                Wallet.RemoveCoins(Change);
                CurrentMoney -= Change.GetMoney();
                return Change;
            }
            catch (MissingMemberException) {
                throw new MissingMemberException("Автомат не может выдать сдачу!");
            }

            throw new Exception();
        }

        private void RecGiveChange(List<int> change, Position Pos, List<int> Nominals, List<int> Coins) {
            change[Pos.Get()] = Coins[Pos.Get()];

            while (true) {
                int money = new Wallet(Nominals, change).GetMoney();

                if (money == CurrentMoney)
                    throw new OperationCanceledException("solution found");
                
                if (money > CurrentMoney)
                    change[Pos.Get()]--;
                
                if (money < CurrentMoney)
                    break;
            }

            while (true) {
                if (Pos.CheckFirst())
                    change[Pos.Get()] = 0;

                if (change[Pos.Get()] == 0)
                    break;

                Pos.Left();
                RecGiveChange(change, Pos, Nominals, Coins);

                change[Pos.Get()]--;
            }

            if (!Pos.CheckFirst()) {
                Pos.Left();
                RecGiveChange(change, Pos, Nominals, Coins);
            }

            if (Pos.CheckLast())
                throw new MissingMemberException("solution not found");

            Pos.Right();
            return;
        }

        // вернуть меню для покупателя
        public List<MenuItem> GetMenu() {
            List<MenuItem> UserMenu = new List<MenuItem>(Menu.Count);
            
            foreach (MenuItem m in Menu)
                UserMenu.Add(m.GetUserCopy());
                
            return UserMenu;
        }

        // купить товар
        public void Buy(int item) {
            int order = item - 1;
            if (order >= Menu.Count || order < 0)
                throw new IndexOutOfRangeException("Выбранного пункта меню не существует!");

            if (Menu[order].CheckEmpty())
                throw new MissingMemberException("Указанного отвара нет в наличии!");

            if (Menu[order].GetPrice() > CurrentMoney)
                throw new AccessViolationException("Внесено недостаточно денег для покупки данного товара!");

            Menu[order].Dec();
            CurrentMoney -= Menu[order].GetPrice();
        }
    }
}
