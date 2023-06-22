using UnityEngine.Events;
public static class Context 
{    
    public enum BoardState 
    {
        Creating, Ready
    }
    public static BoardState CurrentBoardState;
    public static UnityEvent<BoardItem> OnItemDestroyedRequest = new();

    public static UnityEvent<( int width, int height)> OnRequestToGetPlaceholder = new();
    public static UnityEvent<PlaceHolder> OnSendNeededPlaceholder = new();

}
