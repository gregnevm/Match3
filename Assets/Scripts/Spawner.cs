using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{ 
    public float spawnDuration = 0.5f;      
    public static void SpawnNewBoard()
    {
        float spawnDuration = new Spawner().spawnDuration;
        Item[] gameObjects = Resources.LoadAll<Item>("Items/");
        Debug.Log(gameObjects.Length);
        for (var y = 0; y < Board.Instance.Height; y++)
        {
            for (var x = 0; x < Board.Instance.Width; x++)
            {                
                SpawnPoint spawnPoint = Board.Instance.spawnPoints[y];
                PlaceHolder workPlaceHolder = Board.Instance.GetPlaceHolder(x, y);
                Vector3 FinishPosition = Board.Instance.GetPlaceHolder(x, y).transform.position;
                Transform spawnPosition = spawnPoint.transform;

                if (workPlaceHolder.state == PlaceHolder.State.empty)
                {
                    workPlaceHolder.item = Item.SpawnNewGameObject(gameObjects, spawnPosition: spawnPosition);
                    workPlaceHolder.item.transform.DOMove(FinishPosition, spawnDuration, false);
                    workPlaceHolder.state = PlaceHolder.State.newborn;
                }
            }

        }

    }


}
