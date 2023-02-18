using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrimsMST 
{
    private int seed;
    public PrimsMST(int _seed)
    {
        Random.InitState(_seed);
    }
    public List<Edge> MST(List<Triangle> triangleList, int roomNum)
    {
        List<Vertex> vistedVertex = new List<Vertex>();
        List<Edge> ListEdges = ConvertTriangleListToEdgeList(triangleList);
        Vertex startVertex = ListEdges[Random.Range(0, ListEdges.Count - 1)].v1;
        List<Edge> mst = new List<Edge>();

        while(vistedVertex.Count < roomNum - 1)
        {
            List<Edge> edgesThatStartWithVertex = FindAllEdgesStartingAtVertex(startVertex, ListEdges);
            float minWeight = float.MaxValue;
            Edge minEdge = null;
            foreach(Edge edge in edgesThatStartWithVertex)
            {
                if(!vistedVertex.Any(ver => ver.x == edge.v2.x && ver.y == edge.v2.y) 
                    && edge.weight < minWeight) 
                {
                    minWeight = edge.weight;
                    minEdge = edge;
                }
                    
            }
            vistedVertex.Add(startVertex);
            mst.Add(minEdge);
            startVertex = minEdge.v2;
            
        }
        return AddRandomEdge(ListEdges, mst);
    }

    private List<Edge> AddRandomEdge(List<Edge> ListAllEdges, List<Edge> _mst)
    {
        foreach(Edge edge in ListAllEdges)
        {
            int random = Random.Range(0, 100);
            if(!_mst.Any(e => e.v1.x == edge.v1.x && e.v1.y == edge.v1.y && e.v2.x == edge.v2.x && e.v2.y == edge.v2.y)
            && !_mst.Any(e => e.v1.x == edge.v2.x && e.v1.y == edge.v2.y && e.v2.x == edge.v1.x && e.v2.y == edge.v1.y)
            && random < 15)
            {
                _mst.Add(edge);
            }
            
        }
        return _mst;
    }

    private List<Edge> FindAllEdgesStartingAtVertex(Vertex vertex, List<Edge> edgeList)
    {
        List<Edge> result = new List<Edge>();
        foreach(Edge edge in edgeList)
        {
            if(edge.v1.x == vertex.x && edge.v1.y == vertex.y)
            {
                result.Add(edge);
            }
                   
        }
        return result;
    }

    private List<Edge> ConvertTriangleListToEdgeList(List<Triangle> triangleList)
    {
        List<Edge> edgeList = new List<Edge>();
        foreach(Triangle triangle in triangleList)
        {
            edgeList.Add(new Edge(triangle.v1, triangle.v2));
            edgeList.Add(new Edge(triangle.v2, triangle.v3));
            edgeList.Add(new Edge(triangle.v3, triangle.v1));
        }
        return EdgeListToSet(edgeList);
    }

    

    private List<Edge> EdgeListToSet(List<Edge> edgeList)
    {
        Dictionary<string, Edge> edgeSet = new Dictionary<string, Edge>();
        foreach(Edge edge in edgeList)
        {
            if(!edgeSet.ContainsKey(edge.id))
            {
                edgeSet.Add(edge.id, edge);
            }
        }
        List<Edge> result = new List<Edge>();
        foreach(KeyValuePair<string, Edge> edge in edgeSet)
        {
            result.Add(edge.Value);
        }
        return result;
    }

    
}
