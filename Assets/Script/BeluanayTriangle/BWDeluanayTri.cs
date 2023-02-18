using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using UnityEngine;

public class BWDeluanayTri
{
    
    public List<Vector2Int> pointList;

    private List<Triangle> triangleSet = new List<Triangle>();
    public BWDeluanayTri(List<Vector2Int> _midPoints)
    {
        pointList = _midPoints;
    }

    public List<Triangle> Triangulation()
    {
        return DeluanayTriangulation(pointList);
        
    }

    private Triangle MakeSuperTriangle(List<Vector2Int> pointList)
    {
        float minX = pointList[0].x;
        float minY = pointList[0].y;
        float maxX = pointList[0].x;
        float maxY = pointList[0].y;
        for (int i = 1; i < pointList.Count; i++)
        {
            minX = Mathf.Min(minX, pointList[i].x);
            minY = Mathf.Min(minY, pointList[i].y);
            maxX = Mathf.Max(maxX, pointList[i].x);
            maxY = Mathf.Max(maxY, pointList[i].y);
        }

        float dx = (maxX - minX)*10;
        float dy = (maxY - minY)*10;

        Vertex A = new Vertex(minX - dx, minY - dy*3);
        Vertex B = new Vertex(minX - dx, maxY + dy);
        Vertex C = new Vertex(maxX + dx*3, minY - dy);

        return new Triangle(A, B, C);
    }

    private List<Triangle> DeluanayTriangulation(List<Vector2Int> pointList)
    {
        List<Triangle> triangleList = new List<Triangle>();
        List<Triangle> result = new List<Triangle>();
        Triangle superTriangle = MakeSuperTriangle(pointList);
        triangleList.Add(superTriangle);

        foreach(Vector2Int point in pointList)
        {
            List<Triangle> badTriangleList = new List<Triangle>();
            foreach(Triangle triangle in triangleList)
            {
                if(triangle.VertexInCircum(new Vertex(point.x, point.y)))
                {
                    badTriangleList.Add(triangle);
                }
            }

            List<Edge> polygon = new List<Edge>();
            foreach(Triangle triangle in badTriangleList)
            {
                foreach(Edge edge in triangle.GetEdges())
                {
                    if(!polygon.Contains(edge))
                    {
                        polygon.Add(edge);
                    }
                    else
                    {
                        polygon.Remove(edge);
                    }
                }
            }

            foreach(Edge edge in polygon)
            {
                // if(TriangleNotInList(triangleList, edge, point)) triangleList.Add(new Triangle(edge.v1, edge.v2, new Vertex(point.x, point.y)));
                triangleList.Add(new Triangle(edge.v1, edge.v2, new Vertex(point.x, point.y)));




            }

            foreach(Triangle triangle in badTriangleList)
            {
                triangleList.Remove(triangle);
            }

            //remove the final part of the super triangle
            int lengthOfTriangleList = triangleList.Count;
            
            foreach(Triangle triangle in triangleList)
            {
                
                if(!triangle.HasVertex(superTriangle.GetVertices()))
                {
                    //need to remove this
                    result.Add(triangle);
                }
            }
            
        } 
        return TriangleListToSet(result);
    }

    //A function that turns a list of triangles into a set of triangles
    private List<Triangle> TriangleListToSet(List<Triangle> triangleList)
    {
        Dictionary<string, Triangle> triangleSet = new Dictionary<string, Triangle>();
        List<Triangle> result = new List<Triangle>();
        foreach(Triangle triangle in triangleList)
        {
            if(!triangleSet.ContainsKey(triangle.id))
            {
                triangleSet.Add(triangle.id, triangle);
            }
            
        }
        foreach(KeyValuePair<string, Triangle> triangle in triangleSet)
        {
            result.Add(triangle.Value);
        }
        return result;

    }
    
}


public class Vertex
{
    public float x;
    public float y;
    public string id;
    public Vertex(float _x, float _y)
    {
        x = _x;
        y = _y;
        id = $"{x},{y}";
    }
}

public class Edge
{
    public Vertex v1;
    public Vertex v2;
    public string id;
    public float weight;

    public Edge(Vertex _v1, Vertex _v2)
    {
        v1 = _v1;
        v2 = _v2;
        id = $"{v1.x},{v1.y} {v2.x},{v2.y}";
        weight = Mathf.Sqrt(Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2));
    }

    public void DisplayWeight()
    {
        Debug.Log($"Edge starting at ({v1.x},{v1.y}) has a weight of {weight} to get too ({v2.x},{v2.y})");
    }
}

public class Triangle
{
    public Vertex v1;
    public Vertex v2;
    public Vertex v3;
    public string id;
    public Triangle(Vertex _v1, Vertex _v2, Vertex _v3)
    {
        v1 = _v1;
        v2 = _v2;
        v3 = _v3;
        id = IDSortTriangleInXAxis();
    }

