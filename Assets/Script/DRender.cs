using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DRender : Singleton<DRender>
{
    public static DRender instance;
    

    public GameObject tile;
    public Sprite floor;
    public Sprite northEastCorner;
    public Sprite southEastCorner;
    public Sprite southWestCorner;
    public Sprite northWestCorner;
    
    public Sprite northWall;
    public Sprite southWall;
    public Sprite eastWall;
    public Sprite westWall;

    public Tilemap floorTileMap;

    public Tile floorTile;


    private Dictionary<string,Sprite> tileTypes = new Dictionary<string, Sprite>();

    private Dictionary<string,Tile> tileTypesForTileMap = new Dictionary<string, Tile>();
    //import all the tiles 
    
    void Awake() {
        instance = this;
        tileTypes.Add(".", floor);
        tileTypes.Add("NEC", northEastCorner);
        tileTypes.Add("SEC", southEastCorner);
        tileTypes.Add("SWC", southWestCorner);
        tileTypes.Add("NWC", northWestCorner);
        tileTypes.Add("NW", northWall);
        tileTypes.Add("SW", southWall);
        tileTypes.Add("EW", eastWall);
        tileTypes.Add("WW", westWall); 

        tileTypesForTileMap.Add(".", floorTile);
    }
    
    // void Start() {
    //     //instance = this;

         
    // }

    public void TileRenderer(string[][] map, float tileSizeX, float tileSizeY)
    {

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if(map[i][j] != null)
                {
                    // tile.GetComponent<SpriteRenderer>().sprite = tileTypes[map[i][j]];
                    // Instantiate(tile, new Vector3(i*tileSizeX, j*tileSizeY, 0), Quaternion.identity);

                    floorTileMap.SetTile(new Vector3Int(i,j,0), tileTypesForTileMap[map[i][j]]);
                }
            }
        }
    }

}
