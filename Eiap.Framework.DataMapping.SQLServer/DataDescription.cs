using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataMapping.SQLServer
{
    public class DataDescription
    {
        public string PrimaryKeyName { get; set; }

        public string SelectSQL { get; set; }

        public string SelectAllSQL { get; set; }

        public string TableName { get; set; }

        public string InsertSQL { get; set; }

        public string UpdateSQL { get; set; }

        public string DeleteSQL { get; set; }

        public string JoinSQL { get; set; }

        public string PrimaryKeyParameterName { get; set; }
    }
}
