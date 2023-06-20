using UnityEngine.Events;
public static class Context 
{
    public enum BoardState //add more states for new features
    {
        creating, ready
    }
    public static BoardState CurrentBoardState { get; set; } //public set for static property give more controll for game procces 
    public static UnityEvent<Item> OnItemDestroyedRequest = new();

    public static UnityEvent<( int width, int height)> OnRequestToGetPlaceholder = new();
    public static UnityEvent<PlaceHolder> OnSendNeededPlaceholder = new();

}
