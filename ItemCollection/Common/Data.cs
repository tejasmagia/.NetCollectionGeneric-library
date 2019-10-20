using System;
using System.Data;

namespace Tejas.Data
{
    [Serializable]
    public class DbVersion
    {
        #region Fields
        //================================================================================
        private object _value = null;
        private System.Data.DataRowState _state = DataRowState.Unchanged;
        //================================================================================
        #endregion

        #region Constructors
        //================================================================================
        public DbVersion() { }
        public DbVersion(object value) { _value = value; }
        //================================================================================
        #endregion

        #region Properties
        //================================================================================
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        //================================================================================
        public System.Data.DataRowState State
        {
            get { return _state; }
            set { _state = value; }
        }
        //================================================================================
        #endregion

        #region Methods
        //================================================================================
        public bool Equals(DbVersion version)
        {
            if (this.State != version.State)
                return false;
            else if (this.Value is byte[] && version.Value is byte[])
            {
                byte[] value1 = this.Value as byte[];
                byte[] value2 = version.Value as byte[];
                //
                if (value1.Length != value2.Length)
                    return false;
                else
                {
                    for (int i = 0; i < value1.Length; i++)
                        if (value1[i] != value2[i])
                            return false;
                    //
                    return true;
                }
            }
            else
                return this.Value.Equals(version.Value);
        }
        //================================================================================
        #endregion
    }
}
