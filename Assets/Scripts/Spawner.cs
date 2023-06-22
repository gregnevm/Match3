using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{   
    [SerializeField] float _spawnDuration = 0.3f;
    [SerializeField] float _animationSecondsBetwenNewItems = 0.1f;
    [SerializeField] List<BoardItem> _whiteList = new(); 

    private PlaceHolder _placeHolderTemporaryCached;

    public IEnumerator AnimatedSpawnBoard(SpawnPoint[] spawnPoints, int height, int width)
    {
        Context.CurrentBoardState = Context.BoardState.Creating;
        List<BoardItem> items = new();
        if (_whiteList.Count < 2)
        {
            var loadResources = Resources.LoadAll<BoardItem>("Items/");
            foreach (var item in loadResources)
            {
                items.Add(item);
            }
            _whiteList = items;
        }        


        Context.OnSendNeededPlaceholder.AddListener(SetCurrentPlaceholder);

        for (var y = 0; y < height; y++)
        {
            for (var x = width - 1; x >= 0; x--)
            {
                Context.OnRequestToGetPlaceholder.Invoke((x, y));
                PlaceHolder workPlaceHolder =  _placeHolderTemporaryCached;                

                if (workPlaceHolder.ThisState == PlaceHolder.State.empty)
                {
                   BoardItem item = SpawnNewItem(spawnPoints[y].transform.position, workPlaceHolder);
                    item.transform.DOLocalMove(Vector3.zero, _spawnDuration, false);
                    yield return new WaitForSeconds(_animationSecondsBetwenNewItems);
                }
            }
        }
        Context.CurrentBoardState = Context.BoardState.Ready;
    }
    public BoardItem SpawnNewItem(Vector3 spawnPosition, PlaceHolder parentPlaceHolder) 
    {
        int rand = Random.Range(0, _whiteList.Count - 1);
        BoardItem item = Instantiate(_whiteList[rand], spawnPosition, Quaternion.identity);
        parentPlaceHolder.SetNewItem(item);
        return item;
    }
    private void SetCurrentPlaceholder(PlaceHolder placeHolder)
    {
        _placeHolderTemporaryCached = placeHolder;
    }
}