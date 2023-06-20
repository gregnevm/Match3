using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] Spawner _spawner;
    [SerializeField] SpawnPoint _spawnPoint;
    [SerializeField] GameObject _line;
    [SerializeField] PlaceHolder _placeHolder;

    private SpawnPoint[] _spawnPoints;
    private PlaceHolder[,] PlaceHolders { get; set; }

    void Start()
    {
        InitializeBoard();
        Context.OnItemDestroyedRequest.AddListener(DestroyAndDrop);
    }
    
    public void DestroyAndDrop(Item item )
    {
        int _x = item.Parent.X;
        int y = item.Parent.Y;
            
        item.Parent.ThisState = PlaceHolder.State.empty;
        
        if (_x > 0)
        {
            Item newItem;
            
            for (int x = _x; x > 0; x--)
            {                
                newItem = PlaceHolders[x - 1,y].Item;
                PlaceHolders[x,y].SetNewItem(newItem);
                newItem.SetNewPlaceholder (PlaceHolders[x,y]);
                
                newItem.transform.DOLocalMove(Vector3.zero, 0.3f, false);
                PlaceHolders[x,y].ThisState = PlaceHolder.State.newborn;
                PlaceHolders[x - 1,y].ThisState = PlaceHolder.State.empty;                
            }           
        }
        Destroy(item.gameObject);
        StartCoroutine(_spawner.AnimatedSpawnBoard(_spawnPoints,_height,_width));        
    }  
    
    private void SpawnBoardElements()
    {
        Context.CurrentBoardState = Context.BoardState.creating;
        _spawnPoints = new SpawnPoint[_height];
        PlaceHolders = new PlaceHolder[_width, _height];
        
        GameObject[] lines = new GameObject[_height];


        for (var y = 0; y < _height; y++)
        {
            lines[y] = Instantiate(_line, this.transform);
            _spawnPoints[y] = Instantiate(_spawnPoint, _spawner.transform);
            for (var x = 0; x <_width; x++)
            {
               PlaceHolders[x,y] = Instantiate(_placeHolder, lines[y].transform);
               PlaceHolders[x, y].X = x;
               PlaceHolders[x, y].Y = y;
                
            }           
        }
        Context.OnRequestToGetPlaceholder.AddListener(GetPlaceHolder);
    }

    void GetPlaceHolder((int width, int height) coordinates)
    {
        Context.OnSendNeededPlaceholder.Invoke( PlaceHolders[coordinates.width, coordinates.height]);
    }
    [ContextMenu("Init new board")]
    void InitializeBoard()
    {
        SpawnBoardElements();
        StartCoroutine(_spawner.AnimatedSpawnBoard(_spawnPoints, _height, _width));
    }
}
