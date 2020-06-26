class Sort
{
    static void Swap(ref IRenderable x, ref IRenderable y)
    {
        var t = x;
        x = y;
        y = t;
    }

    //метод возвращающий индекс опорного элемента
    static int Partition(IRenderable[] array, int minIndex, int maxIndex)
    {
        var pivot = minIndex - 1;
        for (var i = minIndex; i < maxIndex; i++)
        {
            if (array[i].Image.zIndex < array[maxIndex].Image.zIndex || ((GameObject)array[i]).position._2 < ((GameObject)array[maxIndex]).position._2)
            {
                ++pivot;
                Swap(ref array[pivot], ref array[i]);
            }
        }

        pivot++;
        Swap(ref array[pivot], ref array[maxIndex]);
        return pivot;
    }

    //быстрая сортировка
    static IRenderable[] QuickSort(IRenderable[] array, int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex)
        {
            return array;
        }

        var pivotIndex = Partition(array, minIndex, maxIndex);
        QuickSort(array, minIndex, pivotIndex - 1);
        QuickSort(array, pivotIndex + 1, maxIndex);

        return array;
    }

    public static IRenderable[] QuickSort(IRenderable[] array)
    {
        return QuickSort(array, 0, array.Length - 1);
    }
}