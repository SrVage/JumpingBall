using UnityEngine;

namespace Code.Abstractions.Interfaces
{
    public interface IPool<T>
    {
        public T GetObject();
        public void ReturnToPool(T obj);
    }
}