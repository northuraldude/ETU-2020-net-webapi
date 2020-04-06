/**
 * Дисциплина: Основы разработки корпоративных приложений на платформе .NET
 * Тема: Тестовое задание #1 - Связный список (без стандартных коллекций и LINQ).
 * Разработал: Белоусов Евгений
 * Группа: 6305
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace TestLinkedList
{
    /// <summary>
    /// Класс элемента связного списка.
    /// </summary>
    /// <remarks>
    /// Каждый объект класса содержит некоторую информацию
    /// и ссылку на следующий элемент списка.
    /// </remarks>
    public class Node<T>
    {
        public T Data { get; }
        public Node<T> Next { get; set; }
        
        public Node(T data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Класс односвязного списка.
    /// </summary>
    /// <remarks>
    /// Класс реализует интерфейс IEnumerable.
    /// В каждом объекте сохраняются ссылки на начало и конец списка,
    /// а также учитывается количество содержащихся элементов.
    /// Реализуются типовые действия со связным списком:
    ///  - добавление произвольных данных (в начало и в конец);
    ///  - удаление произвольных данных (при первом их вхождении);
    ///  - реверс списка.
    /// </remarks>
    public class LinkedList<T> : IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _count;

        /// <summary>
        /// Добавить данные в конец связного списка.
        /// </summary>
        /// <param name="data"> Произвольные данные. </param>
        public void Add(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            var node = new Node<T>(data);
            if (_head == null)
                _head = node;
            else
                _tail.Next = node;
            
            _tail = node;
            _count++;
        }

        /// <summary>
        /// Добавить данные в начало связного списка.
        /// </summary>
        /// <param name="data"> Произвольные данные. </param>
        public void AddFirst(T data)
        {
            var node = new Node<T>(data);
            node.Next = _head;
            _head = node;
            if (IsEmpty)
                _tail = _head;
            
            _count++;
        }

        /// <summary>
        /// Удалить данные из связного списка.
        /// </summary>
        /// <remarks>
        /// Удаляется первое вхождение данных.
        /// </remarks>
        /// <param name="data"> Произвольные данные. </param>
        public bool Remove(T data)
        {
            Node<T> current = _head;
            Node<T> previous = null;
            while (current != null)
            {
                if (data != null && current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            _tail = previous;
                    }
                    else
                    {
                        _head = _head.Next;
                        if (_head == null)
                            _tail = null;
                    }
                    
                    _count--;
                    return true;
                }
                
                previous = current;
                current = current.Next;
            }
            
            return false;
        }

        /// <summary>
        /// Реверсировать список.
        /// </summary>
        public void Reverse()
        {
            Node<T> current = _head;
            Node<T> previous = null;
            Node<T> next;
                
            while (current != null)
            {
                next = current.Next;
                if (previous != null)
                {
                    current.Next = previous;
                }
                else
                {
                    current.Next = _tail.Next;
                    _tail = current;
                }

                previous = current;
                current = next;
            }

            _head = previous;
        }
        
        /// <summary>
        /// Очистить список полностью.
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }

        /// <summary>
        /// Свойство, предназначенное для проверки наличия в списке элементов.
        /// </summary>
        public bool IsEmpty => _count == 0;
        /// <summary>
        /// Свойство, предназначенное для получения количества элементов,
        /// содержащихся в списке.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Вернуть перечислитель, выполняющий перебор всех элементов в связном списке.
        /// </summary>
        /// <remarks>
        /// Реализация интерфейса IEnumerable.
        /// </remarks>
        /// <returns> Возвращает перечислитель. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        
        /// <summary>
        /// Вернуть перечислитель, осуществляющий итерационный переход по связному списку.
        /// </summary>
        /// <remarks>
        /// Реализация интерфейса IEnumerable.
        /// </remarks>
        /// <returns> Объект IEnumerator. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable).GetEnumerator();
        }
    }
    
    public static class Program
    {
        /// <summary>
        /// Точка входа программы.
        /// </summary>
        /// <param name="args"> Список аргументов командной строки.</param>
        public static void Main(string[] args)
        {
            var linkedList = new LinkedList<string>();
            // добавляем элементы
            linkedList.Add("Евгений");
            linkedList.Add("Александр");
            linkedList.Add("Илья");
            linkedList.Add("Егор");
            // выводим элементы
            foreach(var item in linkedList)
                Console.WriteLine(item);
            // выводим количество элементов
            Console.WriteLine(linkedList.Count);
            // удаляем элемент и выводим оставшиеся элементы
            linkedList.Remove("Евгений");
            foreach (var item in linkedList)
                Console.WriteLine(item);
            // выводим количество элементов
            Console.WriteLine(linkedList.Count);
            // добавляем элемент в начало и выводим элементы           
            linkedList.AddFirst("Гога");
            foreach(var item in linkedList)
                Console.WriteLine(item);
            // выводим количество элементов
            Console.WriteLine(linkedList.Count);
            // реверсируем список и выводим элементы
            linkedList.Reverse();
            foreach(var item in linkedList)
                Console.WriteLine(item);
            // выводим количество элементов
            Console.WriteLine(linkedList.Count);
            // очищаем список и выводим количество элементов
            linkedList.Clear();
            Console.WriteLine(linkedList.Count);
        }
    }
}