using System;
using System.Collections.Generic;
using System.Linq;

namespace Machine_Application {
    class Reader {
        public static void Pause() {
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        public static int Read() {
            return Read(1)[0];
        }
        
        public static List<int> Read(int N) {
            // формирование сообщения
            string CaseEnding;

            if (N % 10 == 1)
                CaseEnding = "ло";
            else if ((N % 10 >= 2) && (N % 10 <= 4))
                CaseEnding = "ла";
            else
                CaseEnding = "ел";

            // вывод сообщения
            Console.WriteLine();
            if (N == 1)
                Console.WriteLine("Введите число: ");
            else
                Console.WriteLine("Введите {0} чис{1} через пробел: ", N, CaseEnding);

            // чтение
            List<int> numbers;

            try {
                numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList<int>();
            }
            catch (FormatException) {
                throw new FormatException("Неправильный формат числа!");
            }
            catch (OverflowException) {
                throw new OverflowException("Переполнение!");
            }

            // отмена ввода
            if (N != 1 && numbers.Count == 1 && numbers[0] == 0)
                throw new OperationCanceledException();

            // проверка количества чисел
            if (numbers.Count != N) {
                string s1 = "Требуется ввести " + N + " чис" + CaseEnding;
                string s2 = ", введено " + numbers.Count + "!";
                throw new FormatException(s1 + s2);
            }

            // проверка знаков чисел
            foreach (int x in numbers) {
                if (x < 0)
                    throw new FormatException("Числа должны быть больше нуля!");
            }

            return numbers;
        }
    }
}
