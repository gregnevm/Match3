using UnityEngine;

public class BoardItem : MonoBehaviour
{   
    public PlaceHolder Parent { get; private set; }

    public void Init(PlaceHolder placeHolder)
    {        
        Parent = placeHolder;
       
      // ResizeSprite(); // not implemented 
    }
    public void OnMouseDown()
    {
        if (Context.CurrentBoardState == Context.BoardState.Ready)
        {       
            Context.OnItemDestroyedRequest.Invoke(this);
        }
    }
    private void ResizeSprite()
    {
        //TODO: implement logic to resize BoardItem`s sprite to correct board expand
    }
}