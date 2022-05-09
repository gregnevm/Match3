
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class Board : MonoBehaviour
{
    public VerticalLine[] lines;
    public SpawnPoint[] spawnPoints;

    public int GetWidth()
    {
        return PlaceHolders.GetLength(0);
    }

    public int GetHeight()
    {
        return PlaceHolders.GetLength(1);
    }

    private PlaceHolder[,] PlaceHolders { get; set; }
    public PlaceHolder GetPlaceHolder(int x, int y) { return PlaceHolders[x, y]; }

    public static Board Instance { get; private set; }
    private void Awake() => Instance = this;
    private readonly Spawner spawn = new Spawner();

    // Start is called before the first frame update
    void Start()
    {
        PlaceHolders = new PlaceHolder[lines.Max(lines => lines.placeHolders.Length), lines.Length];
        SpawnPlaceholders();
        StartCoroutine( spawn.AnimatedSpawnBoard());
    }
    
    void SpawnPlaceholders()
    {
        for (var y = 0; y < GetHeight(); y++)
        {
            for (var x = 0; x < GetWidth(); x++)
            {
                var placeHolder = lines[y].placeHolders[x];

                placeHolder.X = x;
                placeHolder.Y = y;
                spawnPoints[y].Y = y;

                PlaceHolders[x, y] = placeHolder;
            }
        }
    }
    public void DestroyAndDrop(Item item )
    {
        int _x = item.Parent.X;
        int y = item.Parent.Y;

        PlaceHolder[] verticalLineArray = lines[y].placeHolders;              
        item.Parent.ThisState = PlaceHolder.State.empty;
        
        if (_x > 0)
        {
            Item newItem;
            Transform newParentTransform ;
            for (int x = _x; x > 0; x--)
            {
                newParentTransform = verticalLineArray[x].transform;
                             
                newItem = verticalLineArray[x - 1].Item;
                verticalLineArray[x].Item = newItem;
                newItem.transform.SetParent(newParentTransform);
                newItem.Parent = verticalLineArray[x];
                newItem.transform.DOLocalMove(Vector3.zero, 0.3f, false);                
                verticalLineArray[x].ThisState = PlaceHolder.State.newborn;               
                verticalLineArray[x - 1].ThisState = PlaceHolder.State.empty;                
            }           
        }
        Destroy(item.gameObject);
        StartCoroutine(spawn.AnimatedSpawnBoard());        
    }
}
