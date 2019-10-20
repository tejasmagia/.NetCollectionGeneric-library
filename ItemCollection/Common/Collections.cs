using System;
using System.Collections;
using System.Collections.Generic;

using Tejas;

namespace Tejas.Collections
{
    [Serializable]
    public class ItemKeyChangeEventArgs : System.EventArgs
    {
        #region Fields
        //================================================================================
        private string _oldValue = null;
        private string _newValue = null;
        //================================================================================
        #endregion

        #region Constructors
        //================================================================================
        public ItemKeyChangeEventArgs(string oldValue, string newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }
        //================================================================================
        #endregion

        #region Properties
        //================================================================================		
        public string OldValue { get { return _oldValue; } }
        public string NewValue { get { return _newValue; } }
        //================================================================================
        #endregion
    }
    public delegate void ItemKeyChangeEventHandler(object sender, ItemKeyChangeEventArgs args);
    public interface IItem
    {
        #region Properties
        //================================================================================		
        string Key { get;}
        //================================================================================
        #endregion

        #region Events
        //================================================================================		
        event Tejas.Collections.ItemKeyChangeEventHandler KeyChanging;
        event Tejas.Collections.ItemKeyChangeEventHandler KeyChanged;
        //================================================================================
        #endregion
    }

    [Serializable]
    public abstract class ItemBase : IItem
    {
        #region Fields
        //================================================================================		
        [NonSerialized]
        private Tejas.Collections.ItemKeyChangeEventHandler _keyChangingEventHandler = null;
        [NonSerialized]
        private Tejas.Collections.ItemKeyChangeEventHandler _keyChangedEventHandler = null;
        //================================================================================
        #endregion

        #region Methods - abstract
        //================================================================================		
        protected abstract string GetKey();
        //================================================================================
        #endregion

        #region Methods - virtual
        //================================================================================		
        protected virtual void OnKeyChanging(string oldValue, string newValue)
        {
            if (_keyChangingEventHandler != null)
                _keyChangingEventHandler(this, new ItemKeyChangeEventArgs(oldValue, newValue));
        }
        //================================================================================		
        protected virtual void OnKeyChanged(string oldValue, string newValue)
        {
            if (_keyChangedEventHandler != null)
                _keyChangedEventHandler(this, new ItemKeyChangeEventArgs(oldValue, newValue));
        }
        //================================================================================		
        #endregion

        #region Methods - protected
        //================================================================================		
        protected string MakeKey(int field)
        {
            return ValueUtil.IsNull(field) ? null : field.ToString("0");
        }
        //================================================================================		
        protected string MakeKey(DateTime field)
        {
            return ValueUtil.IsNull(field) ? null : field.ToString("yyyy-MM-dd HH:mm:ss");
        }
        //================================================================================		
        protected string MakeKey(params string[] fields)
        {
            return String.Join("-", fields);
        }
        //================================================================================		
        protected void SetKeyField(ref string field, string value)
        {
            SetKeyField(ref field, value, value);
        }
        //================================================================================		
        protected void SetKeyField(ref string field, string value, string newKey)
        {
            if (field != value)
            {
                string oldKey = this.GetKey();
                OnKeyChanging(oldKey, newKey);
                field = value;
                OnKeyChanged(oldKey, newKey);
            }
        }
        //================================================================================		
        protected void SetKeyField(ref DateTime field, DateTime value)
        {
            SetKeyField(ref field, value, value.ToShortDateString());
        }
        //================================================================================		
        protected void SetKeyField(ref DateTime field, DateTime value, string newKey)
        {
            if (field != value)
            {
                string oldKey = this.GetKey();
                OnKeyChanging(oldKey, newKey);
                field = value;
                OnKeyChanged(oldKey, newKey);
            }
        }
        //================================================================================		
        protected void SetKeyField(ref int field, int value)
        {
            SetKeyField(ref field, value, value.ToString());
        }
        //================================================================================		
        protected void SetKeyField(ref int field, int value, string newKey)
        {
            if (field != value)
            {
                string oldKey = this.GetKey();
                OnKeyChanging(oldKey, newKey);
                field = value;
                OnKeyChanged(oldKey, newKey);
            }
        }
        //================================================================================		
        #endregion

        #region Methods - overrides
        //================================================================================
        public override string ToString()
        {
            string key = this.GetKey();
            if (key != null && key.Length > 0)
                return key;
            else
                return base.ToString();
        }
        //================================================================================
        #endregion

