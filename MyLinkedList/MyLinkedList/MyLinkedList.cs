namespace MyLinkedList
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class MyLinkedList<T>
    {
        public int Count { get; private set; }

        public MyLinkedListItem<T> First { get; private set; }

        public MyLinkedListItem<T> Last { get => this.First?.Previous; }

        public MyLinkedList()
        {
        }

        public MyLinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (T item in collection)
            {
                this.AddLast(item);
            }
        }

        public void AddAfter(MyLinkedListItem<T> item, T value)
        {
            this.ValidateItem(item);
            var newItem = new MyLinkedListItem<T>(value);
            this.InsertNodeBefore(item.Next, newItem);
        }

        public void AddAfter(T item, T value)
        {
            MyLinkedListItem<T> current = this.Find(item);
            this.ValidateItem(current);
            var newItem = new MyLinkedListItem<T>(value);
            this.InsertNodeBefore(current.Next, newItem);
        }

        public void AddBefore(MyLinkedListItem<T> item, T value)
        {
            this.ValidateItem(item);
            var newListElement = new MyLinkedListItem<T>(value);
            this.InsertNodeBefore(item, newListElement);
            if (item == this.First)
            {
                this.First = newListElement;
            }
        }

        public void AddBefore(T item, T value)
        {
            MyLinkedListItem<T> current = this.Find(item);
            this.ValidateItem(current);
            var newListElement = new MyLinkedListItem<T>(value);
            this.InsertNodeBefore(current, newListElement);
            if (current == this.First)
            {
                this.First = newListElement;
            }
        }

        public void AddFirst(T value)
        {
            var newListElement = new MyLinkedListItem<T>(value);

            if (this.First == null)
            {
                this.InsertNodeToEmptyList(newListElement);
            }
            else
            {
                this.InsertNodeBefore(this.First, newListElement);
                this.First = newListElement;
            }
        }

        public void AddLast(T value)
        {
            var newListElement = new MyLinkedListItem<T>(value);

            if (this.First == null)
            {
                this.InsertNodeToEmptyList(newListElement);
            }
            else
            {
                this.InsertNodeBefore(this.First, newListElement);
            }
        }

        public void Clear()
        {
            MyLinkedListItem<T> current = this.First;
            while (current != null)
            {
                MyLinkedListItem<T> temp = current;
                current = current.Next;
                temp.ResetLinks();
            }

            this.First = null;
            this.Count = 0;
        }

        public bool Contains(T value)
        {
            return this.Find(value) != null;
        }

        public MyLinkedListItem<T> Find(T value)
        {
            MyLinkedListItem<T> item = this.First;
            EqualityComparer<T> c = EqualityComparer<T>.Default;

            if (item != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(item.Data, value))
                        {
                            return item;
                        }

                        item = item.Next;
                    }
                    while (item != this.First);
                }
                else
                {
                    do
                    {
                        if (item.Data == null)
                        {
                            return item;
                        }

                        item = item.Next;
                    }
                    while (item != this.First);
                }
            }

            return null;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator
        {
            private MyLinkedList<T> list;
            private MyLinkedListItem<T> node;
            private int index;

            public T Current { get; private set; }

            internal Enumerator(MyLinkedList<T> list)
            {
                this.list = list;
                this.node = list.First;
                this.Current = default(T);
                this.index = 0;
            }

            public bool MoveNext()
            {
                if (this.node == null)
                {
                    this.index = this.list.Count + 1;
                    return false;
                }

                this.index++;
                this.Current = this.node.Data;
                this.node = this.node.Next;
                if (this.node == this.list.First)
                {
                    this.node = null;
                }

                return true;
            }
        }

        public void Remove(MyLinkedListItem<T> itemToRemove)
        {
            this.ValidateItem(itemToRemove);
            var curent = this.First;

            do
            {
                if (object.ReferenceEquals(curent, itemToRemove))
                {
                    if (curent == this.First)
                    {
                        this.First = this.First.Next;
                    }
                    else
                    {
                        curent.Previous.Next = curent.Next;
                        curent.Next.Previous = curent.Previous;
                    }

                    this.Count--;
                    curent = null;
                    break;
                }

                curent = curent.Next;
            }
            while (this.First != curent);
        }

        private void ValidateItem(MyLinkedListItem<T> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
        }

        private void InsertNodeBefore(
                                      MyLinkedListItem<T> item,
                                      MyLinkedListItem<T> newItem)
        {
            MyLinkedListItem<T>.InsertNodeBefore(item, newItem);
            this.Count++;
        }

        private void InsertNodeToEmptyList(MyLinkedListItem<T> item)
        {
            Contract.Assert(
                        this.First == null && this.Count == 0,
                        "LinkedList must be empty when this method is called!");

            this.First = item;
            this.First.Next = item;
            this.First.Previous = item;
            this.Count++;
        }
    }
}
