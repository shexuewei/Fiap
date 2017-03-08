using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonSymbol
    {
        public const string JsonArraySymbol_Begin = "[";
        public const string JsonArraySymbol_End = "]";
        public const string JsonObjectSymbol_Begin = "{";
        public const string JsonObjectSymbol_End = "}";
        public const string JsonQuotesSymbol = "\"";
        public const string JsonNullSymbol = "null";
        public const string JsonSeparateSymbol = ",";
        public const string JsonPropertySymbol = ":";
    }
}
