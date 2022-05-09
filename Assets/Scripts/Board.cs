using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
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
    Spawner spawn = new Spawner();

    // Start is called before the first frame update
    void Start()
    {
        _placeHolders = new PlaceHolder[lines.Max(lines => lines.placeHolders.Length), lines.Length];
        SpawnPlaceholders();
        StartCoroutine( spawn .AnimatedSpawnBoard());
        
        
    }
    private void Update()
    {
        
        
    }

    void SpawnPlaceholders()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var placeHolder = lines[y].placeHolders[x];

                placeHolder.x = x;
                placeHolder.y = y;
                spawnPoints[y].y = y;

                _placeHolders[x, y] = placeHolder;
            }
        }
    }
    public void DestroyAndDrop(Item item )
    {
        int _x = item.Parent.x;
        int y = item.Parent.y;

        PlaceHolder[] verticalLineArray = lines[y].placeHolders;              
        item.Parent.state = PlaceHolder.State.empty;
        
        if (_x > 0)
        {
            Item newItem;
            Transform newParentTransform ;
            for (int x = _x; x > 0; x--)
            {
                newParentTransform = verticalLineArray[x].transform;
                             
                newItem = verticalLineArray[x - 1].item;
                verticalLineArray[x].item = newItem;
                newItem.transform.SetParent(newParentTransform);
                newItem.Parent = verticalLineArray[x];
                newItem.transform.DOLocalMove(Vector3.zero, 0.3f, false);                
                verticalLineArray[x].state = PlaceHolder.State.newborn;               
                verticalLineArray[x - 1].state = PlaceHolder.State.empty;                
            }
            Destroy(item.gameObject);
        }        
        StartCoroutine(spawn.AnimatedSpawnBoard());        
    }

}
