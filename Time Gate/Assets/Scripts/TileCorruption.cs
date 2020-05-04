using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class TileCorruption : MonoBehaviour
{
    private Tilemap map;
    [Range(0,1)]
    private float gameCorruption = 0;//for testing
    private float levelCorruption = 0;
    private float timer;
    [Range(0,1000)]
    public float corruptionUpdateTime = 45;

    public Vector2Int xRange, yRange;

    public List<Tile> tiles;
    // Start is called before the first frame update
    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();
        //initialize the game corruption and the level corruption
        gameCorruption = PlayerPrefs.GetFloat("gamecorruption");
        levelCorruption = gameCorruption;

        //call the corruption function more often depending on the duration of the 
        InvokeRepeating("Corrupt", 5, 0.5f / (gameCorruption+levelCorruption));
        timer = corruptionUpdateTime;
        PlayerPrefs.SetFloat("levelcorruption", levelCorruption);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            timer = corruptionUpdateTime;
            CancelInvoke();
            //get the updated levelcorruptiondata
            levelCorruption += levelCorruption * 0.5f;

            //repeat the invoke with the updated levelcorruption data
            InvokeRepeating("Corrupt", 1, .5f /(gameCorruption + levelCorruption));
            PlayerPrefs.SetFloat("levelcorruption", levelCorruption);
        }
        
    }

    void Corrupt()
    {
        //roll for chance based on the corruptionValue
        float chance = Random.Range(0.0f, 1.0f);
        if (chance < gameCorruption) {
            //select a random tile from the palette
            Tile tile = tiles[Random.Range(0, tiles.Count)];

            //select a random spot on the grid
            int tileX = Random.Range(xRange.x, xRange.y); 
            int tileY = Random.Range(yRange.x, yRange.y);


            //draw the tile on that random spot
            map.SetTile(new Vector3Int(tileX, tileY, 0), tile);
        }
    }
}
