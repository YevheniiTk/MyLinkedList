namespace MyLinkedList
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class MyLinkedList<T>
    {
        private MyLinkedList<T> list;

        public int Count { get; private set; }

        public MyLinkedListItem<T> First { get; private set; }

        public MyLinkedListItem<T> Last { get => this.First?.Previous; private set { } }
        
        public MyLinkedList()
        {
            this.list = this;
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

            this.list = this;
        }

        public void AddAfter(MyLinkedListItem<T> item, T value)
        {
            this.ValidateItem(item);
            var newItem = new MyLinkedListItem<T>(value);
            this.InsertNodeBefore(item.Next, newItem);
            newItem.List = this;
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

            newListElement.List = this;
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

            newListElement.List = this;
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
            
            newListElement.List = this;
        }

        public void Clear()
        {
            MyLinkedListItem<T> current = this.First;
            while (current != null)
            {
                MyLinkedListItem<T> temp = current;
                current = current.Next;
                temp.Invalidate();
            }

            this.First = null;
            this.Count = 0;
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

            if (item.List != this)
            {
                throw new InvalidOperationException($"Current list does not contain this item: {item}");
            }
        }

        private void InsertNodeBefore(
                                      MyLinkedListItem<T> item,
                                      MyLinkedListItem<T> newItem)
        {
            newItem.Next = item;
            newItem.Previous = item.Previous;
            item.Previous.Next = newItem;
            item.Previous = newItem;
            this.Count++;
        }
        
        private void InsertNodeToEmptyList(MyLinkedListItem<T> item)
        {
            Debug.Assert(
                        this.First == null && this.Count == 0, 
                        "LinkedList must be empty when this method is called!");
            this.First = item;
            this.First.Next = item;
            this.First.Previous = item;
            this.Count++;
        }
    }
}
