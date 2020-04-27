using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class DBOracleParameter
    {

        #region Properties

        private string _ParaName;
        public string ParaName
        {
            get { return _ParaName; }
            set { _ParaName = value; }
        }

        private string _ParaType;
        public string ParaType
        {
            get { return _ParaType; }
            set { _ParaType = value; }
        }

        private string _ParaValue;
        public string ParaValue
        {
            get { return _ParaValue; }
            set { _ParaValue = value; }
        }

        private bool _isOutput;
        public bool isOutput
        {
            get { return _isOutput; }
            set { _isOutput = value; }
        }

        private int _Size;
        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        #endregion

        #region Constructor

        public DBOracleParameter()
        {
        }

        public DBOracleParameter(string pParaName, string pParaValue)
        {
            this.ParaName = pParaName;
            this.ParaValue = pParaValue;
            this.isOutput = false;
        }

        public DBOracleParameter(string pParaName, string pParaType, string pParaValue)
        {
            this.ParaName = pParaName;
            this.ParaType = pParaType;
            this.ParaValue = pParaValue;
            this.isOutput = false;
        }

        public DBOracleParameter(string pParaName, string pParaValue, bool pisOutput, int pSize)
        {
            this.ParaName = pParaName;
            this.ParaValue = pParaValue;
            this.isOutput = pisOutput;
            this.Size = pSize;
        }

        #endregion

    }
}
