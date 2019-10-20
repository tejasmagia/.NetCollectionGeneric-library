using System;
using System.Collections.Generic;
using System.Text;
using Tejas;
using Tejas.Collections;
using Tejas.App;

namespace Tejas.App.BO
{
    /// <summary>
    /// Summary description for CompanyUnitHierarchy
    /// </summary>
    public class CompanyUnitHierarchy : DataItemByIdBase
    {
        #region Fields
        private int _CompanyUnitRef = Null.Integer;
        private int _CompanyUnitParentRef = Null.Integer;
        private int _CompanyUnitGrandParentRef = Null.Integer;
        private int _CompanyUnitGreatGrandParentRef = Null.Integer;
        private string _CompanyUnitDesc = Null.String;
        private string _CompanyUnitParentDesc = Null.String;
        private string _CompanyUnitGrandParentDesc = Null.String;
        private string _CompanyUnitGreatGrandParentDesc = Null.String;

        private CompanyUnitDataTablesCollection<CompanyUnitDataTables> _DataTables = null;
        #endregion

        #region Properties

        public new int Id
        {
            get { return base.Id; }
            set { base.Id = value; _CompanyUnitRef = value; }
        }

        public int CompanyUnitRef
        {
            get { return _CompanyUnitRef; }
            set { _CompanyUnitRef = value; }
        }

        public int CompanyUnitParentRef
        {
            get { return _CompanyUnitParentRef; }
            set { _CompanyUnitParentRef = value; }
        }

        public int CompanyUnitGrandParentRef
        {
            get { return _CompanyUnitGrandParentRef; }
            set { _CompanyUnitGrandParentRef = value; }
        }

        public int CompanyUnitGreatGrandParentRef
        {
            get { return _CompanyUnitGreatGrandParentRef; }
            set { _CompanyUnitGreatGrandParentRef = value; }
        }

        public string CompanyUnitDesc
        {
            get { return _CompanyUnitDesc; }
            set { _CompanyUnitDesc = value; }
        }

        public string CompanyUnitParentDesc
        {
            get { return _CompanyUnitParentDesc; }
            set { _CompanyUnitParentDesc = value; }
        }

        public string CompanyUnitGrandParentDesc
        {
            get { return _CompanyUnitGrandParentDesc; }
            set { _CompanyUnitGrandParentDesc = value; }
        }

        public string CompanyUnitGreatGrandParentDesc
        {
            get { return _CompanyUnitGreatGrandParentDesc; }
            set { _CompanyUnitGreatGrandParentDesc = value; }
        }

        public CompanyUnitDataTablesCollection<CompanyUnitDataTables> DataTables
        {
            get { return _DataTables; }
            set { _DataTables = value; }
        }
        #endregion

        #region Methods
        public CompanyUnitHierarchy Clone()
        {
            CompanyUnitHierarchy cloneObj = new CompanyUnitHierarchy();
            cloneObj.CompanyUnitRef = this.CompanyUnitRef;
            cloneObj.CompanyUnitParentRef = this.CompanyUnitParentRef;
            cloneObj.CompanyUnitGrandParentRef = this.CompanyUnitGrandParentRef;
            cloneObj.CompanyUnitGreatGrandParentRef = this.CompanyUnitGreatGrandParentRef;
            cloneObj.DataTables = this.DataTables;
            return cloneObj;
        }
        #endregion
    }

    /// <summary>
    /// Collection of CompanyUnitHierarchy 
    /// </summary>
    public class CompanyUnitHierarchyCollection<T> : DataItemByIdCollectionBase<T> where T : CompanyUnitHierarchy
    {
        #region Properties
        //================================================================================		
        public T this[int index] { get { return (T)BaseGetItem(index); } }
        //================================================================================
        #endregion

        #region Methods
        //================================================================================
        /// <summary>
        /// Find CompanyUnitHierarchy Object using CompanyUnitRef
        /// </summary>
        /// <param name="id">CompanyUnitRef int</param>
        /// <returns>CompanyUnitHierarchy Object</returns>
        public T Find(int id)
        {
            //CompanyUnitHierarchy obj = new CompanyUnitHierarchy();
            //obj = (T)BaseGetItem(MakeKey(id));
            return (T)BaseGetItem(MakeKey(id));
        }

