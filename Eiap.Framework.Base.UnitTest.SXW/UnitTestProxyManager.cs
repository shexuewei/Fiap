﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestProxyManager : IUnitTestProxyManager
    {
        private IUnitTestProxyInterceptor _InterceptorInstance;
        public UnitTestProxyManager(IUnitTestProxyInterceptor interceptorInstance)
        {
            _InterceptorInstance = interceptorInstance;
        }

        public T Create<T>(object objInstance) where T : class
        {
            return (T)Create(typeof(T), objInstance);
        }

        public object Create(Type interfaceType, object objInstance)
        {
            Type interceptorType = _InterceptorInstance.GetType();
            Type instanceType = objInstance == null ? interfaceType : objInstance.GetType();
            //定义类型（持久化）
            //var asm = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(instanceType.Name), AssemblyBuilderAccess.RunAndSave);
            //瞬时
            var asm = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(instanceType.Name), AssemblyBuilderAccess.Run);
            //持久化模块
            //var modulebuilder = asm.DefineDynamicModule(instanceType.Name, instanceType.Name + "EiapProxy.dll");
            //瞬时模块
            var modulebuilder = asm.DefineDynamicModule(instanceType.Name + "EiapUnitTestProxy.dll");
            var typeBuilder = modulebuilder.DefineType(instanceType.Name + "EiapUnitTestProxy", TypeAttributes.Public, typeof(object), new Type[] { interfaceType });

            //定义字段
            var interceptorField = typeBuilder.DefineField("_" + interceptorType.Name, interceptorType, FieldAttributes.Private);
            var instanceField = typeBuilder.DefineField("_" + instanceType.Name, instanceType, FieldAttributes.Private);

            //定义属性
            //var instanceProperty = typeBuilder.DefineProperty(instanceType.Name, PropertyAttributes.HasDefault, instanceType, Type.EmptyTypes);
            //var interceptorProperty = typeBuilder.DefineProperty(interceptorType.Name, PropertyAttributes.HasDefault, interceptorType, Type.EmptyTypes);

            //定义属性方法
            //var setInstancePropertyMethodBuilder = typeBuilder.DefineMethod("Set_" + instanceProperty.Name, MethodAttributes.Public, null, new Type[] { instanceType });
            //var setInterceptorPropertyMethodBuilder = typeBuilder.DefineMethod("Set_" + interceptorType.Name, MethodAttributes.Public, null, new Type[] { interceptorType });

            //ILGenerator setInstanceField_il = setInstancePropertyMethodBuilder.GetILGenerator();
            //setInstanceField_il.Emit(OpCodes.Ldarg_0);
            //setInstanceField_il.Emit(OpCodes.Ldarg_2);
            //setInstanceField_il.Emit(OpCodes.Stfld, instanceField);
            //setInstanceField_il.Emit(OpCodes.Ret);

            //ILGenerator setInterceptorField_il = setInterceptorPropertyMethodBuilder.GetILGenerator();
            //setInterceptorField_il.Emit(OpCodes.Ldarg_0);
            //setInterceptorField_il.Emit(OpCodes.Ldarg_1);
            //setInterceptorField_il.Emit(OpCodes.Stfld, interceptorField);
            //setInterceptorField_il.Emit(OpCodes.Ret);

            //关联，分别把set方法跟相应的属性中set块对应
            //instanceProperty.SetSetMethod(setInstancePropertyMethodBuilder);
            //interceptorProperty.SetSetMethod(setInterceptorPropertyMethodBuilder);

            //无参构造函数
            //ConstructorBuilder nonConBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, Type.EmptyTypes);
            //ILGenerator nonConIl = nonConBuilder.GetILGenerator();
            //nonConIl.Emit(OpCodes.Ret);

            //两个参数的构造函数
            var constructorbuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, new Type[] { interceptorType, instanceType });//new Type[] { instanceType }
            var conil = constructorbuilder.GetILGenerator();
            conil.Emit(OpCodes.Ldarg_0);
            conil.Emit(OpCodes.Ldarg_1);
            //conil.Emit(OpCodes.Newobj, interceptorType.GetConstructor(Type.EmptyTypes));
            conil.Emit(OpCodes.Stfld, interceptorField);
            conil.Emit(OpCodes.Ldarg_0);
            conil.Emit(OpCodes.Ldarg_2);
            conil.Emit(OpCodes.Stfld, instanceField);
            conil.Emit(OpCodes.Ret);

            //获取接口及基类接口方法
            var methods = interfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();
            Type[] baseInterface = interfaceType.GetInterfaces();
            foreach (Type baseInterfaceItem in baseInterface)
            {
                methods.AddRange(baseInterfaceItem.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList());
            }
            var methodList = GetInterfaceMethod(methods);
            var interfaceTypeName = interfaceType.FullName;
            //构造代理方法
            for (var i = 0; i < methods.Count; i++)
            {
                var paramtypes = GetParametersType(methods[i]);
                MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual;
                var methodbuilder = typeBuilder.DefineMethod(methods[i].Name, methodAttributes, CallingConventions.Standard, methods[i].ReturnType, paramtypes);
                var il = methodbuilder.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, interceptorField);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, instanceField);
                il.Emit(OpCodes.Ldstr, interfaceTypeName);
                il.Emit(OpCodes.Ldstr, methods[i].Name);
                
                if (paramtypes == null)
                {
                    il.Emit(OpCodes.Ldnull);
                }
                else
                {
                    var parameters = il.DeclareLocal(typeof(object[]));
                    il.Emit(OpCodes.Ldc_I4, paramtypes.Length);
                    il.Emit(OpCodes.Newarr, typeof(object));
                    il.Emit(OpCodes.Stloc, parameters);
                    for (var j = 0; j < paramtypes.Length; j++)
                    {
                        il.Emit(OpCodes.Ldloc, parameters);
                        il.Emit(OpCodes.Ldc_I4, j);
                        il.Emit(OpCodes.Ldarg, j + 1);
                        il.Emit(OpCodes.Box, paramtypes[j]);
                        il.Emit(OpCodes.Stelem_Ref);
                    }
                    il.Emit(OpCodes.Ldloc, parameters);
                }
                il.Emit(OpCodes.Callvirt, interceptorType.GetMethod("Invoke"));
                if (methods[i].ReturnType == typeof(void))
                {
                    il.Emit(OpCodes.Pop);
                }
                else if (methods[i].ReturnType.IsValueType)
                {
                    il.Emit(OpCodes.Unbox_Any, methods[i].ReturnType);
                }
                il.Emit(OpCodes.Ret);
            }
            var t = typeBuilder.CreateType();
            //持久化
            return Activator.CreateInstance(t, new object[] { _InterceptorInstance, objInstance });
        }

        private List<MethodInfo> GetInterfaceMethod(List<MethodInfo> allMethod)
        {
            bool issame = false;
            List<MethodInfo> methodList = new List<MethodInfo>();
            //TODO:循环算法可以优化
            for (int i = 0; i < allMethod.Count; i++)
            {
                for (int j = i + 1; j < allMethod.Count; j++)
                {
                    if (IsSameMethod(allMethod[i], allMethod[j]))
                    {
                        issame = true;
                    }
                }
                if (!issame)
                {
                    methodList.Add(allMethod[i]);
                }
                issame = false;
            }
            return methodList;
        }

        private Type[] GetParametersType(MethodInfo method)
        {
            Type[] types = null;
            if (method != null)
            {
                var param = method.GetParameters();
                if (param.Length > 0)
                {
                    types = new Type[param.Length];
                    for (var i = 0; i < param.Length; i++)
                    {
                        types[i] = param[i].ParameterType;
                    }
                }

            }
            return types;
        }

        private bool IsSameMethod(MethodInfo method1, MethodInfo method2)
        {
            if (method1.Name != method2.Name)
            {
                return false;
            }
            ParameterInfo[] para1 = method1.GetParameters();
            ParameterInfo[] para2 = method2.GetParameters();
            if(para1.Length != para2.Length)
            {
                return false;
            }
            int paraLen = para1.Length;
            for (int i = 0; i < paraLen; i++)
            {
                if (!IsSameParameterType(para1[i], para2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSameParameterType(ParameterInfo para1, ParameterInfo para2)
        {
            bool res = false;
            if (para1.ParameterType.FullName == para2.ParameterType.FullName)
            {
                res = true;
            }
            return res;
        }
    }
}
