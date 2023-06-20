using UnityEngine;
public class PlaceHolder : MonoBehaviour
{
    private State _state = 0;
    public State ThisState { get{return _state;} set{_state = value;} }
    
    private int _x;   
    private int _y;

    public int X { get { return _x; } set { if (value >= 0) { _x = value; } } }
    public int Y { get { return _y; } set { if (value >= 0) { _y = value; } } }    

    private Item _item;
    public Item Item { get{return _item;} private set{_item = value;} }

    public enum State // There is prototype to better check board for a searching groups to destroy. 
    {                
        empty,
        newborn,
        isChecked,
        hasOneSameNeghbourn,
        alreadyInGroupToDestroy,
    }
    public void SetNewItem(Item item)
    {
        _item = item;
    }
}