        /// <summary>
        /// Find CompanyUnitHierarchy Object using CompanyUnitRef and Filters DataTableUseTypeCategory for 
        /// CompanyUnitObject
        /// </summary>
        /// <param name="id">CompanyUnitRef - Primary Key</param>
        /// <param name="dataTableUseTypeCategoryCode">DataTableUseTypeCategory</param>
        /// <returns></returns>
        public T Find(int id,
            DataTableUseTypeCategory dataTableUseTypeCategoryCode)
        {
            T companyUnit = (T)Find(id).Clone();
            CompanyUnitDataTablesCollection<CompanyUnitDataTables> dataTableColl =
                new CompanyUnitDataTablesCollection<CompanyUnitDataTables>();
            dataTableColl.Merge(companyUnit.DataTables[dataTableUseTypeCategoryCode]);
            companyUnit.DataTables = dataTableColl;
            return companyUnit;
        }
        //================================================================================
        public bool Contains(T item) { return BaseContains(item); }
        public int IndexOf(T item) { return BaseIndexOf(item); }
        //================================================================================
        public T Insert(int index, T item) { BaseInsert(index, item); return item; }
        public T Add(T item) { BaseAdd(item); return item; }
        //================================================================================		
        public void Remove(T item) { BaseRemove(item); }
        //================================================================================
        /// <summary>
        /// Bind CompanyUnitDataTablesCollection, CompanyDataItemCollection & CompanyDataEnumerationCollection
        /// to each CompanyUnit from CompanyUnitHierarchyCollection
        /// </summary>
        /// <param name="allDataTables"></param>
        /// <param name="allDataItems"></param>
        /// <param name="allDataEnumeration"></param>
        /// <returns></returns>
        public void BindDataTablesToEachCompanyUnitHierarchy
            (CompanyUnitDataTablesCollection<CompanyUnitDataTables> allDataTables,
            CompanyDataItemCollection<CompanyDataItem> allDataItems,
            CompanyDataEnumerationCollection<CompanyDataEnumeration> allDataEnumeration
            )
        {
            foreach (T companyUnitObj in this)
            {
                CompanyUnitDataTablesCollection<CompanyUnitDataTables> companyUnitDataTableColl = 
                    allDataTables[companyUnitObj];

                CompanyDataItemCollection<CompanyDataItem> companyUnitDataItemColl = allDataItems[companyUnitObj];

                CompanyDataEnumerationCollection<CompanyDataEnumeration> companyUnitDataEnumerationColl = 
                    allDataEnumeration[companyUnitObj];

                companyUnitDataItemColl.BindDataEnumerationToEachDataItems(companyUnitDataEnumerationColl);

                companyUnitDataTableColl.BindDataItemsToEachDataTable(companyUnitDataItemColl);

                if (companyUnitDataTableColl.Count > 0)
                {
                    companyUnitObj.DataTables = companyUnitDataTableColl;
                }
            }
        }
        #endregion
    }

    public enum DataTableUseTypeCategory : int
    {
        NoneUnknownOrUndefined = 0,
        ControlAndSecurityDatabase = 1,
        SubmissionDataRepository = 2,
        TransactionalAssetDataRepository = 3,
        ReportingDataRepository = 4,
        ReportingApplicationFactDataDb = 5,
        CompanyApplicationDataEntry = 6,
        CAndSEntityTable = 7,
        CAndSStandardTypeCodeTable = 8,
        CAndSEntityRelationshipTable = 9,
        CoreDataRepositoryTable = 10,
        CoreDataStagingTable = 11,
        CompanyApplicationProcessEntry = 12,
        DimensionTableBenchmark = 16,
        DimensionTablePropertyGlobal = 17,
        FactTableFund = 18,
        FactTableBenchmark = 19,
        DimensionTableAsset = 20,
        DimensionTableFund = 21,
        AssetStatic = 22,
        Periodic = 23,
        SubmissionImport = 25,
        SubmissionConglormation = 26,
        SubmissionReferenceMatch = 27,
        SubmissionPreConsolidation = 28,
        SubmissionConsolidation = 29,
        AssetDataLive = 30
    }
}
