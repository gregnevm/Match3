using UnityEngine;
public class SpawnPoint : MonoBehaviour
{
    private int _y;
    public int Y
    {
        set
        {
            if (value >= 0)
            {
                _y = value;
            }
        }        
        get { return _y; }
    }    
}
