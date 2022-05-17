using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    public int Width;
    public int Height;
    public Spawner spawner;
    public SpawnPoint spawnPoint;
    public GameObject line;
    public PlaceHolder placeHolder;    
    public int GetWidth() { return Width;}
    public int GetHeight() { return Height;}

    private SpawnPoint[] spawnPoints;

    private PlaceHolder[,] PlaceHolders { get;  set; }
    public PlaceHolder GetPlaceHolder(int x, int y) { return PlaceHolders[x, y]; }

    public static Board Instance { get; private set; }
    private void Awake() => Instance = this;

    void Start()
    {
        FullSpawnBoard();
        StartCoroutine( spawner.AnimatedSpawnBoard(spawnPoints) );
    }
    
    public void DestroyAndDrop(Item item )
    {
        int _x = item.Parent.X;
        int y = item.Parent.Y;
            
        item.Parent.ThisState = PlaceHolder.State.empty;
        
        if (_x > 0)
        {
            Item newItem;
            Transform newParentTransform ;
            for (int x = _x; x > 0; x--)
            {
                newParentTransform = PlaceHolders[x,y].transform;
                             
                newItem = PlaceHolders[x - 1,y].Item;
                PlaceHolders[x,y].Item = newItem;
                newItem.transform.SetParent(newParentTransform);
                newItem.Parent = PlaceHolders[x,y];
                newItem.transform.DOLocalMove(Vector3.zero, 0.3f, false);
                PlaceHolders[x,y].ThisState = PlaceHolder.State.newborn;
                PlaceHolders[x - 1,y].ThisState = PlaceHolder.State.empty;                
            }           
        }
        Destroy(item.gameObject);
        StartCoroutine(spawner.AnimatedSpawnBoard(spawnPoints));        
    }
   
    private void FullSpawnBoard()
    {
        spawnPoints = new SpawnPoint[Height];
        PlaceHolders = new PlaceHolder[Width, Height];
        
        GameObject[] lines = new GameObject[Height];


        for (var y = 0; y < GetHeight(); y++)
        {
            lines[y] = Instantiate(line, Instance.transform);
            spawnPoints[y] = Instantiate(spawnPoint, spawner.transform);
            for (var x = 0; x < GetWidth(); x++)
            {
               PlaceHolders[x,y] = Instantiate(Instance.placeHolder, lines[y].transform);
               PlaceHolders[x, y].X = x;
               PlaceHolders[x, y].Y = y;
                
            }
        }
    }
}
