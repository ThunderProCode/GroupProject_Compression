namespace compression;

public class CompressedStringTest {
    [Test]
    public void CompressWorks() {
        
        Assert.AreEqual("abcdae", DecompressedString.Compress("abcbcbcbcbcbcbdaaaaaaeaaaeaaaeaa"));
    }

    [Test]
    public void CompressWorks2() {
        
        Assert.AreEqual("abcdefg", DecompressedString.Compress("abcdefggggg"));
    }


    [Test]
    public void CompressWorks3() {
        
        Assert.AreEqual("qwertte", DecompressedString.Compress("qwerttetetetetetetetetet"));
    }


    [Test]
    public void TestCharacterCount() {
        Assert.AreEqual(4,DecompressedString.DetermineCharactersCount("abbbccccb",'c'));
    }
}

public class CompressionTests
{
    [Test]
    public void CanTakeAll()
    {
        Assert.AreEqual("abcdefg", CompressedString.Decompress("abcdefg", new int[]{7}, new int[]{0}, new int []{0}));
    }

    [Test]
    public void CanBackTrackBy1()
    {
        Assert.AreEqual("abcdefggggg", CompressedString.Decompress("abcdefg", new int[]{7}, new int[]{1}, new int []{4}));
    }

    [Test]
    public void CanBackTrackBy5()
    {
        Assert.AreEqual("abcdefgcdefgcdefgcde", CompressedString.Decompress("abcdefg", new int[]{7}, new int[]{5}, new int []{13}));
    }

    [Test]
    public void Constructor()
    {
        var message = "abcdefgcdefgcdefgcde";
        var compressed = new CompressedString(message);
        Assert.AreEqual(message, compressed);
        Console.WriteLine($"Original Length: {message.Length} Encoded length: {compressed.EncodedLength()}");
    }

    [Test]
    public void CanUseStreamsLongerThan1Bigger() {
        Assert.AreEqual("abcbcbcbcbcbcbdaaaaaaeaaaeaaaeaa",CompressedString.Decompress("abcdae",new int[]{3,2,1}, new int[]{2,1,4},new int[]{11,5,10}));
    }

}

public class DequeTests
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

public class DequeFirstAddWorks
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

public class DequeTwoAddsWork
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


public class DequeThreeAddsWork
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


public class DequeUsesCorrectSpace
{

    DoubleEndedArrayQueue<int> q = new DoubleEndedArrayQueue<int>();

    [SetUp]
    public void Setup() {
        q = new DoubleEndedArrayQueue<int>();
        q.Add(0, 9);
        q.Add(1, 7);
        q.Add(2, 6);
        q.Add(3, 10);
    }

    [Test]
    public void RemoveLast()
    {
        q.Remove(3);
        Assert.AreEqual(new int[] {9, 7, 6}, new int[] {q.GetContents(0), q.GetContents(1), q.GetContents(2)});
    }

    [Test]
    public void RemoveFirst()
    {
        q.Remove(0);
        Assert.AreEqual(new int[] {7, 6, 10}, new int[] {q.GetContents(1), q.GetContents(2), q.GetContents(3)});
    }

    [Test]
    public void RemoveSecondToLast()
    {
        q.Remove(2);
        Assert.AreEqual(new int[] {9, 7, 10}, new int[] {q.GetContents(0), q.GetContents(1), q.GetContents(2)});
    }

    [Test]
    public void RemoveSecond()
    {
        q.Remove(1);
        Assert.AreEqual(new int[] {9, 6, 10}, new int[] {q.GetContents(1), q.GetContents(2), q.GetContents(3)});
    }

    [Test]
    public void RemoveSecondAndThenFirstShouldBothShiftRight()
    {
        q.Remove(1);
        q.Remove(0);
        Assert.AreEqual(new int[] {9, 9}, new int[] {q.GetContents(0), q.GetContents(1)});
        Assert.AreEqual(new int[] {6, 10}, new int[] {q.GetContents(2), q.GetContents(3)});
    }

    [Test]
    public void RemoveSecondAndThenSecondShouldShiftLeft ()
    {
        q.Remove(1);
        q.Remove(1);
        Assert.AreEqual(new int[] {9, 10}, new int[] {q.GetContents(0), q.GetContents(3)});
        Assert.AreEqual(new int[] {9, 10}, new int[] {q.GetContents(1), q.GetContents(2)});
    }


    [Test]
    public void RemoveTwoFromFrontAndThenAddBack()
    {
        q.Remove(0);
        q.Remove(0);
        q.Add(2, 15);
        Assert.AreEqual(new int[] {15, 6, 10}, new int[] {q.GetContents(0), q.GetContents(2), q.GetContents(3)});
        Assert.AreEqual(new int[] {6, 10, 15}, new int[] {q.Get(0), q.Get(1), q.Get(2)});
    }

    [Test]
    public void RemoveTwoFromFrontAndThenAddFront()
    {
        q.Remove(0);
        q.Remove(0);
        q.Add(0, 15);
        Assert.AreEqual(new int[] {15, 6, 10}, new int[] {q.GetContents(1), q.GetContents(2), q.GetContents(3)});
        Assert.AreEqual(new int[] {15, 6, 10}, new int[] {q.Get(0), q.Get(1), q.Get(2)});
    }

    [Test]
    public void RemoveTwoFromFrontAndThenAddThreeBackSoThatItGrows()
    {
        q.Remove(0);
        q.Remove(0);
        q.Add(2, 15);
        q.Add(3, 5);
        q.Add(4, 25);
        Assert.AreEqual(new int[] {6, 10, 15, 5, 25}, new int[] {q.GetContents(0), q.GetContents(1), q.GetContents(2), q.GetContents(3), q.GetContents(4)});
        Assert.AreEqual(5, q.Size());
        Assert.AreEqual(new int[] {6, 10, 15, 5, 25}, new int[] {q.Get(0), q.Get(1), q.Get(2), q.Get(3), q.Get(4)});
    }

    [Test]
    public void AddNearFront()
    {
        Assert.AreEqual(4, q.Size());
        Assert.AreEqual(4, q.Capacity());
        q.Add(1, 15);
        Assert.AreEqual(5, q.Size());
        Assert.AreEqual(8, q.Capacity());
        Assert.AreEqual(new int[] {9, 15, 7, 6, 10}, new int[] {q.Get(0), q.Get(1), q.Get(2), q.Get(3), q.Get(4)});
        Assert.AreEqual(new int[] {15, 7, 6, 10, 9}, new int[] {q.GetContents(0), q.GetContents(1), q.GetContents(2), q.GetContents(3), q.GetContents(7)});
    }

}


public class DequeFourAddsWork
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
