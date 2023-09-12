namespace compression;

public class CompressionTests
{
    [Test]
    public void CanTakeAll()
    {
        Assert.AreEqual("abcdefg", Compression.Decompress("abcdefg", new int[]{7}, new int[]{}, new int []{}));
    }

    // [Test]
    // public void CanBackTrackBy1()
    // {
    //     Assert.AreEqual("abcdefggggg", Compression.Decompress("abcdefg", new int[]{7}, new int[]{1}, new int []{4}));
    // }
}

public class DequeueTests
{
    [Test]
    public void InitialSizeWorks()
    {
        Assert.AreEqual(0, new DoubleEndedArrayQueue<int>().Size());
    }

    [Test]
    public void FirstAddWorks()
    {
        var q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 7);
        Assert.AreEqual(1, q.Size());
    }

}

public class DequeueFirstAddWorks
{
    DoubleEndedArrayQueue<int> q = new DoubleEndedArrayQueue<int>();

    [SetUp]
    public void Setup() {
        q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 7);
    }

    [Test]
    public void CorrectSize()
    {
        Assert.AreEqual(1, q.Size());
    }

    [Test]
    public void SetWorks()
    {
        Assert.AreEqual(7, q.Set(0, 5));
        Assert.AreEqual(5, q.Get(0));
    }

    [Test]
    public void CorrectValuesFromGet()
    {
        Assert.AreEqual(7, q.Get(0));
    }

    [Test]
    public void Capacity()
    {
        Assert.AreEqual(1, q.Capacity());
    }

}

public class DequeueTwoAddsWork
{
    DoubleEndedArrayQueue<int> q = new DoubleEndedArrayQueue<int>();

    [SetUp]
    public void Setup() {
        q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 7);
        q.Add(0, 9);
    }

    [Test]
    public void CorrectSize()
    {
        Assert.AreEqual(2, q.Size());
    }

    [Test]
    public void CorrectValuesFromGetAtStart()
    {
        Assert.AreEqual(9, q.Get(0));
    }

    [Test]
    public void CorrectValuesFromGetAtEnd()
    {
        Assert.AreEqual(7, q.Get(1));
    }

    [Test]
    public void Capacity()
    {
        Assert.AreEqual(2, q.Capacity());
    }

}


public class DequeueThreeAddsWork
{
    DoubleEndedArrayQueue<int> q = new DoubleEndedArrayQueue<int>();

    [SetUp]
    public void Setup() {
        q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 9);
        q.Add(1, 7);
        q.Add(0, 6);
    }

    [Test]
    public void CorrectSize()
    {
        Assert.AreEqual(3, q.Size());
    }

    [Test]
    public void CorrectValuesFromGetAtStart()
    {
        Assert.AreEqual(6, q.Get(0));
    }

    [Test]
    public void CorrectValuesFromGetAtMiddle()
    {
        Assert.AreEqual(9, q.Get(1));
    }

    [Test]
    public void CorrectValuesFromGetAt2()
    {
        Assert.AreEqual(7, q.Get(2));
    }

    [Test]
    public void Capacity()
    {
        Assert.AreEqual(4, q.Capacity());
    }

}

public class DequeueFourAddsWork
{
    DoubleEndedArrayQueue<int> q = new DoubleEndedArrayQueue<int>();

    [SetUp]
    public void Setup() {
        q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 9);
        q.Add(1, 7);
        q.Add(0, 6);
        q.Add(3, 10);
    }

    [Test]
    public void CorrectSize()
    {
        Assert.AreEqual(4, q.Size());
    }

    [Test]
    public void CorrectValuesFromGetAtStart()
    {
        Assert.AreEqual(6, q.Get(0));
    }

    [Test]
    public void CorrectValuesFromGetAtMiddle()
    {
        Assert.AreEqual(9, q.Get(1));
    }

    [Test]
    public void CorrectValuesFromGetAt2()
    {
        Assert.AreEqual(7, q.Get(2));
    }

    [Test]
    public void CorrectValuesFromGetAt3()
    {
        Assert.AreEqual(10, q.Get(3));
    }

    [Test]
    public void Capacity()
    {
        Assert.AreEqual(4, q.Capacity());
    }

}


public class DoubleEndedArrayQueueRemoval
{
    DoubleEndedArrayQueue<int> q = new DoubleEndedArrayQueue<int>();

    [SetUp]
    public void Setup() {
        q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 9);
        q.Add(1, 7);
        q.Add(0, 6);
        q.Add(3, 10);
        q.Remove(0);
        q.Remove(0);
        q.Remove(0);
    }

    [Test]
    public void RemoveLastReturnsValue()
    {
        Assert.AreEqual(10, q.Remove(0));
    }

    [Test]
    public void CorrectSize()
    {
        Assert.AreEqual(1, q.Size());
    }

    [Test]
    public void CorrectValuesFromGetAtStart()
    {
        Assert.AreEqual(10, q.Get(0));
    }

    [Test]
    public void Capacity()
    {
        Assert.AreEqual(2, q.Capacity());
    }

}


public interface IList<T> {
    int Size();
    T Get(int index);
    T Set(int index, T value);
    void Add(int index, T value);
    T Remove(int index);
}

public class DoubleEndedArrayQueue<T> : IList<T>
{
    T[] values = new T[]{};
    int front = 0;
    int count = 0;

    public int Capacity() {
        return values.Length;
    }

    public DoubleEndedArrayQueue() {

    }

    private void Resize() {
        T[] newValues = new T[Math.Max(1, count * 2)];
        Array.Copy(values, newValues, count);
        values = newValues;
    }

    public void ShiftRight(int start, int end)
    {
        for (int dest = end; start < dest; dest--) {
            values[dest] = values[dest - 1];
        }
    }

    public void ShiftLeft(int start, int end)
    {
        for (int dest = start - 1; dest < end - 1; dest++) {
            values[dest] = values[dest + 1];
        }
    }

    // public void ShiftLeft(int index)
    // {
    //     for (int source = count - 1; index < source; dest--) {
    //         values[dest] = values[dest - 1];
    //     }
    // }


    public void Add(int index, T value)
    {
        if (count >= values.Length) Resize();
        ShiftRight(index, count);
        values[index] = value;
        count++;
    }

    public T Get(int index)
    {
        return values[index];
    }

    public T Remove(int index)
    {
        T removed = values[index];
        ShiftLeft(index + 1, count);
        count--;
        if (count * 3 < values.Length) Resize();
        return removed;
    }

    public T Set(int index, T value)
    {
        T replaced = values[index];
        values[index] = value;
        return replaced;
    }

    public int Size()
    {
        return count;
    }
}

public class Compression {

    public static IEnumerable<char> Decompress(IEnumerable<char> encoded, IEnumerable<int> take, IEnumerable<int> backtrack, IEnumerable<int> copy) {

        foreach (int numberToTake in take) {
        
        }
        return encoded;
        // if (false) {
        //     yield return 'a';
        // }
    }   
}   