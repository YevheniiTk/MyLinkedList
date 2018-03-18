namespace MyLinkedList
{
    public class MyLinkedListItem<T>
    {
        public MyLinkedList<T> List;
        public MyLinkedListItem<T> Next;
        public MyLinkedListItem<T> Previous;

        public T Data { get; private set; }

        public MyLinkedListItem(T data)
        {
            this.Data = data;
        }

        public void Invalidate()
        {
            this.Next = null;
            this.Previous = null;
        }
    }
}
