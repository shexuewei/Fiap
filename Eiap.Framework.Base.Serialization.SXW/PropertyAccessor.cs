using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class PropertyAccessor<TInstance, TValue> : IPropertyAccessor
    {
        private Func<TInstance, TValue> GetValueDelegate;
        private Action<TInstance, TValue> SetValueDelegate;

        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            GetValueDelegate = (Func<TInstance, TValue>)Delegate.CreateDelegate(typeof(Func<TInstance, TValue>), propertyInfo.GetGetMethod());
            SetValueDelegate = (Action<TInstance, TValue>)Delegate.CreateDelegate(typeof(Action<TInstance, TValue>), propertyInfo.GetSetMethod());
        }

        public object GetValue(object instance)
        {
            return GetValueDelegate((TInstance)instance);
        }

        public void SetValue(object instance, object newValue)
        {
            SetValueDelegate((TInstance)instance, (TValue)newValue);
        }
    }
}
