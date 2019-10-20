using System;
using System.Collections.Generic;
using System.Text;

using Tejas;
using Tejas.Collections;
using Tejas.App;

namespace Tejas.App.BO
{
    /// <summary>
    /// Summary description for CompanyUnitDataTables.
    /// </summary>
    public class CompanyUnitDataTables : DataItemBase
	{
		#region Fields
		private int		_CompanyUnitRef = Null.Integer;
		private int		_DataTableRef = Null.Integer;
		private string	_DataTableDesc = Null.String;
		private DataTableUseTypeCategory
			_DataTableUseTypeCategoryCode = DataTableUseTypeCategory.AssetDataLive;
		private CompanyDataItemCollection<CompanyDataItem> _DataItem = null;
		private int[] _BindingDataTableRefs = new int[2];
		private string[] _BindingDataTableDescs = new string[2];
		private string _DataTableSqlScript = Null.String;
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
			set 
			{ 
				_DataTableRef = value; 
				_BindingDataTableRefs = BuildDataTableRefs(_DataTableRef);
			}
		}

		public string DataTableDesc
		{
			get { return _DataTableDesc; }
			set { _DataTableDesc = value; }
		}

		public DataTableUseTypeCategory 
			DataTableUseTypeCategoryCode
		{
			get { return _DataTableUseTypeCategoryCode; }
			set { _DataTableUseTypeCategoryCode = value; }
		}

        public CompanyDataItemCollection<CompanyDataItem> DataItems
        {
            get { return _DataItem; }
            set { _DataItem = value; }
        }

		public int[] BindingDataTableRefs
		{
			get { return _BindingDataTableRefs; }
		}

		public string[] BindingDataTableDescs
		{
			get { return _BindingDataTableDescs; }
			set { _BindingDataTableDescs = value; }
		}

		public string DataTableSqlScript
		{
			get { return _DataTableSqlScript; }
			set { _DataTableSqlScript = value; }
		}
		#endregion

        #region Overrides
        //================================================================================
        protected override void OnCancelEdit()
        {
            //base.OnCancelEdit();
            //
            CompanyUnitDataTables clone = base.EditClone as CompanyUnitDataTables;
            if (clone == null)
                return;
        }
        //================================================================================
        #endregion

