using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
public class Item : MonoBehaviour
{
   
    public int value;
    private PlaceHolder _parent;
    public PlaceHolder Parent { get { return _parent; } set { _parent = value; }}
    
    public static Item SpawnNewGameObject( Item[] gameObjects, Vector3 spawnPosition)
    {
        int rand = Random.Range(0, gameObjects.Length - 1);
        Item newbornObject = Instantiate(gameObjects[rand], spawnPosition, Quaternion.identity);
        return newbornObject;
    }
    public void OnMouseDown()
    {              
            Board.Instance.DestroyAndDrop(this);     
    }









}