        #region Implementation - Tejas.Collections.IItem
        //================================================================================		
        string Tejas.Collections.IItem.Key
        {
            get { return GetKey(); }
        }
        //================================================================================
        event Tejas.Collections.ItemKeyChangeEventHandler Tejas.Collections.IItem.KeyChanging
        {
            add { _keyChangingEventHandler += value; }
            remove { _keyChangingEventHandler -= value; }
        }
        //================================================================================
        event Tejas.Collections.ItemKeyChangeEventHandler Tejas.Collections.IItem.KeyChanged
        {
            add { _keyChangedEventHandler += value; }
            remove { _keyChangedEventHandler -= value; }
        }
        //================================================================================
        #endregion
    }

    [Serializable]
    public class DuplicateKeyException : System.ArgumentException
    {
        #region Constructors
        //================================================================================
        public DuplicateKeyException()
            :
            base("The key is already associated with an element of this collection.") { }
        //================================================================================
        public DuplicateKeyException(string key)
            :
            this(key, null) { }
        //================================================================================
        public DuplicateKeyException(string key, System.Exception innerException)
            :
            base(string.Format("The key '{0}' is already associated with an element of this collection.", key), innerException) { }
        //================================================================================
        protected DuplicateKeyException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            :
            base(info, context) { }
        //================================================================================
        #endregion
    }
    
    public class ItemBaseComparer : IEqualityComparer<ItemBase>
    {
        #region IEqualityComparer<ItemBase> Members

        bool IEqualityComparer<ItemBase>.Equals(ItemBase x, ItemBase y)
        {
            return (((IItem)x).Key.ToLower() == ((IItem)y).Key.ToLower());
        }

        int IEqualityComparer<ItemBase>.GetHashCode(ItemBase obj)
        {
            return ((IItem)obj).Key.GetHashCode();
        }

        #endregion
    } 

