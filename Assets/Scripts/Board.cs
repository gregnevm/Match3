using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    public int Width;
    public int Height;
    public Spawner spawner;
    public SpawnPoint spawnPoint;
    public VerticalLine line;
    public PlaceHolder placeHolder;

    private VerticalLine[] lines;
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
        StartCoroutine(spawner.AnimatedSpawnBoard(spawnPoints));        
    }
   
    private void FullSpawnBoard()
    {
        spawnPoints = new SpawnPoint[Height];
        lines = new VerticalLine[Height];
        PlaceHolders = new PlaceHolder[Width, Height];        

        for (var y = 0; y < GetHeight(); y++)
        {            
            lines[y] = Instantiate(Instance.line, Instance.transform);
            lines[y].transform.DOLocalMove(Vector3.zero, 0, false);            
            lines[y].placeHolders = new PlaceHolder[Width];
            spawnPoints[y] = Instantiate(spawnPoint, spawner.transform);

            for (var x = 0; x < GetWidth(); x++)
            {
                lines[y].placeHolders[x] = Instantiate(Instance.placeHolder, lines[y].transform);
                PlaceHolder placeHolder = lines[y].placeHolders[x];                
                placeHolder.X = x;
                placeHolder.Y = y;
                PlaceHolders[x, y] = placeHolder;
            }
        }
    }
}
