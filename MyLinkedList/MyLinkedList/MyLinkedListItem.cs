namespace MyLinkedList
{
    public class MyLinkedListItem<T> 
    {
        public MyLinkedListItem<T> Next;
        public MyLinkedListItem<T> Previous;

        public T Data { get; private set; }

        internal MyLinkedListItem(T data)
        {
            this.Data = data;
        }

        internal void ResetLinks()
        {
            this.Next = null;
            this.Previous = null;
        }

        internal static void InsertNodeBefore(
                                      MyLinkedListItem<T> item,
                                      MyLinkedListItem<T> newItem)
        {
            newItem.Next = item;
            newItem.Previous = item.Previous;
            item.Previous.Next = newItem;
            item.Previous = newItem;
        }
    }
}
