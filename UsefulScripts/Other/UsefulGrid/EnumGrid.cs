using System;
using System.Collections.Generic;
using System.Linq;
using LeopotamGroup.Math;
using UnityEngine;

namespace UsefulScripts.Other.UsefulGrid
{
    public class EnumGrid<T, V> where T : struct where V : Enum
    {
        private readonly Bounds2i _bounds2I;
        private readonly Dictionary<V, Grid<T>> _grids = new Dictionary<V, Grid<T>>();
        private readonly Vector2Int _max;
        private readonly Vector2Int _min;

        private readonly Vector2Int _size;

        public EnumGrid(Bounds2i bounds2I)
        {
            _size = bounds2I.Max - bounds2I.Min;
            _min = bounds2I.Min;
            _max = bounds2I.Max;
            _bounds2I = bounds2I;
        }

        public EnumGrid(Vector2Int size)
        {
            _size = size;
            _min = Vector2Int.zero;
            _max = size;
            _bounds2I = new Bounds2i(_min, _max);
        }

        public EnumGrid(Vector2Int min, Vector2Int max)
        {
            _size = max - min;
            _min = min;
            _max = max;
            _bounds2I = new Bounds2i(min, max);
        }

        public EnumGrid(int w, int h)
        {
            var size = new Vector2Int(w, h);
            _size = size;
            _min = Vector2Int.zero;
            _max = new Vector2Int(15, 255);
            _bounds2I = new Bounds2i(_min, _max);

            //Debug.Log(size);
        }

        public EnumGrid(int w, int h, Vector2Int min, Vector2Int max)
        {
            var size = new Vector2Int(w, h);
            _size = size;
            _min = min;
            _max = max;
        }

        public Bounds2i Bounds2I => _bounds2I;

        public void Init(params V[] enums)
        {
            foreach (var enumm in enums)
            {
                var grid = new Grid<T>(_size, _bounds2I);
                _grids.Add(enumm, grid);
            }
        }

        public void Set(Vector2Int pos, T value, V key)
        {
            if (_grids.TryGetValue(key, out var grid))
                grid.Set(pos, value);
        }

        public T Get(Vector2Int pos, V key)
        {
            return _grids.TryGetValue(key, out var grid) ? grid.Get(pos) : default;
        }

        public bool Has(Vector2Int pos, V key)
        {
            return _grids.TryGetValue(key, out var grid) && grid.Has(pos);
        }

        public bool HasBounds(Bounds2i bounds2I, V key)
        {
            return _grids.TryGetValue(key, out var grid) && grid.HasBounds(bounds2I);
        }

        public T Destroy(Vector2Int pos, V key)
        {
            if (_grids.TryGetValue(key, out var grid))
                return grid.Destroy(pos);
            return default;
        }

        public bool InBounds(Vector2Int pos)
        {
            return _bounds2I.Contains(pos);
        }

        public Grid<T> GetGrid(V key)
        {
            return _grids[key];
        }

        public List<T> GetAll()
        {
            return _grids.SelectMany(grid => grid.Value.GetAll()).ToList();
        }

        public void ClearAll()
        {
            foreach (var grid in _grids)
                grid.Value.Clear();
        }
    }
}