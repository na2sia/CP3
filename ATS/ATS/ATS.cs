using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    class ATS: IDictionary<int,Contract>, IList<Calling>
    {
        private readonly IDictionary<int, Contract> contracts;
        private readonly IList<Calling> callings;
        
        public ATS()
        {
            contracts = new Dictionary<int, Contract>();
            callings = new List<Calling>();
        }

        #region IList<Calling>

        public int IndexOf(Calling item)
        {
           return callings.IndexOf(item);
        }

        public void Insert(int index, Calling item)
        {
            callings.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            callings.RemoveAt(index);
        }

        Calling IList<Calling>.this[int index]
        {
            get { return callings[index]; }
            set { callings[index] = value; }
        }
               
        public void Add(Calling item)
        {
            callings.Add(item);
        }

        public void Clear()
        {
            callings.Clear();
        }

        public bool Contains(Calling item)
        {
            return callings.Contains(item);
        }

        public void CopyTo(Calling[] array, int arrayIndex)
        {
            callings.CopyTo(array,arrayIndex);
        }

        public int Count{get { return callings.Count(); }}

        public bool IsReadOnly { get { return callings.IsReadOnly; } }

        public bool Remove(Calling item)
        {
            return callings.Remove(item);
        }

        public IEnumerator<Calling> GetEnumerator()
        {
            return callings.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IDictionary
        public void Add(KeyValuePair<int, Contract> item)
        {
            contracts.Add(item);
        }

        public void Add(int key, Contract value)
        {
            contracts.Add(key,value);
        }

        public bool ContainsKey(int key)
        {
            return contracts.ContainsKey(key);
        }

        public ICollection<int> Keys
        {
            get { return contracts.Keys; }
        }

        public bool Remove(int key)
        {
            return contracts.Remove(key);
        }

        public bool TryGetValue(int key, out Contract value)
        {
            return contracts.TryGetValue(key,out value);
        }

        public ICollection<Contract> Values
        {
            get { return contracts.Values; }
        }

        public Contract this[int key]
        {
            get { return contracts[key]; }
            set { contracts[key] = value; }
        }
        
        public bool Contains(KeyValuePair<int, Contract> item)
        {
            return contracts.Contains(item);
        }

        public void CopyTo(KeyValuePair<int, Contract>[] array, int arrayIndex)
        {
            contracts.CopyTo(array,arrayIndex);
        }

        public bool Remove(KeyValuePair<int, Contract> item)
        {
            return contracts.Remove(item);
        }

        bool ICollection<KeyValuePair<int, Contract>>.IsReadOnly { get { return contracts.IsReadOnly; } }

        int ICollection<KeyValuePair<int, Contract>>.Count { get { return contracts.Count; } }

        IEnumerator<KeyValuePair<int, Contract>> IEnumerable<KeyValuePair<int, Contract>>.GetEnumerator()
        {
            return contracts.GetEnumerator();
        }
        #endregion
    

    }
}
