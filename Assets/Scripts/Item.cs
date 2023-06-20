using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{   
    private PlaceHolder _parent;
    public PlaceHolder Parent { get { return _parent; } private set { _parent = value; }}

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public static Item SpawnNewGameObject( List<Item> gameObjects, Vector3 spawnPosition)
    {
        int rand = Random.Range(0, gameObjects.Count - 1);
        Item newbornObject = Instantiate(gameObjects[rand], spawnPosition, Quaternion.identity);
        return newbornObject;
    }

    public void SetNewPlaceholder(PlaceHolder placeHolder)
    {
        transform.SetParent(placeHolder.transform);
        _parent = placeHolder;
        
      //ResizeSprite(); //not working correctly

    }
    public void OnMouseDown()
    {
        if (Context.CurrentBoardState == Context.BoardState.ready)
        {       
            Context.OnItemDestroyedRequest.Invoke(this);
        }
    }
    private void ResizeSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("No SpriteRenderer component found on the GameObject.");
            return;
        }

        RectTransform childRectTransform = GetComponent<RectTransform>();
        if (childRectTransform == null)
        {
            Debug.LogWarning("Child object does not have a RectTransform component.");
            return;
        }

        // Get the size of the child RectTransform
        Vector2 childSize = childRectTransform.rect.size;

        // Calculate the desired scale based on the child's size and the sprite's original size
        Vector3 scale = transform.localScale;
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        scale.x = childSize.x / spriteSize.x;
        scale.y = childSize.y / spriteSize.y;

        // Apply the new scale to the sprite
        transform.localScale = scale;
    }
}