using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
public class GameManger : Singleton<GameManger>
{
    public static GameManger instance;
    private string[][] map;
    public int mapSeed = 24;
    
    public int mapWidth = 150;
    public int mapHeight = 100;
    public int mapSize = 100;
    public int minHeightSize = 10;
    public int maxHeightSize = 30;
    public int minWidthSize = 10;
    public int maxWidthSize = 30;
    public int minNumRooms = 5;
    public int maxNumRooms = 7;


    

   

    void Start()
    {
        //Test room generator Class
        //TestRoomGenerator(10, 10, 1, 1);

        //Test Seeded Random Map Genrator Class  
        //TestSeedRandomMapGenrator(150, 100, mapSeed, 30, 10, 30, 10, 7, 5, 1, 1);      
        
        //Testing Deluanay Triangulation Class
        // TestDeluanayTriangulation(mapWidth, mapHeight, mapSeed, 
        // maxHeightSize, minHeightSize, maxWidthSize, minWidthSize, 
        // maxNumRooms, minNumRooms, 1, 1);

        // TestDeluanayTriangulation(150, 100, mapSeed, 30, 10, 30, 10, 7, 5, 1, 1);
        
        
        TestPrimsMST(mapWidth, mapHeight, mapSeed, 
        maxHeightSize, minHeightSize, maxWidthSize, minWidthSize, 
         maxNumRooms, minNumRooms, 1, 1);

        // TestMinHeap();   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // private void TestMinHeap()
    // {
    //     MinHeap heap = new MinHeap(10);
    //     heap.Add(new float[2]{10, 0});
    //     heap.Add(new float[2]{5, 1});
    //     heap.Add(new float[2]{17, 2});
    //     heap.Add(new float[2]{3, 3});
    //     heap.Add(new float[2]{7, 4});
    //     heap.Add(new float[2]{12, 5});
    //     heap.Add(new float[2]{19, 6});
    //     heap.Add(new float[2]{1, 7});
    //     heap.Add(new float[2]{2, 8});

        
        
    //     Debug.Log($"Heap Size: {heap.GetSize()}");

    //     float[][] heapItems = heap.GetItems();
    //     for(int i = 0; i < heap.GetSize(); i++)
    //     {
    //         Debug.Log($"Heap Index: {i} Value: {heapItems[i][0]} Edge: {heapItems[i][1]}");
    //     }
        
    // }


    private void TestPrimsMST(int w, int h, 
    int seed, int minRoomSize, int maxRoomSize, int minRoomCount, 
    int maxRoomCount, int minCorridorLength, int maxCorridorLength, float tileWidth, float tileHeight)
    {
        MapGen mapGen = TestSeedRandomMapGenrator(w, h, seed, minRoomSize, maxRoomSize, minRoomCount, 
        maxRoomCount, minCorridorLength, maxCorridorLength, tileWidth, tileHeight);  
        
        BWDeluanayTri bwDeluanayTri = new BWDeluanayTri(mapGen.midPointsSpawnedRooms);  

        List<Triangle> listTrianglation = bwDeluanayTri.Triangulation();

        // foreach(Triangle tri in listTrianglation)
        // {
            
        //     Debug.DrawLine(new Vector3(tri.v1.x*tileWidth, tri.v1.y*tileHeight, 0), 
        //     new Vector3(tri.v2.x*tileWidth, tri.v2.y*tileHeight, 0), Color.red, 1000);
        //     Debug.DrawLine(new Vector3(tri.v2.x*tileWidth, tri.v2.y*tileWidth, 0),
        //      new Vector3(tri.v3.x*tileWidth, tri.v3.y*tileHeight, 0), Color.red, 1000);
        //     Debug.DrawLine(new Vector3(tri.v3.x*tileWidth, tri.v3.y*tileHeight, 0),
        //      new Vector3(tri.v1.x*tileWidth, tri.v1.y*tileWidth, 0), Color.red, 1000);
        // }

        PrimsMST primsMST = new PrimsMST(mapSeed);
        List<Edge> listEdges = primsMST.MST(listTrianglation, mapGen.GetNumRooms());
        foreach(Edge edge in listEdges)
        {
            Debug.DrawLine(new Vector3(edge.v1.x*tileWidth, edge.v1.y*tileHeight, 0), 
            new Vector3(edge.v2.x*tileWidth, edge.v2.y*tileHeight, 0), Color.green, 1000);
            edge.DisplayWeight();
        }
        Debug.Log($"Number Edges: {listEdges.Count}");



    }

    
    private void TestDeluanayTriangulation(int w, int h, 
    int seed, int minRoomSize, int maxRoomSize, int minRoomCount, 
    int maxRoomCount, int minCorridorLength, int maxCorridorLength, float tileWidth, float tileHeight)
    {
        MapGen mapGen = TestSeedRandomMapGenrator(w, h, seed, minRoomSize, maxRoomSize, minRoomCount, 
        maxRoomCount, minCorridorLength, maxCorridorLength, tileWidth, tileHeight);  
        
        BWDeluanayTri bwDeluanayTri = new BWDeluanayTri(mapGen.midPointsSpawnedRooms);  

        List<Triangle> listTrianglation = bwDeluanayTri.Triangulation();

        Debug.Log("Triangulation Count: " + listTrianglation.Count);
        foreach(Triangle tri in listTrianglation)
        {
            tri.DebugListVisulaizer();
            Debug.DrawLine(new Vector3(tri.v1.x*tileWidth, tri.v1.y*tileHeight, 0), 
            new Vector3(tri.v2.x*tileWidth, tri.v2.y*tileHeight, 0), Color.red, 1000);
            Debug.DrawLine(new Vector3(tri.v2.x*tileWidth, tri.v2.y*tileWidth, 0),
             new Vector3(tri.v3.x*tileWidth, tri.v3.y*tileHeight, 0), Color.red, 1000);
            Debug.DrawLine(new Vector3(tri.v3.x*tileWidth, tri.v3.y*tileHeight, 0),
             new Vector3(tri.v1.x*tileWidth, tri.v1.y*tileWidth, 0), Color.red, 1000);
        }

    }

    private MapGen TestSeedRandomMapGenrator(int w, int h, 
    int seed, int minRoomSize, int maxRoomSize, int minRoomCount, 
    int maxRoomCount, int minCorridorLength, int maxCorridorLength, float tileWidth, float tileHeight)
    {
        MapGen mapGen = new MapGen(w, h, seed, minRoomSize, maxRoomSize, minRoomCount, maxRoomCount, minCorridorLength, maxCorridorLength);
        map = mapGen.GetMap();
        DRender.instance.TileRenderer(map, tileWidth, tileHeight);
        return mapGen;
    }
    

    private void TestRoomGenerator(int w, int h, float tileWidth, float tileHeight)
    {
        DGen dGen = new DGen(w, h);
        DRender.instance.TileRenderer(dGen.GetMap(), tileWidth, tileHeight);

    }

    

    public Vector2 HelperConvertVertexToVec2Int(Vertex vertex)
    {
        return new Vector2(vertex.x, vertex.y);
    }


}
