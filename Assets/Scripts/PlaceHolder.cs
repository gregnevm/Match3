using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    private int _x;
    public int x
    {
        get { return _x; }
        set { _x = value; }
    }
    private int _y;
    public int y
    {
        get { return _y; }
        set { _y = value; }
    }
    public int X;
    public int Y;
    private Item _item;
    public Item item { get { return _item; } set { _item = value; } }




    public enum State
    {                
        empty,
        newborn,
        isChecked,
        hasOneSameNeghbourn,
        alreadyInGroupToDestroy,

    }
   
    private State _state = 0;
    public State state {  get { return _state; }  set { _state = value; } }
    

}