    [Serializable]
    public abstract class ItemCollectionBase<T> :
        IEnumerable<T>, ICollection<T>,IList<T>,
        ICollection,IEnumerable,IList 
        where T : ItemBase
    {
        #region Fields
        //================================================================================
        // Collection
        private System.Type _itemType = null;
        private bool _readOnly = false;
        private bool _fixedSize = false;
        private Dictionary<string, T> _dictionary = null;
        private List<T> _List = null;
        // IItem 
        [NonSerialized]
        private Tejas.Collections.ItemKeyChangeEventHandler _itemKeyChangingEventHandler = null;
        [NonSerialized]
        private Tejas.Collections.ItemKeyChangeEventHandler _itemKeyChangedEventHandler = null;
        //================================================================================
        #endregion

        #region Constructors
        //================================================================================
        protected ItemCollectionBase() : this(false, false, true) { }
        //================================================================================
        protected ItemCollectionBase(bool readOnly, bool fixedSize) : this(readOnly, fixedSize, true) { }
        //================================================================================
        protected ItemCollectionBase(bool readOnly, bool fixedSize, bool caseInsensitive)
            :
            this(readOnly, fixedSize,
            (caseInsensitive ? StringComparer.CurrentCultureIgnoreCase : null)) { }
        //================================================================================
        protected ItemCollectionBase(
            bool readOnly,
            bool fixedSize,
            IEqualityComparer<string> comparer)
        {
            // Determine collection item's type (to be used for validation in IList_...)
            System.Reflection.PropertyInfo propInfo = this.GetType().GetProperty("Item", new Type[] { typeof(int) });
            if (propInfo == null)
                propInfo = this.GetType().GetProperty("Item", new Type[] { typeof(long) });
            if (propInfo == null)
                propInfo = this.GetType().GetProperty("Item", new Type[] { typeof(string) });
            if (propInfo != null)
                _itemType = propInfo.PropertyType;
            else
                throw new NotImplementedException("Property 'Item' is not implemented.");

            // Store parameters				
            _readOnly = readOnly;
            _fixedSize = fixedSize;

            // Initialize fields
            //_hashTable = new Hashtable(hashProvider, comparer);
            _dictionary = new Dictionary<string, T>(comparer);
            _List = new List<T>();
            _itemKeyChangingEventHandler = new ItemKeyChangeEventHandler(this.Item_KeyChanging);
            _itemKeyChangedEventHandler = new ItemKeyChangeEventHandler(this.Item_KeyChanged);
        }
        //================================================================================
        #endregion

        #region Methods - private
        //================================================================================
        private void RemoveItem(int index, T item)
        {
            // Declare
            Tejas.Collections.IItem iItem = (Tejas.Collections.IItem)item;
            string key = iItem.Key;

            // Notify (before)
            this.OnItemRemoving(index, item);

            // Remove
            if (key != null)
                _dictionary.Remove(key);
            _List.Remove(item);

            // Remove handlers
            iItem.KeyChanging -= _itemKeyChangingEventHandler;
            iItem.KeyChanged -= _itemKeyChangedEventHandler;

            // Notify (after)
            this.OnItemRemoved(index, item);
        }
        //================================================================================
        #endregion

        #region Methods - virtual
        //================================================================================		
        protected virtual void OnItemInserting(int index, ItemBase item) { }
        protected virtual void OnItemInserted(int index, ItemBase item) { }
        protected virtual void OnItemRemoving(int index, ItemBase item) { }
        protected virtual void OnItemRemoved(int index, ItemBase item) { }
        protected virtual void OnClearing() { }
        protected virtual void OnCleared() { }
        //================================================================================		
        #endregion

        #region Methods - protected
        //================================================================================		
        protected string MakeKey(int field)
        {
            return ValueUtil.IsNull(field) ? null : field.ToString("0");
        }
        //================================================================================		
        protected string MakeKey(DateTime field)
        {
            return ValueUtil.IsNull(field) ? null : field.ToString("yyyy-MM-dd HH:mm:ss");
        }
        //================================================================================		
        protected string MakeKey(params string[] fields)
        {
            return String.Join("-", fields);
        }
        //================================================================================
        protected void BaseInsert(int index, Tejas.Collections.IItem item)
        {
            // Declare
            string key = item.Key;

            // Validate
            if (key != null && _dictionary.ContainsKey(key))
                throw new Tejas.Collections.DuplicateKeyException(key);

            // Notify (before)
            OnItemInserting(index, (T)item);

            // Add to hashtable if key is supplied    
            if (key != null)
                _dictionary.Add(key, (T)item);

            // Add handlers
            item.KeyChanging += _itemKeyChangingEventHandler;
            item.KeyChanged += _itemKeyChangedEventHandler;

            // Add to list
            _List.Insert(index, (T)item);

            // Notify (after)
            OnItemInserted(index, (T)item);
        }
        //================================================================================
        protected int BaseAdd(Tejas.Collections.IItem item)
        {
            BaseInsert(_List.Count, item);
            return _List.Count - 1;
        }
        //================================================================================
        protected void BaseRemove(T item)
        {
            // Remove
            this.RemoveItem(BaseIndexOf(item), item);
        }
        //================================================================================
        protected void BaseRemove(string key)
        {
            // Declare
            object item = _dictionary[key];

            // Remove
            if (item != null)
                this.BaseRemove((T)item);
        }
        //================================================================================
        protected void BaseRemove(int index)
        {
            // Remove
            this.RemoveItem(index, _List[index]);
        }
        //================================================================================
        protected void BaseClear()
        {
            // Notify (before)
            this.OnClearing();

            // Remove handlers
            foreach (Tejas.Collections.IItem item in _List)
            {
                item.KeyChanging -= _itemKeyChangingEventHandler;
                item.KeyChanged -= _itemKeyChangedEventHandler;
            }

            // Clear
            _dictionary.Clear();
            _List.Clear();

            // Notify (after)
            this.OnCleared();
        }
        //================================================================================
        protected bool BaseContains(T item)
        {
            return _List.Contains(item);
        }
        //================================================================================
        protected bool BaseContains(string key)
        {
            return _dictionary.ContainsKey(key);
        }
        //================================================================================
        protected int BaseIndexOf(T item)
        {
            return _List.IndexOf(item);
        }
        //================================================================================
        protected int BaseIndexOf(string key)
        {
            return _List.IndexOf(_dictionary[key]);
        }
        //================================================================================
        protected object BaseGetItem(int index)
        {
            return _List[index];
        }
        //================================================================================
        protected object BaseGetItem(string key)
        {
            return _dictionary[key];
        }
        //================================================================================
        protected List<String> BaseGetKeyList()
        {
            return new List<String>(_dictionary.Keys);
        }
        //================================================================================
        #endregion

        #region Methods - public
        //================================================================================
        public virtual ArrayList GetItemList()
        {
            return new ArrayList(_List);
        }
        //================================================================================
        public virtual void Sort()
        {
            _List.Sort();
        }
        //================================================================================
        public virtual void Sort(IComparer<T> comparer)
        {
            _List.Sort(comparer);
        }
        //================================================================================
        public virtual ItemBase[] ToArray()
        {
            return _List.ToArray();
        }
        //================================================================================
        public virtual void ForEach(Action<T> action)
        {
            if (_List == null)
            {
                throw new ArgumentNullException("Item collection empty.");
            }
            if (action == null)
            {
                throw new ArgumentNullException("Action is empty.");
            }

            _List.ForEach(action);
        }
        //================================================================================
        #endregion

        #region Event handlers
        //================================================================================		
        private void Item_KeyChanging(object sender, Tejas.Collections.ItemKeyChangeEventArgs args)
        {
            // Validate
            if (args.NewValue != null && _dictionary.ContainsKey(args.NewValue))
                throw new Tejas.Collections.DuplicateKeyException(args.NewValue);
        }
        //================================================================================		
        private void Item_KeyChanged(object sender, Tejas.Collections.ItemKeyChangeEventArgs args)
        {
            // Change key in hashtable
            if (args.OldValue != null && _dictionary.ContainsKey(args.OldValue))//_hashTable.ContainsKey(args.OldValue))
            {
                _dictionary.Remove(args.OldValue);
                //_hashTable.Remove(args.OldValue);
            }
            if (args.NewValue != null)
            {
                _dictionary.Add(args.NewValue, (T)sender);
               //_hashTable.Add(args.NewValue, sender);
            }
        }
        //================================================================================		
        #endregion

        #region Implementation - System.Collections.IEnumerable<ItemBase>
        //================================================================================
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _List.GetEnumerator();
        }
        //================================================================================
        #endregion