    public List<Edge> GetEdges()
    {
        List<Edge> edgeList = new List<Edge>();
        edgeList.Add(new Edge(v1, v2));
        edgeList.Add(new Edge(v2, v3));
        edgeList.Add(new Edge(v3, v1));
        return edgeList;
    }

    private float CircumRadius()
    {
        float a = Mathf.Sqrt(Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.y - v1.y, 2));
        float b = Mathf.Sqrt(Mathf.Pow(v3.x - v2.x, 2) + Mathf.Pow(v3.y - v2.y, 2));
        float c = Mathf.Sqrt(Mathf.Pow(v1.x - v3.x, 2) + Mathf.Pow(v1.y - v3.y, 2));
        float s = (a + b + c) / 2;
        return (a * b * c) / (4 * Mathf.Sqrt(s * (s - a) * (s - b) * (s - c)));
    }

    private float CircumCenterX()
    {
        float a = Mathf.Sqrt(Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.y - v1.y, 2));
        float b = Mathf.Sqrt(Mathf.Pow(v3.x - v2.x, 2) + Mathf.Pow(v3.y - v2.y, 2));
        float c = Mathf.Sqrt(Mathf.Pow(v1.x - v3.x, 2) + Mathf.Pow(v1.y - v3.y, 2));
        float s = (a + b + c) / 2;
        return (a * v1.x + b * v2.x + c * v3.x) / (a + b + c);
    }

    private float CircumCenterY()
    {
        float a = Mathf.Sqrt(Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.y - v1.y, 2));
        float b = Mathf.Sqrt(Mathf.Pow(v3.x - v2.x, 2) + Mathf.Pow(v3.y - v2.y, 2));
        float c = Mathf.Sqrt(Mathf.Pow(v1.x - v3.x, 2) + Mathf.Pow(v1.y - v3.y, 2));
        float s = (a + b + c) / 2;
        return (a * v1.y + b * v2.y + c * v3.y) / (a + b + c);
    }  

    public bool VertexInCircum(Vertex v)
    {
        float x = v.x;
        float y = v.y;
        float r = CircumRadius();
        float cx = CircumCenterX();
        float cy = CircumCenterY();
        return Mathf.Sqrt(Mathf.Pow(x - cx, 2) + Mathf.Pow(y - cy, 2)) < r;
    }
    public Vertex[] GetVertices()
    {
        Vertex[] vertices = new Vertex[3];
        vertices[0] = v1;
        vertices[1] = v2;
        vertices[2] = v3;
        return vertices;
    }

    public bool SameTriangle(Triangle triangle)
    {
        float x1 = triangle.v1.x;
        float y1 = triangle.v1.y;
        float x2 = triangle.v2.x;
        float y2 = triangle.v2.y;
        float x3 = triangle.v3.x;
        float y3 = triangle.v3.y;

        bool sameV1 = false;
        bool sameV2 = false;
        bool sameV3 = false;
        if(v1.x == x1)
        {
            if(v1.y == y1)
            {
                sameV1 = true;
            }
        }
        if (v2.x == x1)
        {
            if (v2.y == y1)
            {
                sameV2 = true;
            }
        }
        if (v3.x == x1)
        {
            if (v3.y == y1)
            {
                sameV3 = true;
            }
        }
        return sameV1 && sameV2 && sameV3;
    }
    public bool HasVertex(Vertex[] v)
    {
        

        foreach(Vertex vertex in v)
        {
            if(vertex.x == v1.x && vertex.y == v1.y)
            {
                return true;
            }
            if (vertex.x == v2.x && vertex.y == v2.y)
            {
                return true;
            }
            if (vertex.x == v3.x && vertex.y == v3.y)
            {
                return true;
            }
        }

        return false;
    }
    
    
    public void DebugListVisulaizer()
    {
        Debug.Log($"TRI ID: {id} v1: ({v1.x}, {v1.y}), v2: ({v2.x}, {v2.y}), v3: ({v3.x}, {v3.y})" );
    }
    
    public string IDSortTriangleInXAxis()
    {
        Vertex ver1 = v1;
        Vertex ver2 = v2;
        Vertex ver3 = v3;
        
        if(ver1.x > ver2.x)
        {
            Vertex temp = ver1;
            ver1 = ver2;
            ver2 = temp;
        }
        if(ver2.x > ver3.x)
        {
            Vertex temp = ver2;
            ver2 = ver3;
            ver3 = temp;
        }
        if(ver1.x > ver2.x)
        {
            Vertex temp = ver1;
            ver1 = ver2;
            ver2 = temp;
        }
        return $"{ver1.x} {ver1.y} {ver2.x} {ver2.y} {ver3.x} {ver3.y}";
    }
}