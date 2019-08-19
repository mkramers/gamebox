using System;

namespace RenderCore.Resource
{
    public interface IPathConverter<in T> where T : Enum
    {
        string GetPath(T _id);
    }
}