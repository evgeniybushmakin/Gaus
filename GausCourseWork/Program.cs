using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GausCourseWork
{
    using System;
    class GaussSolver
    {
        static void Main()
        {
            // Запрашиваем у пользователя размерность матрицы A и вектора b
m:          Console.Write("Введите размерность матрицы A и вектора b | A(N x N), b(N) |: ");
            int n = int.Parse(Console.ReadLine());
            int m = n;
            Console.Clear();
            // Инициализируем матрицу A и вектор b
            double[,] A = new double[n, n];
            double[] b = new double[m];
            Console.WriteLine("Введите элементы матрицы A:");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    bool validInput = false;
                    while (!validInput)
                    {
                        Console.Write("A[{0}][{1}] = ", i + 1, j + 1);
                        try
                        {
                            A[i, j] = double.Parse(Console.ReadLine());
                            validInput = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: некорректный формат числа. Нажмите Enter и попробуйте снова.");
                            Console.ReadLine();
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Ошибка: число слишком большое или слишком маленькое. Нажмите Enter и попробуйте снова.");
                            Console.ReadLine();
                        }
                        Console.Clear();
                    }
                }
            }

            Console.WriteLine("Введите элементы вектора b:");
            for (int i = 0; i < m; i++)
            {
                bool validInput = false;
                while (!validInput)
                {
                    Console.Write("b[{0}] = ", i + 1);
                    try
                    {
                        b[i] = double.Parse(Console.ReadLine());
                        validInput = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: некорректный формат числа. Нажмите Enter и попробуйте снова.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Ошибка: число слишком большое или слишком маленькое. Нажмите Enter и попробуйте снова.");
                    }
                    Console.Clear();
                }
            }

            // Решаем СЛАУ методом Гаусса
            double[] x = Gauss(A, b);

            // Проверяем, есть ли нулевой элемент на главной диагонали
            if (A[n - 1, n - 1] == 0)
            {
                Console.WriteLine("Система не имеет решений.");
                Console.ReadLine(); 
                return;
            }
            else
            {
                Console.WriteLine("Решение СЛАУ:");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("x[{0}] = {1}", i, x[i]);
                }
            }

n:          Console.Write("\nЖелаете выполнить решение СЛАУ методом Гауса ещё раз?: \n\n\t1. Да\n\t2. Нет\n\n>>");
            int MenuNumber;
            MenuNumber = Convert.ToInt32(Console.ReadLine());
            switch (MenuNumber)
            {
                case 1:
                    {
                        Console.Clear();
                        Array.Clear(A, 0, n);
                        Array.Clear(b, 0, n);
                        goto m;
                    }
                case 2:
                    {
                        Console.Clear();
                        Console.Write("Для завершения работы программы нажмите клавишу Enter.");
                    }
                    break;
                default:
                    {
                        Console.Write("Данного пункта меню не существует повторите ввод!");
                        Thread.Sleep(2500);
                        Console.Clear();
                        goto n;
                    }
            }
            Console.ReadLine();
        }

        static double[] Gauss(double[,] A, double[] b)
        {
            int n = b.Length;
            double[] x = new double[n];

            // Прямой ход
            for (int k = 0; k < n - 1; k++)
            {
                // Проверяем, есть ли нулевой элемент на главной диагонали
                if (A[k, k] == 0)
                {
                    Console.WriteLine("Система не имеет решений.");
                    return null;
                }

                for (int i = k + 1; i < n; i++)
                {
                    double factor = A[i, k] / A[k, k];
                    b[i] -= factor * b[k];
                    for (int j = k; j < n; j++)
                    {
                        A[i, j] -= factor * A[k, j];
                    }
                }
            }

            // Проверяем, что решения существуют
            if (A[n - 1, n - 1] == 0)
            {
                // Нет решений
                return null;
            }

            // Обратный ход
            x[n - 1] = b[n - 1] / A[n - 1, n - 1];
            for (int k = n - 2; k >= 0; k--)
            {
                double sum = 0;
                for (int j = k + 1; j < n; j++)
                {
                    sum += A[k, j] * x[j];
                }
                x[k] = (b[k] - sum) / A[k, k];
            }

            return x;
        }
    }
}