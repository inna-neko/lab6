using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.Iterator
{
    public class Iterator<TValue>
    {
        private readonly IList<TValue> _list;

        public bool HasNext => _list.Count < _position;

        public TValue Value => _list[_position];

        private int _position;

        public Iterator(IList<TValue> list)
        {
            _list = list;
        }

        public void Next()
        {
            if (HasNext) throw new Exception();

            _position++;
        }
    }
}
