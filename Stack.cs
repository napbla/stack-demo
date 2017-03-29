using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackDemo
{
    class Stack
    {
        private string[] storage;
        private int top;
        private int max;
        
        public Stack()
        {
            top = 0;
            max = 10;
            storage = new string[10];
        }

        public Stack(int m)
        {
            top = 0;
            if (max <= 1000)
                m = max;
            else max = 1000;
            storage = new string[max];
        }

        public int TopPosition()
        {
            return top;
        }

        public string Pop()
        {
            if (top > 0)
            {
                top--;
                return storage[top];
            }
            return "Stack is empty";
        }

        public string ViewTop()
        {
            if (top > 0)
                return storage[top-1];
            return "Stack is empty";
        }

        public string Push(string x)
        {
            if (top < max)
            {
                storage[top] = x;
                top++;
                return "Successfully pushed " + x;
            }
            else return "Stack is full";
        }

    }
}
