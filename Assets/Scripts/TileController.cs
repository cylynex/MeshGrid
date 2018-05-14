using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public Tile tile;
    public GridManager gridManager;
    public bool isFlipped = false;
    public Sprite backSprite;
    public string tileName;

    void Start() {
        GetComponent<SpriteRenderer>().sprite = tile.tileBack;
        gridManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridManager>();
        tileName = tile.tileName;
    }

    void OnMouseDown() {
        isFlipped = true;
        GetComponent<SpriteRenderer>().sprite = tile.tileFront;

        // Flip the tile and check for match if its the second tile.
        if (gridManager.tile1Flipped == false) {
            gridManager.tile1Flipped = true;
            gridManager.tile1Name = tileName;
        } else {
            // Tile 1 is already flipped, flip 2 and check for a match.
            gridManager.tile2Name = tileName;
            gridManager.CheckForMatch();
        }

    }

}
