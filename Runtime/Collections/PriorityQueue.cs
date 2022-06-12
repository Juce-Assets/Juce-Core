using System;
using System.Collections.Generic;

namespace Juce.Core.Collections
{
    public sealed class PriorityQueue<T>
    {
        private readonly List<Item> list = new List<Item>();

        private class Item
        {
            public T Element { get; }
            public float Priority { get; }

            public Item(T element, float priority)
            {
                Element = element;
                Priority = priority;
            }
        }

        public void Add(T element, float priority)
        {
            bool added = false;

            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Priority < priority)
                {
                    list.Insert(i, new Item(element, priority));

                    added = true;

                    break;
                }
            }

            if (!added)
            {
                list.Insert(list.Count, new Item(element, priority));
            }
        }

        public T PopBack()
        {
            T ret = list[list.Count - 1].Element;

            list.RemoveAt(list.Count - 1);

            return ret;
        }

        public T PopFront()
        {
            T ret = list[0].Element;

            list.RemoveAt(0);

            return ret;
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public T At(int index)
        {
            return list[index].Element;
        }
    }
}
