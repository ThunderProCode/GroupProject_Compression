namespace compression;

public interface IList<T> {
    int Size();
    T Get(int index);
    T Set(int index, T value);
    void Add(int index, T value);
    T Remove(int index);
}
