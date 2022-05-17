using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    
    private int _x;
    public int X { get { return _x; } set { _x = value; } }
   
    private int _y;
    public int Y { get { return _y; } set { _y = value; } }    

    private Item _item;
    public Item Item { get{return _item;} set{_item = value;} }

    public enum State
    {                
        empty,
        newborn,
        isChecked,
        hasOneSameNeghbourn,
        alreadyInGroupToDestroy,
    }
    private State _state = 0;
    public State ThisState { get{return _state;} set{_state = value;} }
}
