using UnityEngine;
public class PlaceHolder : MonoBehaviour
{
    public State ThisState;
    
    private int _x;   
    private int _y;

    public int X
    { 
        get { return _x; } 
        set { if (value >= 0) { _x = value; } }
    }
    public int Y 
    { 
        get { return _y; } 
        set { if (value >= 0) { _y = value; } }
    }
    
    public BoardItem Item { get ; private set; }

    public void SetNewItem(BoardItem item)
    {
        item.transform.SetParent(this.gameObject.transform);
        item.Init(this);
        this.Item = item;
        this.ThisState = PlaceHolder.State.newborn;
    }
    public enum State 
    {                
        empty,
        newborn,
        isChecked,
        hasOneSameNeghbourn,
        alreadyInGroupToDestroy,
    }

    
}
