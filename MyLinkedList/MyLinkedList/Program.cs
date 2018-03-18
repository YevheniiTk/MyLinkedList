namespace MyLinkedList
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new[] { 1, 2, 3, 4, 5, 6 };
            var list = new MyLinkedList<int>(collection);

            var item = list.First.Next.Next;
            list.Remove(item);

            foreach (var i in list)
            {
                Console.WriteLine($"foreach {i}");
            }

            list.AddAfter(list.First, 10);
            list.AddBefore(list.First, 10);
            list.AddBefore(list.Last, 99);
            list.AddFirst(0);
            list.AddLast(9);

            foreach (var i in list)
            {
                Console.WriteLine($"foreach {i}");
            }

            var iterator = list.First;
            do
            {
                Console.WriteLine(iterator.Data.ToString());
                iterator = iterator.Next;
            }
            while (list.First != iterator);
        }
    }
}