		#region Methods
		protected override string GetKey()
		{
			return (CompanyUnitRef.ToString() + "" + DataTableUseTypeCategoryCode.ToString() + "" +
				DataTableRef.ToString());
		}
		/// <summary>
		/// Returns Associated DataTableRefs for Given DataTable
		/// </summary>
		/// <param name="dataTableRef"></param>
		/// <returns></returns>
		private int[] BuildDataTableRefs(int dataTableRef)
		{
			switch(dataTableRef)
			{
					#region RefMatch
					//------------ RefMatch Tables -------------------------------
				case 192:
					return new int[2]{192, Null.Integer};
					//break;
				case 191:
					return new int[2] {191, Null.Integer };
					//break;
				case 194: case 208:
					return new int[2]{208, Null.Integer};
					//break;
				case 195:
					return new int[2]{195, Null.Integer};
					//break;
				case 252:
					return new int[2]{252, Null.Integer};
					//break;
				case 431:
					return new int[2]{431, 429};
					//break;
				case 429:
					return new int[2]{429, 431};
					//break;
				case 430:
					return new int[2]{430, 431};
					//break;
				case 432:
					return new int[2]{432, 431};
					//break;
					//------------ END     ---------------------------------------
					#endregion

					#region PreCon
					//------------ PreCon Tables ---------------------------------
				case 196:
					return new int[2]{196, Null.Integer};
					//break;
				case 197:
					return new int[2] {197, Null.Integer };
					//break;
				case 198:
					return new int[2]{198, 197};
					//break;
				case 199:
					return new int[2]{199, 197};
					//break;
				case 200:
					return new int[2]{200, 197};
					//break;
				case 253:
					return new int[2]{253, 197};
					//break;
				case 233:
					return new int[2]{233, Null.Integer};
					//break;
				case 234:
					return new int[2]{234, 233};
					//break;
				case 222:
					return new int[2]{222, 233};
					//break;
				case 223:
					return new int[2]{223, 233};
					//break;
					//------------ END     --------------------------------------
					#endregion

					#region Consolidate
					//------------ Consolidate Tables ---------------------------
				case 201:
					return new int[2]{201, Null.Integer};
					//break;
				case 254:
					return new int[2]{254, Null.Integer };
					//break;
				case 202:
					return new int[2]{202, 201};
					//break;
				case 203:
					return new int[2]{203, 201};
					//break;
				case 204:
					return new int[2]{204, 201};
					//break;
				case 205:
					return new int[2]{205, 201};
					//break;
				case 261:
					return new int[2]{261, 259};
					//break;
				case 259:
					return new int[2]{259, 261};
					//break;
				case 260:
					return new int[2]{260, 261};
					//break;
				case 262:
					return new int[2]{262, 261};
					//break;
					//------------ END     --------------------------------------
					#endregion

					#region LiveAsset
					//------------ LiveAsset Tables -----------------------------
				case 174:
					return new int[2]{174, Null.Integer};
					//break;
				case 168:
					return new int[2]{168, 174};
					//break;
				case 176:
					return new int[2]{176, 174};
					//break;
				case 178:
					return new int[2]{178, 174};
					//break;
				case 179:
					return new int[2]{179, 174};
					//break;
				case 180:
					return new int[2]{180, 174};
					//break;
				case 255:
					return new int[2]{255, 174};
					//break;
				case 257:
					return new int[2]{257, 174};
					//break;
					//------------ END     --------------------------------------
					#endregion

				default:
					return new int[2]{dataTableRef, Null.Integer};
					//break;
			}
		}
		#endregion
	}

    /// <summary>
    /// Collection of CompanyUnitDataTables 
    /// </summary>
    public class CompanyUnitDataTablesCollection<T> : DataItemCollectionBase<T> where T : CompanyUnitDataTables
	{
		#region Properties
		//================================================================================		
		/// <summary>
		/// Index key created of 
		/// CompanyUnitRef+""+DataTableUseTypeCategoryCode+""+DataTableRef
		/// </summary>
		public T this[int index] {get {return (T)BaseGetItem(index);}}
        //================================================================================
		public CompanyUnitDataTablesCollection<T> this[
            DataTableUseTypeCategory dataTableUseTypeCategoryCode]
		{
			get
			{
                return this.FindAll(FindByDataTableUseTypeCategory(dataTableUseTypeCategoryCode));	
                
                //CompanyUnitDataTablesCollection<T> tempColl = new CompanyUnitDataTablesCollection<T>();
                //foreach(T obj in this)
                //{
                //    if(obj.DataTableUseTypeCategoryCode.Equals(dataTableUseTypeCategoryCode))
                //        tempColl.Add(obj);
                //}
                //return tempColl;
			}
		}
        //================================================================================
		public CompanyUnitDataTablesCollection<T> this[CompanyUnitHierarchy CompanyUnitObj]
		{
			get
			{
				return this.GetAllIncludingCompanyHierarchy(
					CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef, 
					CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);
			}
		}
        //================================================================================
		public CompanyUnitDataTablesCollection<T> this[
			CompanyUnitHierarchy CompanyUnitObj,
			DataTableUseTypeCategory dataTableUseTypeCategoryCode]
		{
			get
			{
				CompanyUnitDataTablesCollection<T> tempColl = 
					this.GetAllIncludingCompanyHierarchy(
					CompanyUnitObj.CompanyUnitRef, CompanyUnitObj.CompanyUnitParentRef, 
					CompanyUnitObj.CompanyUnitGrandParentRef, CompanyUnitObj.CompanyUnitGreatGrandParentRef);

				//tempColl = tempColl[dataTableUseTypeCategoryCode];

                return tempColl[dataTableUseTypeCategoryCode];
			}
		}
		//================================================================================
		#endregion

		#region Methods
        //================================================================================
        private Predicate<T> FindByDataTableUseTypeCategory(
            DataTableUseTypeCategory dataTableUseTypeCategoryCode)
        {
            return delegate(T obj)
            {
                if (obj.DataTableUseTypeCategoryCode == dataTableUseTypeCategoryCode)
                    return true;
                return false;
            };
        }
        //================================================================================
        private Predicate<T> FindByDataTableRef(int dataTableRef)
        {
            return delegate(T obj)
            {
                if (obj.DataTableRef == dataTableRef)
                    return true;
                return false;
            };
        }
        //================================================================================
        public CompanyUnitDataTablesCollection<T> FindAll(Predicate<T> match)
        {
            if (this == null)
            {
                throw new ArgumentNullException("Item collection empty.");
            }
            if (match == null)
            {
                throw new ArgumentNullException("Match condition empty.");
            }

            CompanyUnitDataTablesCollection<T> tempColl = new CompanyUnitDataTablesCollection<T>();

            foreach (T obj in this)
            {
                if (match(obj))
                    tempColl.Add(obj);
            }

            return tempColl;
        }
		//================================================================================
		/// <summary>
		/// Gets all DataTables for Given CompanyRef including there ParentCompanyRefs
		/// </summary>
		/// <param name="CompanyRef"></param>
		/// <param name="CompanyParentRef"></param>
		/// <param name="CompanyGrandParentRef"></param>
		/// <returns>CompanyUnitDataTablesCollection Object</returns>
		public CompanyUnitDataTablesCollection<T> GetAllIncludingCompanyHierarchy(
			int CompanyRef, int CompanyParentRef, int CompanyGrandParentRef, int CompanyGreatGrandParentRef)
		{
			return (((this.Find(CompanyRef)).Merge(
                        this.Find(CompanyParentRef))).Merge(
                            this.Find(CompanyGrandParentRef)).Merge(
                                this.Find(CompanyGreatGrandParentRef)));

            //CompanyUnitDataTablesCollection<T> tempColl = this.Find(CompanyRef);
            //CompanyUnitDataTablesCollection tempColl1 = this.Find(CompanyParentRef);
			//CompanyUnitDataTablesCollection tempColl2 = this.Find(CompanyGrandParentRef);
			//CompanyUnitDataTablesCollection tempColl3 = this.Find(CompanyGreatGrandParentRef);

			// Merge into CompanyRef Collection from CompanyParentRef Collection
			//tempColl.Merge(tempColl1);
			// Merge into First Merge Collection from CompanyGrandParentRef Collection
			//tempColl.Merge(tempColl2);
			// Merge into First Merge Collection from CompanyGrandParentRef Collection
			//tempColl.Merge(tempColl3);

			//return tempColl;
		}
		//================================================================================
		public bool Contains(T item) {return BaseContains(item);}
        //================================================================================
		/// <summary>
		/// Finds DataTableRef within CompanyUnitDataTablesCollection
		/// </summary>
		/// <param name="DataTableRef"></param>
		/// <returns></returns>
		public bool Contains(int DataTableRef) 
		{
			if(this != null)
			{
				foreach(T obj in this)
				{
					if(obj.DataTableRef == DataTableRef)
						return true;
				}
			}
			return false;
		}
        //================================================================================
		/// <summary>
		/// Returns CompanyUnitDataTablesCollection for given CompanyRef
		/// </summary>
		public CompanyUnitDataTablesCollection<T> Find(int CompanyRef)
		{
			if(CompanyRef != Null.Integer || !CompanyRef.Equals(null))
			{
                return this.FindAll(FindByDataTableRef(CompanyRef));
                //CompanyUnitDataTablesCollection<T> tempColl = new CompanyUnitDataTablesCollection<T>();
                //foreach(T obj in this)
                //{
                //    if(obj.CompanyUnitRef == CompanyRef)
                //        tempColl.Add(obj);
                //}
                //return tempColl;
			}
			return null;
		}
        //================================================================================
		/// <summary>
		/// Returns CompanyUnitDataTables for given CompanyRef
		/// </summary>
		public T Find(string tableDesc)
		{
			if(tableDesc != Null.String || !tableDesc.Equals(null))
			{
				foreach(T obj in this)
				{
					if(obj.DataTableDesc == tableDesc)
						return obj;
				}
			}
			return null;
		}
        //================================================================================
		/// <summary>
		/// Returns CompanyUnitDataTables for given DataTableRef
		/// </summary>
		public T FindForDataTableRef(int dataTableRef)
		{
			if(dataTableRef != Null.Integer)
			{
				foreach(T obj in this)
				{
					if(obj.DataTableRef == dataTableRef)
						return obj;
				}
			}
			return null;
		}
        //================================================================================
		public int IndexOf(T item) {return BaseIndexOf(item);}
        //================================================================================
		/// <summary>
		/// Returns DataTableRef for given Table Description else Null.Integer
		/// </summary>
		/// <param name="tableDesc">DataTable Name - Description</param>
		/// <returns>DataTableRef</returns>
		public int GetDataTableRef(string tableDesc)
		{
            CompanyUnitDataTables selectedDataTable = new CompanyUnitDataTables();
			selectedDataTable = this.Find(tableDesc);
			if(selectedDataTable == null)
			{
				return Null.Integer;
			}
			return selectedDataTable.DataTableRef;
		}
        //================================================================================
		/// <summary>
		/// Returns DataTable Name for given Table Description else Null.String
		/// </summary>
		/// <param name="tableRef">DataTable Ref - Primary key</param>
		/// <returns>Table Name - Description</returns>
		public string GetDataTableDesc(int tableRef)
		{
			CompanyUnitDataTables selectedDataTable = new CompanyUnitDataTables();
			selectedDataTable = this.FindForDataTableRef(tableRef);
			if(selectedDataTable == null)
			{
				return Null.String;
			}
			return selectedDataTable.DataTableDesc;
		}
		//================================================================================
		public T Insert(int index, T item) {BaseInsert(index, item); return item;}
        //================================================================================
		public T Add(T item) {BaseAdd(item); return item;}
        //================================================================================
		/// <summary>
		/// Merge given CompanyUnitDataTablesCollection (mergeFromColl) using DataTableRef 
		/// to existing CompanyUnitDataTablesCollection
		/// </summary>
		/// <param name="mergeFromColl"></param>
        public CompanyUnitDataTablesCollection<T> Merge(CompanyUnitDataTablesCollection<T> mergeFromColl) 
		{
			if(mergeFromColl != null)
			{
				foreach(T obj in mergeFromColl)
				{
					if(!this.Contains(obj.DataTableRef))
                        this.Add(obj);
				}
			}
            return this;
		}
		//================================================================================		
		public void Remove(T item) {BaseRemove(item);}
		//================================================================================
		/// <summary>
		/// Converts DataTableCollection -DataTableRefs into int[DataTableRefs]
		/// </summary>
		/// <returns>Int Array of DataTableRef</returns>
		public int[] DataTableRefsToIntArray()
		{
			int[] dataTableRefs = new int[this.Count];
			int i = 0;
			foreach(T dataTableObj in this)
			{
				dataTableRefs.SetValue(dataTableObj.DataTableRef, i++);
			}
			return dataTableRefs;
		}
		//================================================================================
		/// <summary>
		/// Binds DataDataItems to respective DataTable, Removes duplicate DataItems and 
		/// Assigned DataTableDesc from BindingDataTableRefs
		/// </summary>
		/// <param name="enumColl"></param>
		public void BindDataItemsToEachDataTable(CompanyDataItemCollection<CompanyDataItem> dataItemColl
            )
		{
			foreach(T obj in this)
			{
                CompanyDataItemCollection<CompanyDataItem> forEachDataTableDataItemColl =
                    new CompanyDataItemCollection<CompanyDataItem>();
                forEachDataTableDataItemColl = dataItemColl.Find(obj.BindingDataTableRefs);
                if (forEachDataTableDataItemColl.Count != 0)
                {
                    obj.DataItems = forEachDataTableDataItemColl.RemoveDuplicateDataItems();
                }
                string[] tableDescs = new string[obj.BindingDataTableRefs.Length];

                for (int i = 0; i < obj.BindingDataTableRefs.Length; i++)
                {
                    tableDescs[i] = this.GetDataTableDesc(obj.BindingDataTableRefs[i]);
                }
                obj.BindingDataTableDescs = tableDescs;

                string sqlScriptText = Null.String;
                //if (obj.DataItems != null)
                //    sqlScriptText = (new Company.DMS.BusinessEntities.SqlDataItemScriptBuilder())
                //                    .BuildDataItemSqlScript(obj);

				obj.DataTableSqlScript = sqlScriptText;
			}
		}
		//================================================================================
		#endregion
	}
}
