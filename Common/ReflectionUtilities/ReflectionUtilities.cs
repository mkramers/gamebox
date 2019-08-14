﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.ReflectionUtilities
{
    /// <summary>
    /// derived from https://stackoverflow.com/a/2362756/1620721
    /// </summary>
    public static class ReflectionUtilities
    {
        public static List<Type> FindAllDerivedTypes<T>()
        {
            return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }

        public static List<Type> FindAllDerivedTypes<T>(Assembly _assembly)
        {
            Type derivedType = typeof(T);
            return _assembly
                .GetTypes()
                .Where(_type =>
                    _type != derivedType &&
                    derivedType.IsAssignableFrom(_type)
                ).ToList();
        }
    }
}