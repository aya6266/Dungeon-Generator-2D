using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGen
{
    private string[][] map;
    private int width;
    private int height;

    // Defining a dictionary to store the tile types
    private Dictionary<string,string> tileTypes = new Dictionary<string, string>();
    //Filling the dictionary with the tile types
    public void Main()
    {
        tileTypes.Add(".", "Floor");
        tileTypes.Add("NEC", "NorthEastCorner");
        tileTypes.Add("SEC", "SouthEastCorner");
        tileTypes.Add("SWC", "SouthWestCorner");
        tileTypes.Add("NWC", "NorthWestCorner");
        tileTypes.Add("NW", "NorthWall");
        tileTypes.Add("SW", "SouthWall");
        tileTypes.Add("EW", "EastWall");
        tileTypes.Add("WW", "WestWall");    
    }
    
    
    
    public DGen(int width, int height)
    {
        this.width = width;
        this.height = height;
        map = new string[width][];
        for (int i = 0; i < width; i++)
        {
            map[i] = new string[height];
        }
    }

    private string[][] Generate()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = GetTileType(i, j);
            }
        }
        return map;
    }

    private string GetTileType(int x, int y)
    {
        if(x == 0)
        {
            if(y == 0)
            {
                return "SWC";
            }
            else if(y == height - 1)
            {
                return "NWC";
            }
            else
            {
                return "WW";
            }
        }
        else if(x == width - 1)
        {
            if(y == 0)
            {
                return "SEC";
            }
            else if(y == height - 1)
            {
                return "NEC";
            }
            else
            {
                return "EW";
            }
        }
        else
        {
            if(y == 0)
            {
                return "SW";
            }
            else if(y == height - 1)
            {
                return "NW";
            }
            else
            {
                return ".";
            }
        } 
    }
    public string[][] GetMap()
    {
        return Generate();
    }

    

}
