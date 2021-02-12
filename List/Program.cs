using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class MyNode<T>
    {
        public T data;
        public MyNode<T> nextNode;
        public MyNode(T _data)
        {
            data = _data;
        }
    }

    class MyLinkedList<T> : IEnumerable<T>
    {
        MyNode<T> head;
        MyNode<T> tail;
        int length;

        public void AddFirst(T data)
        {
            MyNode<T> newNode = new MyNode<T>(data);

            if (head != null)
            {
                newNode.nextNode = head;
            }
            head = newNode;

            length++;
        }

        public void AddNext(T data)
        {
            MyNode<T> newNode = new MyNode<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = head;
            }
            else
            {
                tail.nextNode = newNode;
            }
            tail = newNode;

            length++;
        }
        //создает после элемента под номером pos (отсчитывая от ноля)
        //если forced = true, то если позиция больше чем размер списка, функция воткнет в конце списка, иначе вернет ошибку
        public void AddAt(T data, int pos, bool forced = true)
        {
            if (!forced)
            {
                if (pos >= length || pos < 0)
                {
                    Console.WriteLine("Element with such position doesn't exist");
                    return;
                }
            }


            if (pos >= length)
                AddNext(data);
            else if (pos < 0)
                AddFirst(data); 
            else
            {
                MyNode<T> newNode = new MyNode<T>(data);
                MyNode<T> iterator = head;
                for (int i = 0; i < pos; i++)
                {
                    iterator = iterator.nextNode;
                }
                MyNode<T> post_iterator = iterator.nextNode;
                iterator.nextNode = newNode;
                newNode.nextNode = post_iterator;
            }
            length++;
        }
        public void RemoveAt(int pos)
        {
            if(pos >= length || pos < 0)
            {
                Console.WriteLine("Element with such position doesn't exist");
                return;
            }

            MyNode<T> iterator = head;
            MyNode<T> pre_iterator = null;
            for (int i = 0; i < pos; i++)
            {
                pre_iterator = iterator;
                iterator = iterator.nextNode;
            }
            MyNode<T> post_iterator = iterator.nextNode;

            //начало
            if (pre_iterator == null)
            {
                head = post_iterator;
                if (head == null)
                    tail = null;
            }
            else //середина или конец
            {
                pre_iterator.nextNode = post_iterator;
                if (post_iterator == null)
                    tail = pre_iterator;
            }
            length--;
        }

        public void Remove(T data)
        {
            if (length == 0)
                return;

            MyNode<T> iterator = head;
            MyNode<T> pre_iterator = null;
            while(iterator != null)
            {
                if(iterator.data.Equals(data))
                {
                    MyNode<T> post_iterator = iterator.nextNode;
                    //начало
                    if (pre_iterator == null)
                    {
                        head = post_iterator;
                        if (head == null)
                            tail = null;
                    }
                    else //середина или конец
                    {
                        pre_iterator.nextNode = post_iterator;
                        if (post_iterator == null)
                            tail = pre_iterator;
                    }
                    break;
                }
                pre_iterator = iterator;
                iterator = iterator.nextNode;
            }
            length--;
        }

        public MyNode<T> At(int pos)
        {
            if(pos >= length || pos < 0)
            {
                Console.WriteLine("No element on this position");
                return null;
            }

            MyNode<T> iterator = head;
            for (int i = 0; i < pos; i++)
            {
                iterator = iterator.nextNode;
            }

            return iterator;
        }

        public bool Contains(T data)
        {
            MyNode<T> iterator = head;
            while(iterator != null)
            {
                if(iterator.data.Equals(data))
                {
                    return true;
                }
                iterator = iterator.nextNode;
            }
            return false;
        }

        public void ReverseList()
        {
            MyNode<T> pre_iterator = null;
            MyNode<T> iterator = head;
            MyNode<T> post_iterator = head.nextNode;
            tail = head;
            while(iterator != null)
            {
                post_iterator = iterator.nextNode;
                iterator.nextNode = pre_iterator;
                pre_iterator = iterator;
                iterator = post_iterator;
            }
            head = pre_iterator;
        }

        public int Length() { return length; }
        public void PrintOut()
        {
            foreach(T node_data in this)
            {
                Console.Write("[" + node_data + "]");
            }
            Console.WriteLine();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            MyNode<T> current = head;
            while(current!=null)
            {
                yield return current.data;
                current = current.nextNode;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList<string> list = new MyLinkedList<string>();
            list.AddNext("first");
            list.AddNext("second");
            list.PrintOut();
            Console.WriteLine();

            list.AddFirst("zero");
            list.PrintOut();
            Console.WriteLine();

            list.AddAt("error", 1000, false);
            list.AddAt("no error", 1000);
            list.PrintOut();
            Console.WriteLine();

            list.AddAt("new first", 1);
            list.PrintOut();
            Console.WriteLine();

            list.Remove("first");
            list.PrintOut();
            Console.WriteLine();

            Console.WriteLine(list.At(2).data);
            Console.WriteLine();

            list.RemoveAt(2);
            list.PrintOut();
            Console.WriteLine();

            Console.WriteLine(list.At(2).data);
            Console.WriteLine();

            list.AddFirst("minus one");
            list.AddNext("third");
            list.PrintOut();
            Console.WriteLine();

            Console.WriteLine(list.Contains("one hundredth"));
            Console.WriteLine(list.Contains("zero"));
            Console.WriteLine();

            list.ReverseList();
            list.PrintOut();
            Console.WriteLine();

            list.AddFirst("mystery");
            list.PrintOut();
            Console.WriteLine();

            list.ReverseList();
            list.PrintOut();
            Console.WriteLine();
        }
    }
}