        #region Implementation - System.Collections.ICollection<ItemBase>
        //================================================================================
        void ICollection<T>.Add(T item)
        {
            _List.Add(item);
        }
        //================================================================================
        void System.Collections.Generic.ICollection<T>.Clear()
        {
            _List.Clear();
        }
        //================================================================================
        bool System.Collections.Generic.ICollection<T>.Contains(T item)
        {
            return (_List.Contains(item));
        }
        //================================================================================
        void ICollection<T>.CopyTo(T[] array, int index)
        {
            _List.CopyTo(array, index);
        }
        //================================================================================
        int ICollection<T>.Count
        {
            get { return _List.Count; }
        }
        //================================================================================
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }
        //================================================================================
        bool ICollection<T>.Remove(T item)
        {
            return _List.Remove(item);
        }
        //================================================================================
        #endregion

        #region Implementation - System.Collections.IList<ItemBase>
        //================================================================================
        void IList<T>.Insert(int index, T item)
        {
            // Validate
            if (_readOnly || _fixedSize)
                throw new NotSupportedException();
            if (_itemType != null && !_itemType.IsInstanceOfType(item))
                throw new ArgumentException();
            if (!(item is IItem))
                throw new ArgumentException();

            // Insert
            this.BaseInsert(index, (T)item);
        }
        //================================================================================
        void IList<T>.RemoveAt(int index)
        {
            // Validate
            if (_readOnly || _fixedSize)
                throw new NotSupportedException();

            // Remove
            this.BaseRemove(index);
        }
        //================================================================================
        T IList<T>.this[int index]
        {
            get { return _List[index]; }
            set
            {
                if (_readOnly)
                    throw new NotSupportedException();
                if (_itemType != null && !_itemType.IsInstanceOfType(value))
                    throw new ArgumentException();
                if (!(value is IItem))
                    throw new ArgumentException();
                _List[index] = value;
            }
        }
        //================================================================================
        int IList<T>.IndexOf(T item)
        {
            return this.BaseIndexOf(item);
        }
        //================================================================================
        #endregion

        #region Implementation - System.Collections.IEnumerable
        //================================================================================
        public virtual IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_List).GetEnumerator();
        }
        //================================================================================
        #endregion

        #region Implementation - System.Collections.ICollection
        //================================================================================
        public virtual int Count
        {
            get { return _List.Count; }
        }
        //================================================================================
        public virtual bool IsSynchronized
        {
            get { return ((ICollection)_List).IsSynchronized; }
        }
        ////================================================================================
        public virtual object SyncRoot
        {
            get { return ((ICollection)_List).SyncRoot; }
        }
        //================================================================================
        public virtual void CopyTo(System.Array array, int index)
        {
            ((ICollection)_List).CopyTo(array, index);
        }
        //================================================================================
        #endregion

        #region Implementation - System.Collections.IList
        //================================================================================
        bool IList.IsReadOnly
        {
            get { return _readOnly; }
        }
        //================================================================================
        bool IList.IsFixedSize
        {
            get { return _fixedSize; }
        }
        //================================================================================
        void IList.Insert(int index, object item)
        {
            // Validate
            if (_readOnly || _fixedSize)
                throw new NotSupportedException();
            if (_itemType != null && !_itemType.IsInstanceOfType(item))
                throw new ArgumentException();
            if (!(item is IItem))
                throw new ArgumentException();

            // Insert
            this.BaseInsert(index, (IItem)item);
        }
        //================================================================================
        int IList.Add(object item)
        {
            ((IList)this).Insert(Count, item);
            return Count - 1;
        }
        //================================================================================
        void IList.RemoveAt(int index)
        {
            // Validate
            if (_readOnly || _fixedSize)
                throw new NotSupportedException();

            // Remove
            this.BaseRemove(index);
        }
        //================================================================================
        void IList.Remove(object item)
        {
            // Validate
            if (_readOnly || _fixedSize)
                throw new NotSupportedException();

            // Remove
            this.BaseRemove((T)item);
        }
        //================================================================================
        void IList.Clear()
        {
            // Validate
            if (_readOnly || _fixedSize)
                throw new NotSupportedException();

            // Clear
            this.BaseClear();
        }
        //================================================================================
        object IList.this[int index]
        {
            get { return _List[index]; }
            set
            {
                // Validate
                if (_readOnly)
                    throw new NotSupportedException();
                if (_itemType != null && !_itemType.IsInstanceOfType(value))
                    throw new ArgumentException();
                if (!(value is IItem))
                    throw new ArgumentException();

                // Set - remove/insert
                this.BaseRemove(index);
                this.BaseInsert(index, (IItem)value);
            }
        }
        //================================================================================
        bool IList.Contains(object item)
        {
            return this.BaseContains((T)item);
        }
        //================================================================================
        int IList.IndexOf(object item)
        {
            return this.BaseIndexOf((T)item);
        }
        //================================================================================
        #endregion 		
    }

    [Serializable]
    public class NameValue : ItemBase
    {
        #region Fields
        //================================================================================		
        private string _name = null;
        private string _value = null;
        //================================================================================
        #endregion

        #region Constructors
        //================================================================================		
        public NameValue() { }
        public NameValue(string name) { _name = name; }
        public NameValue(string name, string value) : this(name) { _value = value; }
        //================================================================================
        #endregion

        #region Properties
        //================================================================================				
        public string Name
        {
            get { return _name; }
            set { base.SetKeyField(ref _name, value); }
        }
        protected override string GetKey() { return this.Name; }
        //================================================================================		
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        //================================================================================
        #endregion
    }

    [Serializable]
    public class NameValueCollection<T> : ItemCollectionBase<T> where T : NameValue
    {
        #region Properties
        //================================================================================				
        public T this[int index] { get { return (T)BaseGetItem(index); } }
        public T this[string name] { get { return (T)BaseGetItem(name); } }
        //================================================================================
        #endregion

        #region Methods
        //================================================================================
        public bool Contains(T item) { return BaseContains(item); }
        public bool Contains(string name) { return BaseContains(name); }
        public int IndexOf(T item) { return BaseIndexOf(item); }
        public int IndexOf(string name) { return BaseIndexOf(name); }
        //================================================================================
        public T Insert(int index, T item) { BaseInsert(index, item); return item; }
        public T Add(T item) { BaseAdd(item); return item; }
        public T Add(string name) { return Add((T)new NameValue(name)); }
        public T Add(string name, string value) { return Add((T)new NameValue(name, value)); }
        //================================================================================		
        public void Remove(T item) { BaseRemove(item); }
        public void Remove(string name) { BaseRemove(name); }
        public void Remove(int index) { BaseRemove(index); }
        public void Clear() { BaseClear(); }
        //================================================================================
        #endregion
    }

    //[Serializable]
    //public class IntegerCollection : System.Collections.CollectionBase
    //{
    //    #region Properties
    //    //================================================================================				
    //    public int this[int index] { get { return (int)base.InnerList[index]; } }
    //    //================================================================================
    //    #endregion

    //    #region Methods
    //    //================================================================================
    //    public bool Contains(int item) { return base.InnerList.Contains(item); }
    //    public int IndexOf(int item) { return base.InnerList.IndexOf(item); }
    //    //================================================================================
    //    public int Add(int item) { return base.InnerList.Add(item); }
    //    public void Remove(int item) { base.InnerList.Remove(item); }
    //    //================================================================================
    //    #endregion
    //}
}
