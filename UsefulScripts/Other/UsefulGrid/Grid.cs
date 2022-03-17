using System.Collections;
using System.Collections.Generic;
using LeopotamGroup.Math;
using UnityEngine;

namespace UsefulScripts.Other.UsefulGrid
{
    public class Grid<T> : IEnumerable where T : struct
    {
        private T[,] _grid;

        private bool[,] _hasGrid;

        public Grid(Vector2Int size)
        {
            Init(size, Vector2Int.zero, size);
        }

        public Grid(Vector2Int size, Vector2Int min, Vector2Int max)
        {
            Init(size, min, max);
        }

        public Grid(Vector2Int size, Bounds2i bounds2I)
        {
            Init(size, bounds2I);
        }

        public Grid(int w, int h)
        {
            Init(new Vector2Int(w, h), Vector2Int.zero, new Vector2Int(w, h));
        }

        public Grid(int w, int h, Vector2Int min, Vector2Int max)
        {
            Init(new Vector2Int(w, h), min, max);
        }

        private Vector2Int Size { get; set; } = Vector2Int.zero;
        private Bounds2i Bounds { get; set; }

        public IEnumerator GetEnumerator()
        {
            return _grid.GetEnumerator();
        }

        private void Init(Vector2Int size, Vector2Int min, Vector2Int max)
        {
            Init(size, new Bounds2i
            {
                Min = min,
                Max = new Vector2Int(15, 255)
            });
        }

        private void Init(Vector2Int size, Bounds2i bounds2I)
        {
            Size = size;
            Bounds = bounds2I;
            var min = bounds2I.Min;
            var max = bounds2I.Max;

            _hasGrid = new bool[min.x + max.x + 1, min.y + max.y + 1];
            _grid = new T[min.x + max.x + 1, min.y + max.y + 1];
        }

        public void Clear()
        {
            Init(Size, Bounds.Min, Bounds.Max);
        }

        public void Set(Vector2Int pos, T value)
        {
            if (InBounds(pos))
            {
                Prepare(ref pos);

                _grid[pos.x, pos.y] = value;
                _hasGrid[pos.x, pos.y] = true;
            }
        }

        public T Get(Vector2Int pos)
        {
            if (InBounds(pos))
            {
                Prepare(ref pos);
                return _grid[pos.x, pos.y];
            }

            return default;
        }

        public bool Has(Vector2Int pos)
        {
            if (InBounds(pos))
            {
                Prepare(ref pos);
                return _hasGrid[pos.x, pos.y];
            }

            return false;
        }

        public bool HasBounds(Bounds2i bounds2I)
        {
            var min = bounds2I.Min;
            var max = bounds2I.Max;
            if (InBounds(min) && InBounds(max))
                for (var i = min.x; i < max.x; i++)
                for (var j = min.y; j < max.y; j++)
                    if (_hasGrid[i, j])
                        return true;

            return false;
        }

        public T Destroy(Vector2Int pos)
        {
            if (InBounds(pos))
            {
                var a = _grid[pos.x, pos.y];
                Prepare(ref pos);
                _grid[pos.x, pos.y] = default;
                _hasGrid[pos.x, pos.y] = false;
                return a;
            }

            return default;
        }

        public List<T> GetAll()
        {
            var list = new List<T>();
            for (var i = 0; i < _grid.GetLength(0); i++)
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                var t = _grid[i, j];
                if (_hasGrid[i, j])
                    list.Add(t);
            }

            return list;
        }

        private bool InBounds(Vector2Int pos)
        {
            return Bounds.Contains(pos);
        }

        private void Prepare(ref Vector2Int pos)
        {
            pos -= Bounds.Min;
        }
    }
}