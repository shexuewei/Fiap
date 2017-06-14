using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonSymbol
    {
        public const char JsonArraySymbol_Begin = '[';
        public const char JsonArraySymbol_End = ']';
        public const char JsonObjectSymbol_Begin = '{';
        public const char JsonObjectSymbol_End = '}';
        public const char JsonQuotesSymbol = '"';
        public const string JsonNullSymbol = "null";
        public const char JsonSeparateSymbol = ',';
        public const char JsonPropertySymbol = ':';
        public const char JsonSpaceSymbol = ' ';
    }
}
