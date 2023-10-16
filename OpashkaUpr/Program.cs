using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;
    private T[] elements;
    private int startIndex = 0;
    private int endIndex = 0;

    public int Count { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            this.Grow();
        }

        this.elements[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty");
        }

        var result = this.elements[this.startIndex];
        this.startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;

        return result;
    }

    public T[] ToArray()
    {
        var resultArr = new T[this.Count];
        int sourceIndex = this.startIndex;
        int destinationIndex = 0;

        for (int i = 0; i < this.Count; i++)
        {
            resultArr[destinationIndex] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.elements.Length;
            destinationIndex++;
        }

        return resultArr;
    }

    private void Grow()
    {
        int newCapacity = elements.Length * 2;
        T[] newElements = new T[newCapacity];
        for (int i = 0; i < Count; i++)
        {
            newElements[i] = elements[(startIndex + i) % elements.Length];
        }
        elements = newElements;
        startIndex = 0;
        endIndex = Count;
    }

    private void CopyAllElementsTo(T[] resultArr)
    {
        int sourceIndex = this.startIndex;
        int destinationIndex = 0;

        for (int i = 0; i < this.Count; i++)
        {
            resultArr[destinationIndex] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.elements.Length;
            destinationIndex++;
        }
    }
    static void Main()
    {
        CircularQueue<int> queue = new CircularQueue<int>(3);
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        Console.WriteLine("Изваден от опашката елемент: " + queue.Dequeue());
        Console.WriteLine("Изваден от опашката елемент: " + queue.Dequeue());
        queue.Enqueue(5);
        queue.Enqueue(6);
        int[] array = queue.ToArray();
        Console.WriteLine("Елементите на опашката като масив: ");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
    }
}
