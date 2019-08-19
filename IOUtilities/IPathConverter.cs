using System;

namespace IOUtilities
{
    public interface IPathConverter<in T> where T : Enum
    {
        string GetPath(T _id);
    }
}