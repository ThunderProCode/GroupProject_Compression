namespace compression;

public class Compression {

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
}   