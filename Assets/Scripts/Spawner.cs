using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
   
    public float spawnDuration = 0.3f;
    public float animationSecondsBetwenNewItems = 0.1f;
    public IEnumerator AnimatedSpawnBoard(SpawnPoint[] spawnPoints)
    {
        Item[] gameObjects = Resources.LoadAll<Item>("Items/");

        for (var y = 0; y < Board.Instance.GetHeight(); y++)
        {
            for (var x = Board.Instance.GetWidth() - 1; x >= 0; x--)
            {
                PlaceHolder workPlaceHolder = Board.Instance.GetPlaceHolder(x, y);
                Transform parentTransform = workPlaceHolder.transform;

                if (workPlaceHolder.ThisState == PlaceHolder.State.empty)
                {
                    Item item = Item.SpawnNewGameObject(gameObjects, spawnPoints[y].transform.position);
                    item.transform.SetParent(parentTransform);
                    item.Parent = workPlaceHolder;
                    item.transform.DOLocalMove(Vector3.zero, spawnDuration, false);

                    workPlaceHolder.Item = item;
                    workPlaceHolder.ThisState = PlaceHolder.State.newborn;

                    yield return new WaitForSeconds(animationSecondsBetwenNewItems);
                }
            }
        }
    }
}