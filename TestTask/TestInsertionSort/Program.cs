/**
 * Дисциплина: Основы разработки корпоративных приложений на платформе .NET
 * Тема: Тестовое задание #3 - Сортировка вставками (без .OrderBy())
 * Разработал: Белоусов Евгений
 * Группа: 6305
 */

using System;

namespace TestInsertionSort
{
    public static class Program
    {
        /// <summary>
        /// Метод сортирует массив целочисленных значений при помощи вставок.
        /// </summary>
        /// <param name="array"> Неотсортированный массив целочисленных значений.</param>
        /// <returns>
        /// Возвращает отсортированный по возрастанию массив целочисленных значений.
        /// </returns>
        private static int[] InsertionSort(int[] array)
        {
            for (var index = 1; index < array.Length; index++)
            {
                var key = array[index];
                var indexOfSorted = index;
                while (indexOfSorted > 0 && array[indexOfSorted - 1] > key)
                {
                    Swap(ref array[indexOfSorted - 1], ref array[indexOfSorted]);
                    indexOfSorted--;
                }

                array[indexOfSorted] = key;
            }
            return array;
        }
        
        /// <summary>
        /// Метод меняет значения двух целочисленных переменных между собой.
        /// </summary>
        /// <remarks>
        /// Переменные передаются в метод по ссылке.
        /// </remarks>
        /// <param name="valueA"> Первая целочисленная переменная.</param>
        /// <param name="valueB"> Вторая целочисленная переменная.</param>
        private static void Swap(ref int valueA, ref int valueB)
        {
            var temp = valueA;
            valueA = valueB;
            valueB = temp;
        }
        
        /// <summary>
        /// Точка входа программы.
        /// </summary>
        /// <param name="args"> Список аргументов командной строки.</param>
        public static void Main(string[] args)
        {
            Console.Write("Please, enter the values: ");
            var values = Console.ReadLine()?.Split(new[] { " ", ","},
                                                            StringSplitOptions.RemoveEmptyEntries);
            var array = new int[values.Length];
            for (var index = 0; index < values.Length; index++)
                array[index] = Convert.ToInt32(values[index]);
            
            Console.WriteLine("Sorted by insertions: {0}", string.Join(" ", InsertionSort(array)));
        }
    }
}