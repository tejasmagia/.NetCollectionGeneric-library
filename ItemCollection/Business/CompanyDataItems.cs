using System;
using System.Collections.Generic;
using System.Text;

using Tejas;
using Tejas.Collections;
using Tejas.App;

namespace Tejas.App.BO
{
    /// <summary>
    /// Summary description for CompanyDataItem.
    /// </summary>
    public class CompanyDataItem : DataItemBase
	{
		#region Fields

		private string	_dataItemCode = Null.String;
		private int		_CompanyUnitRef = Null.Integer;
		private int		_DataTableRef = Null.Integer;
		private string	_Description = Null.String;
		private bool	_EnumeratedInd = false;		
		//stores the underlying .NET type of the data item
		private System.Type	_dataItemType = null;
		private CompanyDataEnumerationCollection<CompanyDataEnumeration> _dataItemEnumColl = null;

		private double	_minXBarResponseCode = Null.Double;
		private bool	_isNull_MinXBarResponseCode=false;	
		// Description of DataItem (intended for Column Caption)
							
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
			get { return _dataItemCode;	}
			set { _dataItemCode = value; }
		}
		
		// Not Used so far
		public bool IsNull_MinXBarResponseCode
		{
			get{ return _isNull_MinXBarResponseCode; }
		}
		

		public double MinXBarResponseCode
		{
			get { return _minXBarResponseCode; }
			set { _minXBarResponseCode = value; }
		}
		

		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}
		
		//Alun Morgan 17.04.2007
		/// <summary>
		/// Gets whether this DataItem has a lookup
		/// </summary>
		public bool EnumeratedInd
		{	
			get { return _EnumeratedInd; }
			set { _EnumeratedInd = value; }
		}
		
		/// <summary>
		/// Returns the DOT NET System Type required to store this data item
		/// </summary>		
		public System.Type SystemType
		{
			get { return _dataItemType; }
			set { _dataItemType = value; }
		}

        public CompanyDataEnumerationCollection<CompanyDataEnumeration> DataEnumeration
        {
            get { return _dataItemEnumColl; }
            set { _dataItemEnumColl = value; }
        }
		#endregion

        #region Overrides
        //================================================================================
        protected override void OnCancelEdit()
        {
            //base.OnCancelEdit();
            //
            CompanyDataItem clone = base.EditClone as CompanyDataItem;
            if (clone == null)
                return;
        }
        //================================================================================
        #endregion

		#region Members

