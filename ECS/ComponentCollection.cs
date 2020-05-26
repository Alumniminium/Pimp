using System;
using System.Collections.Generic;

namespace pimp.ECS
{
    public class PooledComponentCollection<T> where T : struct
    {
        public int Count => items.Length;
        private readonly Dictionary<int, int> OwnerToIndex;
        private readonly Dictionary<int, int> IndexToOwner;
        private readonly Stack<int> availableIndicies;
        private readonly T[] items;
        public ref T this[int index] => ref items[index];

        public PooledComponentCollection(int size)
        {
            OwnerToIndex = new Dictionary<int, int>();
            IndexToOwner = new Dictionary<int, int>();
            availableIndicies = new Stack<int>();
            items = new T[size];
            for (int i = 0; i < size; i++)
                availableIndicies.Push(i);
        }

        public ref T GetFor(int id)
        {
            if (OwnerToIndex.TryGetValue(id, out var index))
            {
                return ref items[index];
            }
            else
            {
                index = availableIndicies.Pop();
                IndexToOwner.Add(index, id);
                OwnerToIndex.Add(id, index);
            }
            return ref items[index];
        }
        public void ReturnFor(int id)
        {
            if (OwnerToIndex.Remove(id, out var index))
            {
                IndexToOwner.Remove(index);
                availableIndicies.Push(index);
            }
        }
        public IEnumerable<(int index,int owner)> GetInUse()
        {
            foreach(var kvp in IndexToOwner)
                yield return (kvp.Key, kvp.Value);
        }
    }
}