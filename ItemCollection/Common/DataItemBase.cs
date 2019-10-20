using System;
using System.Runtime.Serialization.Formatters.Binary;

using Tejas;
using Tejas.Data;
using Tejas.Collections;
using Tejas.Serialization;

namespace Tejas.App
{
    [Serializable]
    public abstract class DataItemBase : ItemBase, System.ComponentModel.IEditableObject
    {
        #region Fields
        //================================================================================			
        private DbVersion _version = new DbVersion();
        private DataItemBase _editClone = null;
        //================================================================================
        #endregion

        #region Properties
        //================================================================================
        public DbVersion Version
        {
            get { return _version; }
            set { _version = value; }
        }
        ////================================================================================
        protected object EditClone
        {
            get { return _editClone; }
        }
        //================================================================================
        #endregion

        #region Implementation - IEditableObject Members
        //================================================================================
        void System.ComponentModel.IEditableObject.BeginEdit() { OnBeginEdit(); }
        protected virtual void OnBeginEdit()
        {
            // Create clone 
            _editClone = SerializationUtil.BinaryCloneObject(this) as DataItemBase;
        }
        //================================================================================
        void System.ComponentModel.IEditableObject.EndEdit()
        {
            // Invoke specific implementation
            OnEndEdit();

            // Discard clone
            _editClone = null;
        }
        protected virtual void OnEndEdit() { }
        //================================================================================
        void System.ComponentModel.IEditableObject.CancelEdit()
        {
            // Restore field values
            //if (_editClone != null)
            //	_version = _editClone._version;

            // Invoke specific implementation
            OnCancelEdit();

            // Discard clone
            _editClone = null;
        }
        protected abstract void OnCancelEdit();
        //================================================================================
        #endregion
    }

    [Serializable]
    public abstract class DataItemCollectionBase<T> : ItemCollectionBase<T> where T : DataItemBase
    {
        #region Methods
        //================================================================================		
        public void RemoveAt(int index) { BaseRemove(index); }
        public void Clear() { BaseClear(); }
        //================================================================================		
        public DataItemCollectionBase<T> Clone()
        {
            return SerializationUtil.BinaryCloneObject(this) as DataItemCollectionBase<T>;
        }
        //================================================================================
        #endregion
    }

    [Serializable]
    public abstract class DataItemByCodeBase : DataItemBase
    {
        #region Fields
        //================================================================================			
        private string _code = Null.String;
        //================================================================================
        #endregion

        #region Properties
        //================================================================================
        public string Code
        {
            get { return _code; }
            set { base.SetKeyField(ref _code, value); }
        }
        protected override string GetKey() { return _code; }
        //================================================================================
        #endregion

        #region Overrides
        //================================================================================
        protected override void OnCancelEdit()
        {
            DataItemByCodeBase clone = base.EditClone as DataItemByCodeBase;

            if (clone != null)
                _code = clone._code;
        }
        //================================================================================
        #endregion
    }

    [Serializable]
    public abstract class DataItemByCodeCollectionBase<T> :
        DataItemCollectionBase<T> where T : DataItemByCodeBase
    {
        #region Methods
        //================================================================================
        public bool Contains(string code) { return BaseContains(code); }
        public int IndexOf(string code) { return BaseIndexOf(code); }
        //================================================================================		
        public void Remove(string code) { BaseRemove(code); }
        //================================================================================
        #endregion
    }

    [Serializable]
    public abstract class DataItemByIdBase : DataItemBase
    {
        #region Fields
        //================================================================================			
        private int _id = Null.Integer;
        //================================================================================
        #endregion

        #region Properties
        //================================================================================
        public int Id
        {
            get { return _id; }
            set { base.SetKeyField(ref _id, value); }
        }
        protected override string GetKey() { return MakeKey(_id); }
        //================================================================================
        #endregion

        #region Overrides
        //================================================================================
        protected override void OnCancelEdit()
        {
            DataItemByIdBase clone = base.EditClone as DataItemByIdBase;
            //
            if (clone != null)
                _id = clone._id;
        }
        //================================================================================
        #endregion

        #region Methods
        //================================================================================			
        public void SetIdVersionInfo(DataItemIdVersionInfo info)
        {
            this.Id = info.Id;
            this.Version = info.Version;
        }
        //================================================================================
        #endregion
    }

    [Serializable]
    public abstract class DataItemByIdCollectionBase<T> : DataItemCollectionBase<T> 
        where T : DataItemByIdBase
    {
        #region Methods
        //================================================================================
        public bool Contains(int id) { return BaseContains(MakeKey(id)); }
        public int IndexOf(int id) { return BaseIndexOf(MakeKey(id)); }
        //================================================================================		
        public void Remove(int id) { BaseRemove(MakeKey(id)); }
        //================================================================================
        #endregion
    }	

    [Serializable]
    public class DataItemIdVersionInfo
    {
        #region Fields
        //================================================================================			
        private int _id = Null.Integer;
        private DbVersion _version = new DbVersion();
        //================================================================================
        #endregion

        #region Constructors
        //================================================================================			
        public DataItemIdVersionInfo() { }
        //================================================================================			
        public DataItemIdVersionInfo(int id) { _id = id; }
        //================================================================================			
        public DataItemIdVersionInfo(
            int id,
            DbVersion version)
            : this(id) { _version = version; }
        //================================================================================
        public DataItemIdVersionInfo(
            int id,
            object versionValue)
            : this(id) { _version.Value = versionValue; }
        //================================================================================
        #endregion

        #region Properties
        //================================================================================
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        //================================================================================
        public DbVersion Version
        {
            get { return _version; }
        }
        //================================================================================
        #endregion
    }
}
