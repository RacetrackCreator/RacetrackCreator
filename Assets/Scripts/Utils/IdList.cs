using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    public class IdList<T>
    {
        private Dictionary<int, T> elements;
        private List<int> holes;

        public IEnumerable<int> Keys => elements.Keys;
        public IEnumerable<T> Values => elements.Values;

        public IdList()
        {
            holes = new List<int>();
            elements = new Dictionary<int, T>();
        }

        public int Push(T e)
        {
            int idx = elements.Count;
            if (holes.Count > 0)
            {
                idx = holes[0];
                holes.Remove(0);
            }
            elements.Add(idx, e);
            return idx;
        }

        public T Get(int i)
        {
            return elements[i];
        }

        public void Set(int i, T e)
        {
            elements[i] = e;
        }

        public T Remove(int i)
        {
            T e = elements[i];
            elements.Remove(i);
            int j = 0;
            for (; j < holes.Count; j++)
            {
                if (holes[j] > i)
                {
                    break;
                }
            }
            holes.Insert(j, i);
            return e;
        }
    }
}