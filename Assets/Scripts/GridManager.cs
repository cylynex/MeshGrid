using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour {

    public Tile[] tiles;
    public GameObject tilePrefab;
    public GameObject gridManager;
    public List<int> tileSet = new List<int>();
    public List<int> tileSetFinal = new List<int>();

    public bool tile1Flipped = false;
    public bool tile2Flipped = false;

    public string tile1Name;
    public string tile2Name;

    public Text timerLabel;
    private float timer;
    public float timerStart;

    void Start() {
        gridManager = GameObject.FindGameObjectWithTag("GameController");
        timer = timerStart;
        timerLabel.text = timer.ToString();
        CreateGame(10,6);
    }


    void Update() {
        timer -= Time.deltaTime;
        timerLabel.text = timer.ToString("N0");
        if (timer <= 0) {
            SceneManager.LoadScene("Lose");
        }
    }

    void CreateGame(int xTiles, int yTiles) {

        // Get number of tiles and divide them evenly
        int numTiles = tiles.Length;
        int totalTiles = xTiles * yTiles;
        int numberEachTile = totalTiles / numTiles;
        int tileCreateSpot = 0;
        int tileElement = 0;
        int spotChecker = 1;

        while (tileCreateSpot < totalTiles) {
            //tileSet[tileCreateSpot] = tileElement;
            tileSet.Add(tileElement);

            if (spotChecker == numberEachTile) {
                // Maxed out this tile switch to next ID
                tileElement++;
                spotChecker = 0;
            }
            tileCreateSpot++;
            spotChecker++;
        }

        // Distribute the tile slots and create a new list to use for instantiation
        int count = tileSet.Count;
        int outSlot;
        int thisVar;

        while (count > 0) {
            outSlot = Random.Range(0, count);
            thisVar = tileSet[outSlot];
            tileSet.RemoveAt(outSlot);
            tileSetFinal.Add(thisVar);
            count = tileSet.Count;
        }

        int createSlot = 0;
        for (int x = 0; x < xTiles; x++) {
            for (int y = 0; y < yTiles; y++) {

                // Create the blank tile
                GameObject thisTile = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                thisTile.transform.SetParent(gridManager.transform);

                // Send the tile data over
                int finalOutputSlot = tileSetFinal[createSlot];
                thisTile.GetComponent<TileController>().tile = tiles[finalOutputSlot];
                createSlot++;
            }
        }
    }


    public void CheckForMatch() {

        // If match, destroy those tiles and give player score.
        if (tile1Name == tile2Name) {
            // They Match!  fuck.  destroy all tiles that are currently flipped over.
            foreach (Transform child in transform) {
                if (child.GetComponent<TileController>().isFlipped == true) {
                    child.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            StartCoroutine("KillTiles");
        }

        // reset tiles
        StartCoroutine("ResetTiles");

    }


    IEnumerator KillTiles() {
        yield return new WaitForSeconds(1);
        foreach (Transform child in transform) {
            if (child.GetComponent<TileController>().isFlipped == true) {
                Destroy(child.gameObject);
            }
        }
    }


    IEnumerator ResetTiles() {
        
        yield return new WaitForSeconds(1);

        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<TileController>().isFlipped == true) {
                child.gameObject.GetComponent<TileController>().isFlipped = false;
                child.gameObject.GetComponent<SpriteRenderer>().sprite = child.gameObject.GetComponent<TileController>().tile.tileBack;
            }
        }

        tile1Flipped = false;
    }


}
