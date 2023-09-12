using System.Collections;

namespace compression;

public class DoubleEndedArrayQueue<T> : IList<T>, IEnumerable<T>
{
    T[] values = new T[]{};
    int front = 0;
    int count = 0;

    public int Capacity() {
        return values.Length;
    }

    private void Resize() {
        T[] newValues = new T[Math.Max(1, count * 2)];
        for (int i = 0; i < count; i++) {
            newValues[i] = values[ShiftedAndWrappedIndex(i)];
        }
        front = 0;
        values = newValues;
    }

    public void ShiftRight(int start, int end)
    {
        for (int dest = end; start < dest; dest--) {
            values[ShiftedAndWrappedIndex(dest)] = values[ShiftedAndWrappedIndex(dest - 1)];
        }
    }

    public void ShiftLeft(int start, int end)
    {
        for (int dest = start - 1; dest < end - 1; dest++) {
            values[ShiftedAndWrappedIndex(dest)] = values[ShiftedAndWrappedIndex(dest + 1)];
        }
    }

    public void Add(int index, T value)
    {
        if (count >= values.Length) Resize();
        if (NearBack(index))
        {
            AddRight(index, value);
        }
        else
        {
            AddLeft(index, value);
        }
        values[ShiftedAndWrappedIndex(index)] = value;
        count++;
    }

    private void AddRight(int index, T value)
    {
        ShiftRight(index, count);
    }

    private void AddLeft(int index, T value)
    {
        ShiftLeft(-1, index);
        front = (front - 1) % values.Length;
    }

    private int PositiveMod(int x, int m) {
        return (x + m) % m;
    }

    private int ShiftedAndWrappedIndex(int index) {
        return PositiveMod(front + index,  values.Length);
    }

    public T Get(int index)
    {
        return values[ShiftedAndWrappedIndex(index)];
    }

    public T Remove(int index)
    {
        T removed = values[ShiftedAndWrappedIndex(index)];
        if (NearBack(index))
        {
            RemoveRight(index);
        }
        else
        {
            RemoveLeft(index);
        }
        count--;
        if (count * 3 < values.Length) Resize();
        return removed;
    }

    private bool NearBack(int index)
    {
        return count - 1 - index <= index;
    }

    private void RemoveRight(int index)
    {
        ShiftLeft(index + 1, count);
    }

    private void RemoveLeft(int index)
    {
        ShiftRight(0, index);
        front = (front + 1) % values.Length;
    }

    public T Set(int index, T value)
    {
        T replaced = values[ShiftedAndWrappedIndex(index)];
        values[ShiftedAndWrappedIndex(index)] = value;
        return replaced;
    }

    public int Size()
    {
        return count;
    }

    public T GetContents(int rawIndex) {
        return values[rawIndex];
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Size(); i++) {
            yield return Get(i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T value)
    {
        Add(Size(), value);
    }
}
