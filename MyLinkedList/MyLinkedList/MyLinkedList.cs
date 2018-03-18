namespace MyLinkedList
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Collections;

    public class MyLinkedList<T>
    {
        private MyLinkedList<T> list;

        public int Count { get; private set; }
        public MyLinkedListItem<T> First { get; private set; }
        public MyLinkedListItem<T> Last { get => this.First?.Previous; private set { } }
        
        public MyLinkedList() {
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
                AddLast(item);
            }
            list = this;
        }

        public void AddAfter(MyLinkedListItem<T> item, T value)
        {
            ValidateItem(item);
            var newItem = new MyLinkedListItem<T>(value);
            InsertNodeBefore(item.Next, newItem);
            newItem.List = this;
        }

        public void AddBefore(MyLinkedListItem<T> item, T value)
        {
            ValidateItem(item);
            var newListElement = new MyLinkedListItem<T>(value);
            InsertNodeBefore(item, newListElement);
            if (item == First)
            {
                First = newListElement;
            }

            newListElement.List = this;
        }

        public void AddFirst(T value)
        {
            var newListElement = new MyLinkedListItem<T>(value);

            if (First == null)
            {
                InsertNodeToEmptyList(newListElement);
            }
            else
            {
                InsertNodeBefore(First, newListElement);
                First = newListElement;
            }

            newListElement.List = this;
        }

        public void AddLast(T value)
        {
            var newListElement = new MyLinkedListItem<T>(value);

            if (First == null)
            {
                InsertNodeToEmptyList(newListElement);
            }
            else
            {
                InsertNodeBefore(First, newListElement);
            }
            
            newListElement.List = this;
        }

        public void Clear()
        {
            MyLinkedListItem<T> current = First;
            while (current != null)
            {
                MyLinkedListItem<T> temp = current;
                current = current.Next;
                temp.Invalidate();
            }

            First = null;
            Count = 0;
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
                node = list.First;
                Current = default(T);
                index = 0;
            }
            
            public bool MoveNext()
            {
                if (node == null)
                {
                    index = list.Count + 1;
                    return false;
                }

                index++;
                Current = node.Data;
                node = node.Next;
                if (node == list.First)
                {
                    node = null;
                }
                return true;
            }
        }

        public void Remove(MyLinkedListItem<T> itemToRemove)
        {
            ValidateItem(itemToRemove);
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
                    Count--;
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

        private void InsertNodeBefore(MyLinkedListItem<T> item,
                                      MyLinkedListItem<T> newItem)
        {
            newItem.Next = item;
            newItem.Previous = item.Previous;
            item.Previous.Next = newItem;
            item.Previous = newItem;
            Count++;
        }
        
        private void InsertNodeToEmptyList(MyLinkedListItem<T> item)
        {
            Debug.Assert(First == null && Count == 0, 
                        "LinkedList must be empty when this method is called!");
            First = item;
            First.Next = item;
            First.Previous = item;
            Count++;
        }
    }
}
