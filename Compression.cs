using System.Collections;

namespace compression;

public class CompressedString : IEnumerable<char> {
    private DoubleEndedArrayQueue<char> encoded = new DoubleEndedArrayQueue<char>();
    private DoubleEndedArrayQueue<int> countsToTakeFromOriginal = new DoubleEndedArrayQueue<int>();
    private DoubleEndedArrayQueue<int> amountsToBacktrackInEncoded = new DoubleEndedArrayQueue<int>();
    private DoubleEndedArrayQueue<int> countsToCopyFromEncoded = new DoubleEndedArrayQueue<int>();

    public CompressedString(IEnumerable<char> original) {
        foreach (char c in original) {
            encoded.Add(c);
        }
        countsToTakeFromOriginal.Add(encoded.Size());
        amountsToBacktrackInEncoded.Add(0);
        countsToCopyFromEncoded.Add(0);
    }


    public static IEnumerable<char> Decompress(IEnumerable<char> encoded, IEnumerable<int> take, IEnumerable<int> backtrack, IEnumerable<int> copy) {
        DoubleEndedArrayQueue<char> result = new DoubleEndedArrayQueue<char>();

        foreach (int numberToTake in take) {
            foreach (char c in encoded.Take(numberToTake)) {
                result.Add(c);
            }
            int startIndex = result.Size() - backtrack.Take(1).First();
            int copyCount = copy.Take(1).First();
            for (int i = 0; i < copyCount; i++) {
                result.Add(result.Get(startIndex + i));
            }
        }
        return result;
    }

    public IEnumerator<char> GetEnumerator()
    {
        return Decompress(encoded, countsToTakeFromOriginal, amountsToBacktrackInEncoded, countsToCopyFromEncoded).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int EncodedLength() {
        return encoded.Size() + countsToCopyFromEncoded.Size();
    }
}   