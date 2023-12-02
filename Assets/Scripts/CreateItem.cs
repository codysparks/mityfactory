using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateItem : MonoBehaviour
{
    public GameObject prefabItem;
    public Tilemap tilemap;
    public Tile emptyTile;
    public Tile readyTile;
    public Vector3Int tileLocation;

    public bool isRepeating = true;
    public float timerDuration = 5f;
    private float timer;

    // Start is called before the first frame update
    void Start() {
        tilemap = GetComponentInParent<Tilemap>();
        tileLocation = getTilemapLocation();
        StartCoroutine(TimerCoroutine(timerDuration));
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator TimerCoroutine(float duration) {
        // Infinite loop for repetition
        while (isRepeating) { 
            yield return new WaitForSeconds(duration);
            ItemInstance();
            isRepeating = false;
        }
    }

    public void ItemInstance() {
        // Replace the tile
        tilemap.SetTile(tileLocation, readyTile);

        // Create item
        Vector3 startPosition = GameObject.Find("Item Create Position").transform.position;
        Instantiate(prefabItem, new Vector3(startPosition.x, startPosition.y, startPosition.z), Quaternion.identity);
    }

    public void OnCollisionExit(Collision other) {
        Debug.Log(other);
    }

    public void Restart() {
        // Replace the tile
        tilemap.SetTile(tileLocation, emptyTile);
        isRepeating = true;
    }

    public Vector3Int getTilemapLocation() {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        int xPosition = 0;
        int yPosition = 0;

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
                    xPosition = x + bounds.x;
                    yPosition = y + bounds.y;
                }
            }
        }

        return new Vector3Int(xPosition, yPosition, 0);
    }
}