//		public void SetMinXBarResponseCode(DataRow dataRow)
//		{
//			//mm
//			_minXBarResponseCode = (double)Functions.Nz(Functions.GetOptionalData(dataRow,"MinXBarResponseCode",(double)0),(double)0);//mm
//			_isNull_MinXBarResponseCode = 
//				(!dataRow.Table.Columns.Contains("MinXBarResponseCode")) ||
//				(Convert.IsDBNull(dataRow["MinXBarResponseCode"]));
//			//
//		}	
	
		protected override string GetKey()
		{
			return (CompanyUnitRef.ToString() + "" + DataTableRef.ToString() + "" +
				DataItemCode);
		}
		#endregion
	}

    /// <summary>
    /// Collection of CompanyDataItem 
    /// </summary>
    public class CompanyDataItemCollection<T> : DataItemCollectionBase<T> where T :CompanyDataItem
	{
		#region Properties
		//================================================================================		
		/// <summary>
		/// Index key created of 
		/// CompanyUnitRef+""+DataTableRef+""+DataItemCode
		/// </summary>
		public CompanyDataItem this[string index] {get {return (CompanyDataItem)BaseGetItem(index);}}

		/// <summary>
		/// Returns CompanyUnitDataTablesCollection for given CompanyRef
		/// </summary>
		public CompanyDataItemCollection<T> this[int CompanyRef]
		{
			get
			{
				if(CompanyRef != Null.Integer || !CompanyRef.Equals(null))
				{
                    CompanyDataItemCollection<T> tempColl = new CompanyDataItemCollection<T>();
					foreach(T obj in this)
					{
						if(obj.CompanyUnitRef == CompanyRef)
							tempColl.Add(obj);
					}

					return tempColl;
				}
				return null;
			}
		}

		public CompanyDataItemCollection<T> this[CompanyUnitHierarchy CompanyUnitObj]
		{
			get
			{
				return this.GetAllDataItemForCompanyHierarchy(
					CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef, 
					CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);
			}
		}

		public CompanyDataItemCollection<T> this[
			CompanyUnitHierarchy CompanyUnitObj, int dataTableRef]
		{
			get
			{
				CompanyDataItemCollection<T> tempColl = 
					this.GetAllDataItemForCompanyHierarchy(
					CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef, 
					CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

				//tempColl = tempColl.Find(dataTableRef);

                return tempColl.Find(dataTableRef);
			}
		}

		public CompanyDataItemCollection<T> this[
			CompanyUnitHierarchy CompanyUnitObj, params int[] dataTableRefs]
		{
			get
			{
				CompanyDataItemCollection<T> tempColl = 
					this.GetAllDataItemForCompanyHierarchy(
					CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef, 
					CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

				//tempColl = tempColl.Find(dataTableRefs);

                return tempColl.Find(dataTableRefs);
			}
		}

//		public CompanyDataItemCollection this[
//			int CompanyRef, int CompanyParentRef, int CompanyGrandParentRef, int CompanyGrandGrandParentRef]
//		{
//			get
//			{
//				return this.GetAllDataItemForCompanyHierarchy(CompanyRef, CompanyParentRef, CompanyGrandParentRef, CompanyGrandGrandParentRef);
//			}
//		}
//		public CompanyDataItemCollection this[
//			int CompanyRef, int CompanyParentRef, int CompanyGrandParentRef, int CompanyGrandGrandParentRef,
//			int dataTableRef]
//		{
//			get
//			{
//				CompanyDataItemCollection tempColl = 
//					this.GetAllDataItemForCompanyHierarchy(CompanyRef, CompanyParentRef, CompanyGrandParentRef, CompanyGrandGrandParentRef);
//
//				tempColl = tempColl.Find(dataTableRef);
//
//				return tempColl;
//			}
//		}

		//================================================================================
		#endregion

		#region Methods
		//================================================================================
		/// <summary>
		/// Gets all DataTables for Given CompanyRef including there ParentCompanyRefs
		/// </summary>
		/// <param name="CompanyRef"></param>
		/// <param name="CompanyParentRef"></param>
		/// <param name="CompanyGrandParentRef"></param>
		/// <param name="CompanyGrandGrandParentRef"></param>
		/// <returns>CompanyUnitDataTablesCollection Object</returns>
		public CompanyDataItemCollection<T> GetAllDataItemForCompanyHierarchy(
			int CompanyRef, int CompanyParentRef, int CompanyGrandParentRef, int CompanyGreatGrandParentRef)
		{
            return this[CompanyRef].
                Merge(this[CompanyParentRef]).
                    Merge(this[CompanyGrandParentRef]).
                        Merge(this[CompanyGreatGrandParentRef]);

            //CompanyDataItemCollection<T> tempColl = this[CompanyRef];
            //CompanyDataItemCollection<T> tempColl1 = this[CompanyParentRef];
            //CompanyDataItemCollection<T> tempColl2 = this[CompanyGrandParentRef];
            //CompanyDataItemCollection<T> tempColl3 = this[CompanyGreatGrandParentRef];

            //// Merge into CompanyRef Collection from CompanyParentRef Collection
            //tempColl.Merge(tempColl1);
            //// Merge into First Merge Collection from CompanyGrandParentRef Collection
            //tempColl.Merge(tempColl2);
            //// Merge into First Merge Collection from CompanyGrandParentRef Collection
            //tempColl.Merge(tempColl3);

            //return tempColl;
		}
		//================================================================================
		public bool Contains(T item) {return BaseContains(item);}
		/// <summary>
		/// Finds DataTableRef within CompanyUnitDataTablesCollection
		/// </summary>
		/// <param name="dataItemCode"></param>
		/// <returns></returns>
		public bool Contains(string dataItemCode, int dataTableRef) 
		{
			if(this != null)
			{
				foreach(T obj in this)
				{
					if(obj.DataItemCode.ToUpper() == dataItemCode.ToUpper() && 
                        obj.DataTableRef == dataTableRef )
						return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Finds DataTableRef within CompanyUnitDataTablesCollection
		/// </summary>
		/// <param name="dataItemCode"></param>
		/// <returns></returns>
		public bool Contains(string dataItemCode) 
		{
			if(this != null || dataItemCode.Length == 0)
			{
				if(this.Find(dataItemCode) != null)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Finds and Return all DataItems for given DataTableRef within CompanyDataItemCollection
		/// </summary>
		/// <param name="DataTableRef"></param>
		/// <returns></returns>
        public CompanyDataItemCollection<T> Find(int dataTableRef) 
		{
			if(this != null && dataTableRef != Null.Integer)
			{
                CompanyDataItemCollection<T> tempColl = new CompanyDataItemCollection<T>();
				foreach(T obj in this)
				{
					if(obj.DataTableRef == dataTableRef)
						tempColl.Add(obj);
				}
				return tempColl;
			}
			return null;
		}

		/// <summary>
		/// Finds DataItemCode within CompanyDataItemCollection
		/// </summary>
		/// <param name="dataItemCode"></param>
		/// <returns></returns>
		public T Find(string dataItemCode)
		{
			foreach(T obj in this)
			{
				if(obj.DataItemCode == dataItemCode || 
					(obj.DataItemCode.ToUpper() == dataItemCode.ToUpper()))
					return obj;
			}
			return null;
		}
		/// <summary>
		/// Finds DataTableRefs within CompanyDataItemCollection
		/// </summary>
		/// <param name="dataTableRefs">Collection of int[dataTableRef]</param>
		/// <returns>CompanyDataItemCollection Object</returns>
        public CompanyDataItemCollection<T> Find(params int[] dataTableRefs) 
		{
			if(dataTableRefs.Length > 0)
			{
                CompanyDataItemCollection<T> tempColl = new CompanyDataItemCollection<T>();
				for(int i=0; i < dataTableRefs.Length && dataTableRefs[i] != Null.Integer; i++)
				{
					tempColl.Merge(this.Find(dataTableRefs[i]));
				}
				return tempColl;
			}
			return null;
		}
		public int IndexOf(CompanyDataItem item) {return BaseIndexOf((T)item);}
		//================================================================================
		public T Insert(int index, T item) {BaseInsert(index, item); return item;}
		public T Add(T item) { BaseAdd(item); return item;}
		/// <summary>
		/// Merge given CompanyDataItemCollection (mergeFromColl) using DataItemCode, DataTableRef 
		/// to existing CompanyDataItemCollection
		/// </summary>
		/// <param name="mergeFromColl"></param>
        public CompanyDataItemCollection<T> Merge(CompanyDataItemCollection<T> mergeFromColl) 
		{
			if(mergeFromColl != null)
			{
				foreach(T obj in mergeFromColl)
				{
					if(!this.Contains(obj.DataItemCode, obj.DataTableRef))
						this.Add(obj);
				}
			}
            return this;
		}
		/// <summary>
		/// Remove Duplicate DataItems (DataItemCode) within Collection
		/// </summary>
		/// <returns></returns>
        public CompanyDataItemCollection<T> RemoveDuplicateDataItems() 
		{
			if(this != null || this.Count > 0)
			{
                CompanyDataItemCollection<T> distinctDataItemColl = new CompanyDataItemCollection<T>();
				foreach(T obj in this)
				{
					if(!distinctDataItemColl.Contains(obj.DataItemCode))
						distinctDataItemColl.Add(obj);
				}
				return distinctDataItemColl;
			}
			return this;
		}
		//================================================================================		
		public void Remove(T item) {BaseRemove(item);}
		//================================================================================
		/// <summary>
		/// Binds DataEnumerations to respective DataItem
		/// </summary>
		/// <param name="enumColl"></param>
        public void BindDataEnumerationToEachDataItems(
            CompanyDataEnumerationCollection<CompanyDataEnumeration> enumColl)
		{
			foreach(T obj in this)
			{
				if(obj.EnumeratedInd)
				{
                    CompanyDataEnumerationCollection<CompanyDataEnumeration> dataItemenumColl =
                        new CompanyDataEnumerationCollection<CompanyDataEnumeration>();
                    dataItemenumColl = enumColl[obj.DataItemCode, obj.DataTableRef];
                    if (dataItemenumColl.Count != 0)
                    {
                        obj.DataEnumeration = dataItemenumColl;
                    }
				}
			}
		}
		#endregion
	}
}

