namespace MyLinkedList
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new[] { 1, 3, 5, 7 };
            var list = new MyLinkedList<int>(collection);

            foreach (var i in list)
            {
                Console.WriteLine($"foreach {i}");
            }

            Console.WriteLine($"===================");
            list.AddAfter(1, 2);
            list.AddAfter(3, 4);
            list.AddAfter(5, 6);

            Console.WriteLine(list.Contains(5));
            Console.WriteLine(list.Contains(25));

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

            list.Clear();
        }
    }
}
