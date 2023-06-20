using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{   
    [SerializeField] float _spawnDuration = 0.3f;
    [SerializeField] float _animationSecondsBetwenNewItems = 0.1f;
    [SerializeField] List<Item> _whiteList = new(); // use it 

    private PlaceHolder _placeHolderTemporaryCached;

    public IEnumerator AnimatedSpawnBoard(SpawnPoint[] spawnPoints, int height, int width)
    {
        Context.CurrentBoardState = Context.BoardState.creating;
        List<Item> items = new();
        if (_whiteList.Count < 2)
        {
            var loadResources = Resources.LoadAll<Item>("Items/");
            foreach (var item in loadResources)
            {
                items.Add(item);
            }
        }
        else
        {
            items = _whiteList;
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
                    Item item = Item.SpawnNewGameObject(items, spawnPoints[y].transform.position);                   
                    item.SetNewPlaceholder( workPlaceHolder);
                    item.transform.DOLocalMove(Vector3.zero, _spawnDuration, false);

                    workPlaceHolder.SetNewItem(item);
                    workPlaceHolder.ThisState = PlaceHolder.State.newborn;

                    yield return new WaitForSeconds(_animationSecondsBetwenNewItems);
                }
            }
        }
        Context.CurrentBoardState = Context.BoardState.ready;
    }

    void SetCurrentPlaceholder(PlaceHolder placeHolder)
    {
        _placeHolderTemporaryCached = placeHolder;
    }
}