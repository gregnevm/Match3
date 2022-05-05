using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
public class Item : MonoBehaviour
{
    private string _tag ;
    public int value;   
    public static Item SpawnNewGameObject( Item[] gameObjects,Transform spawnPosition)
    {
        int rand = Random.Range(0, gameObjects.Length - 1);
        Item newbornObject = Instantiate(gameObjects[rand], spawnPosition);
        
        return newbornObject;

    }

  
}