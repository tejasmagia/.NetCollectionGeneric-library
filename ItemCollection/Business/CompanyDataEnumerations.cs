using System;
using System.Collections.Generic;
using System.Text;

using Tejas;
using Tejas.Collections;
using Tejas.App;

namespace Tejas.App.BO
{
    /// <summary>
    /// Summary description for CompanyDataEnumeration.
    /// </summary>
    public class CompanyDataEnumeration : DataItemBase
    {
        #region Fields

        private string _DataItemCode = Null.String;
        private int _CompanyUnitRef = Null.Integer;
        private int _DataTableRef = Null.Integer;
        private string _DataItemEnumValue = Null.String;
        private string _DataItemEnumDesc = Null.String;

        #endregion

        #region Properties

        public int CompanyUnitRef
        {
            get { return _CompanyUnitRef; }
            set { _CompanyUnitRef = value; }
        }

        public int DataTableRef
        {
            get { return _DataTableRef; }
            set { _DataTableRef = value; }
        }

        public string DataItemCode
        {
            get { return _DataItemCode; }
            set { _DataItemCode = value; }
        }

        public string DataItemEnumValue
        {
            get { return _DataItemEnumValue; }
            set { _DataItemEnumValue = value; }
        }

        public string DataItemEnumDesc
        {
            get { return _DataItemEnumDesc; }
            set { _DataItemEnumDesc = value; }
        }


        #endregion

        #region Overrides
        //================================================================================
        protected override void OnCancelEdit()
        {
            //base.OnCancelEdit();
            //
            CompanyDataEnumeration clone = base.EditClone as CompanyDataEnumeration;
            if (clone == null)
                return;
        }
        //================================================================================
        #endregion

        #region Members

        protected override string GetKey()
        {
            return (CompanyUnitRef.ToString() + "|" + DataTableRef.ToString() + "|" +
                DataItemCode + "|" + DataItemEnumValue);
        }
        #endregion
    }

    /// <summary>
    /// Collection of CompanyDataEnumeration 
    /// </summary>
    public class CompanyDataEnumerationCollection<T> : DataItemCollectionBase<T> where T : CompanyDataEnumeration
    {
        #region Properties
        //================================================================================		
        /// <summary>
        /// Index key created of 
        /// CompanyUnitRef+"|"+DataTableRef+"|"+DataItemCode+"|"+DataItemEnumValue
        /// </summary>
        public CompanyDataEnumeration this[string index] { get { return (CompanyDataEnumeration)BaseGetItem(index); } }

        /// <summary>
        /// Returns CompanyDataEnumerationCollection for given CompanyRef
        /// </summary>
        public CompanyDataEnumerationCollection<T> this[int CompanyRef]
        {
            get
            {
                if (CompanyRef != Null.Integer || !CompanyRef.Equals(null))
                {
                    CompanyDataEnumerationCollection<T> tempColl =
                        new CompanyDataEnumerationCollection<T>();
                    foreach (T obj in this)
                    {
                        if (obj.CompanyUnitRef == CompanyRef)
                            tempColl.Add(obj);
                    }

                    return tempColl;
                }
                return null;
            }
        }


        public CompanyDataEnumerationCollection<T> this[
            CompanyUnitHierarchy CompanyUnitObj]
        {
            get
            {
                return this.GetAllDataEnumerationForCompanyHierarchy(
                    CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef,
                    CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);
            }
        }


