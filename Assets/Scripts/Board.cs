using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;
using System.Threading.Tasks;

public class Board : MonoBehaviour
{
    public VerticalLine[] lines;
    public SpawnPoint[] spawnPoints;    

    public int Width => _placeHolders.GetLength(0);
    public int Height => _placeHolders.GetLength(1); 
    

    private PlaceHolder[,] _placeHolders { get; set; }
    public PlaceHolder GetPlaceHolder(int x, int y) { return _placeHolders[x, y]; }

    public static Board Instance { get; private set; }
    private void Awake() => Instance = this;

    // Start is called before the first frame update
    void Start()
    {
        _placeHolders = new PlaceHolder[lines.Max(lines => lines.placeHolders.Length), lines.Length];
        SpawnPlaceholders();
        Spawner.SpawnNewBoard();        
    }
   
    
    void SpawnPlaceholders()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                spawnPoints[y].y = y;
                var placeHolder = lines[y].placeHolders[x];

                placeHolder.x = x;
                placeHolder.y = y;
                placeHolder.X = x;
                placeHolder.Y = y;

                _placeHolders[x, y] = placeHolder;
            }

        }


    }    
    
}
