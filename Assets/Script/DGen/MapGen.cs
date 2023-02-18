
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGen : MonoBehaviour 
{
    public int mapHeight;
    public int mapWidth;
    public int randomSeed;

    public int maxHightDungeon;

    public int minHightDungeon;

    public int maxWidthDungeon;
    public int minWidthDungeon;

    public int miniNumRooms;

    public int maxNumRooms;

    private int ActualNumRooms;

    public List<Vector2Int> midPointsSpawnedRooms;
    public MapGen( int _mW, int _mH, int _seed, 
    int _maxHD, int _minHD, int _maxWD, 
    int _minWD,int _maxNR, int _minNR)
    {
        mapHeight = _mH;
        mapWidth = _mW;
        maxHightDungeon = _maxHD;
        minHightDungeon = _minHD;
        maxWidthDungeon = _maxWD;
        minWidthDungeon = _minWD;
        miniNumRooms = _minNR;
        maxNumRooms = _maxNR;
        Random.InitState(_seed);
    }

    
    
    private string[][] GenerateMap()
    {
        string[][] map = new string[mapWidth][];
        for (int i = 0; i < mapWidth; i++)
        {
            map[i] = new string[mapHeight];
        }

        HashSet<Vector2Int> dungeonPlaced = new HashSet<Vector2Int>();
        HashSet<int[]> roomLocationDim = new HashSet<int[]>();

        int numRooms = Random.Range(miniNumRooms, maxNumRooms);
        SetNumRooms(numRooms);
        
        while(numRooms > 0)
        {
            int dungeonHeight = Random.Range(minHightDungeon, maxHightDungeon);
            int dungeonWidth = Random.Range(minWidthDungeon, maxWidthDungeon);

            int dungeonX = Random.Range(0, mapWidth - dungeonWidth);
            int dungeonY = Random.Range(0, mapHeight - dungeonHeight);

            Vector2Int dungeonPos = new Vector2Int(dungeonX, dungeonY);
            
            
            
            if(   !dungeonPlaced.Contains(dungeonPos) && CanCreateRoom(dungeonPos, dungeonWidth, dungeonHeight, roomLocationDim))
            {
                //prevents rooms from spawning inside already spawned rooms
                dungeonPlaced.Add(dungeonPos);
                //A set to prevent the spawning of rooms that will overlap with prevous spawned rooms
                roomLocationDim.Add(new int[]{dungeonX, dungeonY, dungeonWidth, dungeonHeight});
                

                for (int i = dungeonX; i < dungeonX + dungeonWidth; i++)
                {
                    for (int j = dungeonY; j < dungeonY + dungeonHeight; j++)
                    {
                        map[i][j] = ".";
                        
                        dungeonPlaced.Add(new Vector2Int(i, j));
                        
                    }
                }
                numRooms--;
            }
        }
        midPointsSpawnedRooms = MidPointRoomsSpawned(roomLocationDim);
        return map;
    }

    private void SetNumRooms(int _numRooms)
    {
        ActualNumRooms = _numRooms;
    }

    public int GetNumRooms()
    {
        return ActualNumRooms;
    }   

    private bool CanCreateRoom(Vector2Int currSpawnRoom, int width, int height, HashSet<int[]> roomLocationDim)
    {
        foreach(int[] room in roomLocationDim)
        {
            int roomX = room[0];
            int roomY = room[1];
            int roomWidth = room[2];
            int roomHeight = room[3];


            if(currSpawnRoom.x < roomX + roomWidth 
            && currSpawnRoom.x + width > roomX 
            && currSpawnRoom.y < roomY + roomHeight 
            && currSpawnRoom.y + height > roomY)
            {
                return false;
            }
        }

        return true;
    }

    public string[][] GetMap()
    {
        return GenerateMap();
    }

    public int Test(int min, int max){
        return Random.Range(min, max);
    }

    public List<Vector2Int> MidPointRoomsSpawned(HashSet<int[]> roomLocationDim)
    {
        List<Vector2Int> midPointRooms = new List<Vector2Int>();
        foreach(int[] room in roomLocationDim)
        {
            int roomX = room[0];
            int roomY = room[1];
            int roomWidth = room[2];
            int roomHeight = room[3];

            int midPointX = roomX + (roomWidth / 2);
            int midPointY = roomY + (roomHeight / 2);

            midPointRooms.Add(new Vector2Int(midPointX, midPointY));
        }

        return midPointRooms;
    }

}