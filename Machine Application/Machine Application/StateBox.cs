using System;
using System.Linq;
using System.Collections.Generic;

namespace Machine_Application {
    class StateBox {
        private Machine Machine;
        private Customer Customer;
        
        public StateBox(Pair<Machine, Customer> Pair) {
            Machine = Pair.A;
            Customer = Pair.B;
        }
        
        public void Launch() {
            Console.Clear();
            MainState();
        }
        
        private void MainState() {
            // вывод сообщения
            Console.WriteLine("Внесено: {0}р.", Machine.GetCurrentMoney());
            Console.WriteLine("Выберете действие покупателя:");
            Console.WriteLine("1. Внести деньги в автомат");
            Console.WriteLine("2. Выбрать пункт меню");
            Console.WriteLine("3. Попросить сдачу");
            Console.WriteLine("Для выхода из программы введите 0.");

            // ввод числа
            int x;
            try {
                x = Reader.Read();
            }
            catch (Exception e) {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                MainState();
                return;
            }

            // переход в новое состояние
            Console.Clear();
            switch (x) {
                case 1:
                    State1();
                    return;
                case 2:
                    State2();
                    return;
                case 3:
                    State3();
                    return;
                case 0:
                    return;
                default:
                    Console.WriteLine("Выбранного действия не существует!");
                    Console.WriteLine();
                    MainState();
                    return;
            }
        }
        
        private void State1() {
            // вывод сообщения
            Console.WriteLine("Внесено: {0}р.", Machine.GetCurrentMoney());
            Console.WriteLine("Монеты покупателя (всего {0}р):", Customer.GetWallet().GetMoney());

            List<int> Nominals = Customer.GetWallet().GetNominals();
            foreach (int x in Nominals)
                Console.WriteLine("{0}р - {1} шт", x, Customer.GetWallet().Get(x));
            Console.WriteLine();
            
            Console.WriteLine("Введите вносимое количество монет для");
            Console.WriteLine("каждого номинала по возрастанию номиналов.");
            Console.WriteLine();
            
            Console.WriteLine("Для отмены введите 0.");

            // ввод чисел
            List<int> X;

            try {
                X = Reader.Read(Customer.GetWallet().GetNominals().Count);
            }
            catch(OperationCanceledException e) {
                Console.Clear();
                MainState();
                return;
            }
            catch (Exception e) {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                State1();
                return;
            }

            // внесение денег
            Wallet DX = Customer.GetWallet().ToWallet(X);

            try {
                Customer.GetWallet().RemoveCoins(DX);
                Machine.WalletAddCoins(DX);
            }
            catch(Exception e) {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                State1();
                return;
            }

            // вывод сообщения
            Console.Clear();
            Console.WriteLine("Успешно внесено {0}р!", DX.GetMoney());
            Console.WriteLine("Всего внесено: {0}р.", Machine.GetCurrentMoney());
            Reader.Pause();

            // переход в главное состояние
            Console.Clear();
            MainState();
        }
        
        private void State2() {
            // вывод сообщения
            List<MenuItem> Menu = Machine.GetMenu();

            Console.WriteLine("Меню:");
            for (int i = 0; i < Menu.Count; i++)
                Console.WriteLine("{0}. {1}, {2}р", i + 1, Menu[i].GetName(), Menu[i].GetPrice());
            Console.WriteLine("Введите выбранный пункт меню.");
            Console.WriteLine("Для отмены введите 0.");

            // ввод числа
            int x;
            try {
                x = Reader.Read();
            }
            catch (Exception e) {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                State2();
                return;
            }

            // отмена покупки товара
            if (x == 0) {
                Console.Clear();
                MainState();
                return;
            }

            // покупка товара
            try {
                Machine.Buy(x);
            }
            catch(Exception e) {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                State2();
                return;
            }

            // вывод сообщения
            Console.Clear();
            Console.WriteLine("Товар успешно получен!");
            Reader.Pause();

            // переход в главное состояние
            Console.Clear();
            MainState();
        }
        
        private void State3() {
            // запрос сдачи
            Wallet Change;
            try {
                Change = Machine.GiveChange();
                Customer.GetWallet().AddCoins(Change);
            }
            catch(Exception e) {
                Console.Clear();
                Console.WriteLine(e.Message);
                Reader.Pause();

                Console.Clear();
                MainState();
                return;
            }

            // вывод сообщения
            Console.Clear();
            Console.WriteLine("Выдана сдача (всего {0}р):", Change.GetMoney());
            
            List<int> Nominals = Customer.GetWallet().GetNominals();
            foreach (int x in Nominals)
                Console.WriteLine("{0}р - {1} шт", x, Change.Get(x));
                
            Reader.Pause();

            // переход в главное состояние
            Console.Clear();
            MainState();
        }
    }
}
