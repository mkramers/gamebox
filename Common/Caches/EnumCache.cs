﻿using System;
using Common.Cache;

namespace Common.Caches
{
    public class EnumCache<T, TY> : Cache<T, TY> where T : class where TY : Enum
    {
    }
}