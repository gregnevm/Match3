using UnityEngine;

public class Item : MonoBehaviour
{   
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