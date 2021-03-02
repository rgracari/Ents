using System;
using System.Collections.Generic;
using System.Text;

namespace Ents.Storages
{
    public interface GenerationalArenaCell
    {

    }

    public struct Empty : GenerationalArenaCell
    {
        public int next;

        public Empty(int next)
        {
            this.next = next;
        }

        public override string ToString()
        {
            return $"Empty -> next: {next}";
        }
    }

    public struct Occupied<T> : GenerationalArenaCell
    {
        public int generation;
        public T data;

        public Occupied(int generation, T data)
        {
            this.generation = generation;
            this.data = data;
        }

        public override string ToString()
        {
            return $"Occupied -> generation: {generation}, data: {data}";
        }
    }

    public struct GenerationalIndex
    {
        public int index;
        public int generation;

        public GenerationalIndex(int index, int generation)
        {
            this.index = index;
            this.generation = generation;
        }
    }

    public class GenerationalArena<T>
    {
        private List<GenerationalArenaCell> _arena;
        private int _generation;
        private int _freeHead;
        private int _length;

        public GenerationalArena()
        {
            int defaultSize = 4;
            _arena = new List<GenerationalArenaCell>(defaultSize);
            _length = 0;
            _generation = 0;
            _freeHead = 0;

            for (int i = 0; i < defaultSize; i++)
            {
                _arena.Add(new Empty(i+1));
            }
        }

        // public void insert
        public GenerationalIndex Insert(T data)
        {
            Empty nextFree = (Empty)_arena[_freeHead];
            _arena[_freeHead] = new Occupied<T>(_generation, data);
            GenerationalIndex generationalIndex = new GenerationalIndex(_freeHead, _generation);
            _freeHead = nextFree.next;
            _length += 1;
            return generationalIndex;
        }

        // public void remove

        // public voi contains

        public int GetLength()
        {
            return _length;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Generational Arena: ");
            foreach (GenerationalArenaCell cell in _arena)
            {
                if (cell is Empty empty)
                {
                    stringBuilder.AppendLine(empty.ToString());
                }
                else if (cell is Occupied<T> occupied)
                {
                    stringBuilder.AppendLine(occupied.ToString());
                }
            }
            return stringBuilder.ToString();
        }
    }
}
