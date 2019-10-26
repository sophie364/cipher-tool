using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherTools
{
    class Stack<T> // generic; can be any type
    {
        private T[] items;
        private int pointer = -1;

        public T[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public void Push(T item) // adds items to the stack
        {
            if (pointer < items.Length - 1) // if stack isn't full
            {
                pointer++;
                items[pointer] = item;
            }
        }

        public void Pop(ref T item) // stores the item at the top of the stack and moves the pointer down
        {
            if (pointer > -1) // if stack isn't empty
            {
                item = items[pointer];
                pointer--;
            }
        }

        public T[] ReverseArray(T[] array) // flips the array back to front
        {
            items = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                Push(array[i]); // puts all of the original array items onto the stack
            }
            for (int i = 0; i < array.Length; i++)
            {
                Pop(ref array[i]); // LIFO - the output is the original array but in reverse
            }
            return array;
        }

    }
}
