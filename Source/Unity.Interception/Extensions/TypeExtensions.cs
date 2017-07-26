using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    internal static class TypeExtensions
    {
        public static PropertyInfo GetProperty(this Type type, string propertyName, BindingFlags bindingAttr, object binder, Type returnType, Type[] types, object[] modifiers)
        {
            if (binder != null) throw new NotSupportedException("Parameter binder must be null.");
            if (modifiers != null) throw new NotSupportedException("Parameter modifiers must be null.");

            ////return type.GetProperties(bindingAttr)
            ////    .SingleOrDefault(property => property.GetIndexParameters().Select(p => p.ParameterType).SequenceEqual(types));

            //ToDo: Need to check binder bindingAttr as well 



            return type.GetProperty(propertyName, returnType, types);
        }

        public static bool IsSubclassOf(this Type type, Type parentType)
        {
            return type.GetTypeInfo().IsSubclassOf(parentType);
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsPrimitive(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive;
        }

        public static Type GetBaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        public static TypeAttributes GetTypeAttributes(this Type type)
        {
            return type.GetTypeInfo().Attributes;
        }

        public static Module GetModule(this Type type)
        {
            return type.GetTypeInfo().Module;
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsGenericTypeDefinition(this Type type)
        {
            return type.IsConstructedGenericType;
        }

        public static bool IsValueType(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsVisible(this Type type)
        {
            return type.GetTypeInfo().IsVisible;
        }

        public static bool IsInterface(this Type type)
        {
            return type.GetTypeInfo().IsInterface;
        }

        public static bool IsClass(this Type type)
        {
            return type.GetTypeInfo().IsClass;
        }

        public static bool IsPublic(this Type type)
        {
            return type.GetTypeInfo().IsPublic;
        }

        public static bool IsNestedPublic(this Type type)
        {
            return type.GetTypeInfo().IsNestedPublic;
        }

        public static bool IsSealed(this Type type)
        {
            return type.GetTypeInfo().IsSealed;
        }

        public static bool IsAbstract(this Type type)
        {
            return type.GetTypeInfo().IsAbstract;
        }

        public static InterfaceMapping GetInterfaceMap(this Type type, Type interfaceType)
        {
            return type.GetTypeInfo().GetRuntimeInterfaceMap(interfaceType);
        }

        public static GenericParameterAttributes GetGenericParameterAttributes(this Type type)
        {
            return type.GetTypeInfo().GenericParameterAttributes;
        }

        public static Type[] GetGenericParameterConstraints(this Type type)
        {
            return type.GetTypeInfo().GetGenericParameterConstraints();
        }

        public static object GetPropertyValue(this Object instance, string propertyValue)
        {
            return instance.GetType().GetTypeInfo().GetDeclaredProperty(propertyValue).GetValue(instance);
        }

        public static string GetAssemblyQualifiedNameWithoutVersion(this Type type)
        {
            var parts = type.AssemblyQualifiedName.Split(',');
            return String.Format("{0}, {1}", parts[0].Trim(), parts[1].Trim());
        }
    }
}