        public CompanyDataEnumerationCollection<T> this[
            CompanyUnitHierarchy CompanyUnitObj, int dataTableRef]
        {
            get
            {
                CompanyDataEnumerationCollection<T> tempColl =
                    this.GetAllDataEnumerationForCompanyHierarchy(
                    CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef,
                    CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

                tempColl = tempColl.Find(dataTableRef);

                return tempColl;
            }
        }

        public CompanyDataEnumerationCollection<T> this[
            CompanyUnitHierarchy CompanyUnitObj, params int[] dataTableRefs]
        {
            get
            {
                CompanyDataEnumerationCollection<T> tempColl =
                    this.GetAllDataEnumerationForCompanyHierarchy(
                    CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef,
                    CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

                tempColl = tempColl.Find(dataTableRefs);

                return tempColl;
            }
        }


        public CompanyDataEnumerationCollection<T> this[
            CompanyUnitHierarchy CompanyUnitObj, string dataItemCode, int dataTableRef]
        {
            get
            {
                CompanyDataEnumerationCollection<T> tempColl =
                    this.GetAllDataEnumerationForCompanyHierarchy(
                    CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef,
                    CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

                tempColl = tempColl.Find(dataTableRef);

                tempColl = tempColl.Find(dataItemCode);

                return tempColl;
            }
        }

        public CompanyDataEnumerationCollection<T> this[
            string dataItemCode, int dataTableRef]
        {
            get
            {
                CompanyDataEnumerationCollection<T> tempColl = this.Find(dataTableRef);

                tempColl = tempColl.Find(dataItemCode);

                return tempColl;
            }
        }

        public CompanyDataEnumerationCollection<T> this[
            CompanyUnitHierarchy CompanyUnitObj, string dataItemCode, params int[] dataTableRefs]
        {
            get
            {
                CompanyDataEnumerationCollection<T> tempColl =
                    this.GetAllDataEnumerationForCompanyHierarchy(
                    CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef,
                    CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

                tempColl = (tempColl.Find(dataTableRefs)).Find(dataItemCode);

                //tempColl = tempColl.Find(dataItemCode);

                return tempColl;
            }
        }

        public CompanyDataEnumerationCollection<T> this[
            string dataItemCode, params int[] dataTableRefs]
        {
            get
            {
                CompanyDataEnumerationCollection<T> tempColl =
                    this.Find(dataTableRefs);

                tempColl = tempColl.Find(dataItemCode);

                return tempColl;
            }
        }


        //================================================================================
        #endregion

        #region Methods
        //================================================================================
        /// <summary>
        /// Gets all CompanyDataEnumerations for Given CompanyRef including there ParentCompanyRefs
        /// </summary>
        /// <param name="CompanyRef"></param>
        /// <param name="CompanyParentRef"></param>
        /// <param name="CompanyGrandParentRef"></param>
        /// <param name="CompanyGrandGrandParentRef"></param>
        /// <returns>CompanyDataEnumerationCollection Object</returns>
        public CompanyDataEnumerationCollection<T> GetAllDataEnumerationForCompanyHierarchy(
            int CompanyRef, int CompanyParentRef,
            int CompanyGrandParentRef, int CompanyGrandGrandParentRef)
        {
            return this[CompanyRef].
                Merge(this[CompanyParentRef]).
                    Merge(this[CompanyGrandParentRef]).
                        Merge(this[CompanyGrandGrandParentRef]);

            //CompanyDataEnumerationCollection<T> tempColl = this[CompanyRef];
            //CompanyDataEnumerationCollection<T> tempColl1 = this[CompanyParentRef];
            //CompanyDataEnumerationCollection<T> tempColl2 = this[CompanyGrandParentRef];
            //CompanyDataEnumerationCollection<T> tempColl3 = this[CompanyGrandGrandParentRef];

            // Merge into CompanyRef Collection from CompanyParentRef Collection
            //tempColl.Merge(tempColl1);
            // Merge into First Merge Collection from CompanyGrandParentRef Collection
            //tempColl.Merge(tempColl2);
            // Merge into First Merge Collection from CompanyGrandParentRef Collection
            //tempColl.Merge(tempColl3);

            //return tempColl;
        }
        //================================================================================
        public bool Contains(T item) { return BaseContains(item); }
        /// <summary>
        /// Finds DataTableRef within CompanyDataEnumerationCollection
        /// </summary>
        /// <param name="dataItemCode"></param>
        /// <returns></returns>
        public bool Contains(string dataItemCode, int dataTableRef, string dataItemEnumValue)
        {
            if (this != null)
            {
                foreach (T obj in this)
                {
                    if (obj.DataItemCode == dataItemCode &&
                        obj.DataTableRef == dataTableRef &&
                        obj.DataItemEnumValue == dataItemEnumValue)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Finds DataTableRef within CompanyDataEnumerationCollection
        /// </summary>
        /// <param name="DataTableRef"></param>
        /// <returns></returns>
        public CompanyDataEnumerationCollection<T> Find(int dataTableRef)
        {
            if (this != null && dataTableRef != Null.Integer)
            {
                CompanyDataEnumerationCollection<T> tempColl = new CompanyDataEnumerationCollection<T>();
                foreach (T obj in this)
                {
                    if (obj.DataTableRef == dataTableRef)
                        tempColl.Add(obj);
                }
                return tempColl;
            }
            return null;
        }

        /// <summary>
        /// Finds DataItemCode within CompanyDataEnumerationCollection
        /// </summary>
        /// <param name="DataTableRef"></param>
        /// <returns></returns>
        public CompanyDataEnumerationCollection<T> Find(string dataItemCode)
        {
            if (this != null && dataItemCode != Null.String)
            {
                CompanyDataEnumerationCollection<T> tempColl = new CompanyDataEnumerationCollection<T>();
                foreach (T obj in this)
                {
                    if (obj.DataItemCode == dataItemCode || obj.DataItemCode.ToUpper() == dataItemCode.ToUpper())
                        tempColl.Add(obj);
                }
                return tempColl;
            }
            return null;
        }

        /// <summary>
        /// Finds DataTableRefs within CompanyDataEnumerationCollection
        /// </summary>
        /// <param name="dataTableRefs">Collection of int[dataTableRef]</param>
        /// <returns>CompanyDataEnumerationCollection Object</returns>
        public CompanyDataEnumerationCollection<T> Find(params int[] dataTableRefs)
        {
            if (dataTableRefs.Length > 0)
            {
                CompanyDataEnumerationCollection<T> tempColl = new CompanyDataEnumerationCollection<T>();
                for (int i = 0; i < dataTableRefs.Length && dataTableRefs[i] != Null.Integer; i++)
                {
                    tempColl.Merge(this.Find(dataTableRefs[i]));
                }
                return tempColl;
            }
            return null;
        }
        public int IndexOf(T item) { return BaseIndexOf(item); }
        //================================================================================
        public T Insert(int index, T item) { BaseInsert(index, item); return item; }
        public T Add(T item) { BaseAdd(item); return item; }
        public CompanyDataEnumerationCollection<T> Merge(CompanyDataEnumerationCollection<T> mergeFromColl)
        {
            if (mergeFromColl != null)
            {
                foreach (T obj in mergeFromColl)
                {
                    if (!this.Contains(obj.DataItemCode, obj.DataTableRef, obj.DataItemEnumValue))
                        this.Add(obj);
                }
            }
            return this;
        }
        //================================================================================		
        public void Remove(T item) { BaseRemove(item); }
        //================================================================================
        #endregion
    }
}
