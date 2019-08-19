using System;

namespace RenderCore.Resource
{
    public interface IResourceManager<in TType, TValue> where TType : Enum
    {
        Resource<TValue> GetResource(TType _id);
    }
}