namespace CodeWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        public Node(int value)
        {
            Value = value;
            Next = null;
        }

        public int Value { get; set; }
        public Node Next { get; set; }
    }

    public class Stack
    {
        private Node first;
        private Node last;
        private int size;

        public Stack()
        {
            first = null;
            last = null;
            size = 0;
        }

        public int Push(int val)
        {
            var newNode = new Node(val);
            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                var temp = first;
                first = newNode;
                first.Next = temp;
            }

            return ++size;
        }

        public int Pop()
        {
            if (first == null) return 0;
            var temp = first;
            if (first == last)
            {
                last = null;
            }

            first = first.Next;
            size--;
            return temp.Value;
        }
    }

    public class Kata
    {
        public static void Main(string[] args)
        {
            var mystack = new Stack();
            mystack.Push(1);
            mystack.Push(2);
            mystack.Push(3);
            Console.WriteLine(mystack.Pop());
            Console.WriteLine(mystack.Pop());
            Console.WriteLine(mystack.Pop());
            Console.WriteLine("Done");
        }
    }
}