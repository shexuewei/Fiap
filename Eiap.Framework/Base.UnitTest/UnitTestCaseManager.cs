using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public class UnitTestCaseManager
    {
        public static List<object> GetParametersTypeList(object para)
        {
            List<object> list = new List<object>();
            Type paraType = para.GetType();
            if (paraType.IsValueType)
            {
                if (!paraType.IsEnum)
                {
                    switch (paraType.Name.ToLower())
                    {
                        case "int":
                            list.Add(int.MinValue);
                            list.Add(int.MaxValue);
                            list.Add(new Random().Next());
                            break;
                        case "long":
                            list.Add(int.MinValue);
                            list.Add(int.MaxValue);
                            list.Add(new Random().Next());
                            break;
                    }

                }
                else if (paraType.IsEnum)
                { }
            }
            else if (paraType.Name.ToLower() == "string")
            { }
            else if (paraType.IsGenericType && !paraType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            { }
            else if (paraType.IsGenericType && paraType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            { }
            else if (paraType.IsClass)
            { }
            return null;
        }
    }
}
