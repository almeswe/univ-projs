using System.Collections;
using System.Collections.Generic;

namespace Geometry.ShapeList
{
    public sealed class Shapes : IList<Shape>
    {
        private List<Shape> _container =
            new List<Shape>();

        public Shape this[int index] 
        { 
            get => this._container[index];
            set => this._container[index] = value; 
        }

        public int Count => this._container.Count;

        public bool IsReadOnly => false;

        public void Add(Shape item) =>
            this._container.Add(item);

        public void Clear() =>
            this._container.Clear();

        public bool Contains(Shape item) =>
            this._container.Contains(item);

        public void CopyTo(Shape[] array, int arrayIndex) =>
            this._container.CopyTo(array, arrayIndex);

        public IEnumerator<Shape> GetEnumerator() =>
            this._container.GetEnumerator();

        public int IndexOf(Shape item) =>
            this._container.IndexOf(item);

        public void Insert(int index, Shape item) =>
            this._container.Insert(index, item);

        public bool Remove(Shape item) =>
            this._container.Remove(item);

        public void RemoveAt(int index) =>
            this._container.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() =>
            this._container.GetEnumerator();
    }
}