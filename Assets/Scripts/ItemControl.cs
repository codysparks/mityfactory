using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemControl : MonoBehaviour
{

    public PlayerControl Player;
    public SpriteRenderer parentSpriteRenderer;
    public BoxCollider2D parentCollider;
    public int progress;
    public int totalProgress;

    // Start is called before the first frame update
    void Start()
    {
        parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        parentCollider = GetComponentInParent<BoxCollider2D>();
        Player = FindObjectOfType<PlayerControl>();
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player is on item
        if (other.CompareTag("Player")) {
            Player.SetInteraction(this);
        }
        
        // If the item is on an object
        if (other.CompareTag("Object")) {
            // If it's a tilemap renderer with a sorting layer player, higher sorting priority
            TilemapRenderer tilemapRenderer = other.GetComponent<TilemapRenderer>();

            if(parentSpriteRenderer != null && tilemapRenderer.sortingLayerName == "Player" && tilemapRenderer != null) {
                parentSpriteRenderer.sortingOrder = 15;
            }

            ItemProgress(other);            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.SetInteraction(null);
        }
    }

    public string GetName()
    {
        return this.name;
    }

    public void ShowItem(Vector3 position)
    {
        parentSpriteRenderer.sortingOrder = 0;
        parentSpriteRenderer.transform.position = position;
        parentSpriteRenderer.enabled = true;
        parentCollider.enabled = true;
    }

    public void HideItem()
    {
        parentSpriteRenderer.enabled = false;
        parentCollider.enabled = false;
    }

    public void ItemProgress(Collider2D other) {
        // Check if the object contains the word step
        if(other.name.ToLower().Contains("step")) {
            string numberString = other.name.ToLower().Replace("step", "").Trim();

            // Get number from name of object
            if (int.TryParse(numberString, out int number)) {
                
                // Must go through order or steps
                if(progress == number - 1) {
                    progress = number;
                }
                else {
                    Debug.Log("WRONG ORDER");
                }
            }

            // Completed all steps
            if(progress == totalProgress) {
                Debug.Log("DONE");
            }
        }
    }
}
