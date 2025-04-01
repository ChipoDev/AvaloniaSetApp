using System;
using System.Collections.Generic;
using System.Linq;

namespace SetApp.Models
{
    public class Set<T>
    {
        private readonly HashSet<T> _elements = new HashSet<T>(); 

        public int Count => _elements.Count;

        public bool IsEmpty => _elements.Count == 0;

        public Set(IEnumerable<T>? initialElements = null)
        {
            if (initialElements != null)
            {
                foreach (var element in initialElements)
                {
                    Add(element);
                }
            }
        }

        public void Add(T item)
        {
            _elements.Add(item);
        }

        public void Remove(T item)
        {
            _elements.Remove(item);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public T[] ToArray()
        {
            return _elements.ToArray();
        }

        public bool Contains(T item)
        {
            return _elements.Contains(item);
        }
    }
}