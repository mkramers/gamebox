using System;

namespace RenderCore
{
    public interface ICacheObjectProvider<out T, in TY> where T : class where TY : IEquatable<TY>
    {
        T GetObject(TY _id);
    }
}