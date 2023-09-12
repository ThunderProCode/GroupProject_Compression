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
    public void CorrectValuesFromGet()
    {
        Assert.AreEqual(7, q.Get(0));
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

    public DoubleEndedArrayQueue() {

    }

    private void Resize() {
        T[] newValues = new T[Math.Max(1, count * 2)];
        Array.Copy(values, newValues, count);
        values = newValues;
    }


    public void Add(int index, T value)
    {
        if (count >= values.Length) Resize();
        values[index] = value;
        count++;
    }

    public T Get(int index)
    {
        return values[index];
    }

    public T Remove(int index)
    {
        throw new NotImplementedException();
    }

    public T Set(int index, T value)
    {
        throw new NotImplementedException();
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