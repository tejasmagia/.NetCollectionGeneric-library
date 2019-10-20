using System;
using Tejas;
using Tejas.Collections;
using Tejas.App;

namespace Tejas.App.BO
{
    /// <summary>
    /// Summary description for Issuers.
    /// </summary>
    public class Issuer : DataItemByIdBase
    {
        #region Issuer Field Names
        //================================================================================
        /*	
        //		IssuerId =  _Int  
        //		IssuerPID =  varchar(7)  
        //		Issuer =  varchar(50)  
        */

        private int _issuerId = Null.Integer;
        private string _issuerPId = Null.String;
        private string _issuerName = Null.String;
        //================================================================================
        #endregion

        #region Properties

        public int IssuerId
        {
            get { return base.Id; }
            set { base.Id = value; }
        }
        //================================================================================
        public string IssuerCode
        {
            get { return _issuerPId; }
            set { _issuerPId = value; }
        }
        //================================================================================
        public string IssuerName
        {
            get { return _issuerName; }
            set { _issuerName = value; }
        }
        //================================================================================
        #endregion

        #region Overrides
        //================================================================================
        protected override void OnCancelEdit()
        {
            base.OnCancelEdit();
            //
            Issuer clone = base.EditClone as Issuer;
            if (clone == null)
                return;

            _issuerId = clone._issuerId;
            _issuerPId = clone._issuerPId;
            _issuerName = clone._issuerName;
        }
        //================================================================================
        #endregion

        #region Methods
        //================================================================================

        //================================================================================
        #endregion
    }

    /// <summary>
    /// Collection of Issuers 
    /// </summary>
    public class IssuerCollection<T> : DataItemByIdCollectionBase<T>
        where T : Issuer
    {
        #region Properties
        //================================================================================				
        public T this[int index] { get { return (T)BaseGetItem(index); } }

        public IssuerCollection<T> this[string issuerName]
        {
            get
            {
                IssuerCollection<T> tempColl = new IssuerCollection<T>();
                foreach (T issuer in this)
                {
                    if (issuer.IssuerName == issuerName)
                        tempColl.Add(issuer);
                }
                return tempColl;
            }
        }
        //================================================================================
        #endregion

        #region Methods
        //================================================================================
        public T Find(int id) { return (T)BaseGetItem(MakeKey(id)); }
        //================================================================================
        public bool Contains(T item) { return BaseContains(item); }
        public int IndexOf(T item) { return BaseIndexOf(item); }
        //================================================================================
        public T Insert(int index, T item) { BaseInsert(index, item); return item; }
        public T Add(T item) { BaseAdd(item); return item; }
        //================================================================================		
        public void Remove(T item) { BaseRemove(item); }
        //================================================================================
        #endregion
    }
}
