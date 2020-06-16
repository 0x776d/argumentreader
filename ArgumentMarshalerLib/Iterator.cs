using System;
using System.Collections.Generic;

namespace ArgumentMarshalerLib
{
    public class Iterator<T>
    {
        private readonly IList<T> _list;
        private int index;

        public Iterator(IList<T> list)
        {
            _list = list ?? throw new NullReferenceException();
        }

        public T Current => this._list[index];

        public bool HasNext => index < _list.Count;

        public T Next()
        {
            return _list[index++];
        }

        public T Previous()
        {
            return _list[--index];
        }
    }
}
