using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Type", menuName = "Tiles/New Tile")]
public class Tile : ScriptableObject {

    public Tile tileData;

    public int tileID;
    public string tileName;
    public Sprite tileFront;
    public Sprite tileBack;
}
