using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public int columns = 20;
    public int rows = 30;

    public GameObject tile;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    public List<int> boardStatus = new List<int>();

    int getIndexFromVector(Vector3 vector)
    {
        return gridPositions.IndexOf(vector);
    }

    public void towerPlaced(int index, int towerCode)
    {
        boardStatus[index] = towerCode;
        boardStatus[index + 1] = towerCode;
        boardStatus[index + columns] = towerCode;
        boardStatus[index + columns + 1] = towerCode;

        foreach (Transform child in boardHolder)
        {
			int childIndex = child.gameObject.GetComponent<TileManager>().index;

			if (childIndex == index+1 || childIndex == index+columns || childIndex == index+columns+1)
            {
                Renderer rend = child.gameObject.GetComponent<Renderer>();
                rend.material.color = Color.gray;
            }
        }

    }

    public int tileStatus(Vector3 vector)
    {
        return boardStatus[getIndexFromVector(vector)];
    }

	public int tileStatus(int index) {
        if(index >= boardStatus.Count)
        {
            return -1;
        }
		return boardStatus [index];
	}

    void InitializeList()
    {
        gridPositions.Clear();

        for (int y = 14; y >= 15 - rows; y--)
        {
            for (int x = -10; x < columns - 10; x++)
            {
                gridPositions.Add(new Vector3(x + .5F, 0.01F, y + .5F));
            }
        }

        boardStatus.Clear();

		for (int y = 14; y >= 15 - rows; y--)
		{
			for (int x = -10; x < columns - 10; x++)
			{
                boardStatus.Add(0);
            }
        }
    }

    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;

        for (int y = 14; y >= 15 - rows; y--)
        {
			for (int x = -10; x < columns - 10; x++)
            {
                GameObject instance = Instantiate(tile, new Vector3(x + .5F, 0.01F, y + .5F), Quaternion.identity) as GameObject;

				instance.GetComponent<TileManager>().index = (14-y)*columns+(x+10);

                instance.transform.SetParent(boardHolder);
            }
        }
    }


	public void SetUpScene()
    {
        BoardSetUp();
        InitializeList();
    }

    public List<Vector3> findShortestPath(Vector3 start)
    { 
        List<Vector3> ans = new List<Vector3>();
        int startIndex = findClosestTile(start);
        List<int> indexPath = findTilePath(startIndex);
        ans.Add(start);
        for(int x = 0; x < indexPath.Count; x++)
        {
            ans.Add(gridPositions[indexPath[x]]);
        }
        Vector3 end = ans[ans.Count - 1];
        end.z = end.z - 2F;
        ans.Add(end);
        return ans;
    }

    private int findClosestTile(Vector3 pos)
    { //untested
        int ans = -1;
        double ansDist = 999999;

        foreach (Transform child in boardHolder)
        {
            int childIndex = child.gameObject.GetComponent<TileManager>().index;
            if (pos.z > 15)
            {
                if (boardStatus[childIndex] == 0 && childIndex <= 14 && childIndex >= 5)
                {
                    double tempDist = Vector3.Distance(child.gameObject.transform.position, pos);
                    if (tempDist < ansDist)
                    {
                        ansDist = tempDist;
                        ans = childIndex;
                    }
                }
            }
            else
            {
                if (boardStatus[childIndex] == 0)
                {
                    double tempDist = Vector3.Distance(child.gameObject.transform.position, pos);
                    if (tempDist < ansDist)
                    {
                        ansDist = tempDist;
                        ans = childIndex;
                    }
                }
            }
        }
        return ans;
    }

    private List<int> findTilePath(int start)
    { 

        PriorityQueue<int> openset = new PriorityQueue<int>();
        List<int> closedset = new List<int>();
        Dictionary<int, int> came_from = new Dictionary<int, int>();
        Dictionary<int, int> g_score = new Dictionary<int, int>();
        Dictionary<int, double> f_score = new Dictionary<int, double>();

        for(int x = 0; x < boardStatus.Count; x++)
        {
            g_score[x] = 999999;
            f_score[x] = 999999;
        }
        g_score[start] = 0;
        f_score[start] = g_score[start] + HeuristicCostEstimate(start);

        openset.Add(f_score[start], start);

        while (openset.Count > 0)
        {
            int current = openset.Peek();
            if(current <= 594 && current >= 585 && boardStatus[current] == 0)
            {
                List<int> path = new List<int>();
                while(current != start)
                {
                    path.Add(current);
                    current = came_from[current];
                }
                path.Add(start);
                path.Reverse();
                return path;
            }

            openset.RemoveMin();
            closedset.Add(current);
            List<int> neighbors = findNeighbors(current);
            for(int x = 0; x < neighbors.Count; x++)
            {
                int neighbor = neighbors[x];
                if(closedset.Contains(neighbor))
                {
                    continue;
                }

                int tentative_g_score = g_score[current] + 1;

                if(!openset.Contains(neighbor) || tentative_g_score < g_score[neighbor])
                {
                    came_from[neighbor] = current;
                    g_score[neighbor] = tentative_g_score;
                    f_score[neighbor] = g_score[neighbor] + HeuristicCostEstimate(neighbor);
                    if(!openset.Contains(neighbor))
                    {
                        openset.Add(f_score[neighbor], neighbor);
                    }
                }
            }
        }

        //return failure      
        return new List<int>();
    }

    private List<int> findNeighbors(int index)
    {
        List<int> neighbors = new List<int>();
        if(index == 0) //top left
        {
            if (boardStatus[1] == 0) { neighbors.Add(1); }
            if (boardStatus[20] == 0) { neighbors.Add(20); }
        }
        else if(index == 19) //top right
        {
            if (boardStatus[18] == 0) { neighbors.Add(18); }
            if (boardStatus[39] == 0) { neighbors.Add(39); }
        }
        else if(index == 599) //bottom right
        {
            if (boardStatus[598] == 0) { neighbors.Add(598); }
            if (boardStatus[579] == 0) { neighbors.Add(579); }
        }
        else if(index == 580) //bottom left
        {
            if (boardStatus[581] == 0) { neighbors.Add(581); }
            if (boardStatus[560] == 0) { neighbors.Add(560); }
        }
        else if(index % 20 == 0) //left edge
        {
            if (boardStatus[index+1] == 0) { neighbors.Add(index+1); }
            if (boardStatus[index-20] == 0) { neighbors.Add(index-20); }
            if (boardStatus[index+20] == 0) { neighbors.Add(index+20); }
        }
        else if(index > 0 && index < 19) //top edge 
        {
            if (boardStatus[index + 1] == 0) { neighbors.Add(index + 1); }
            if (boardStatus[index - 1] == 0) { neighbors.Add(index - 1); }
            if (boardStatus[index + 20] == 0) { neighbors.Add(index + 20); }
        }
        else if(index < 599 && index > 580) //bottom edge
        {
            if (boardStatus[index + 1] == 0) { neighbors.Add(index + 1); }
            if (boardStatus[index - 1] == 0) { neighbors.Add(index - 1); }
            if (boardStatus[index - 20] == 0) { neighbors.Add(index - 20); }
        }
        else if((index + 1) % 20 == 0) //right edge
        {
            if (boardStatus[index - 1] == 0) { neighbors.Add(index - 1); }
            if (boardStatus[index - 20] == 0) { neighbors.Add(index - 20); }
            if (boardStatus[index + 20] == 0) { neighbors.Add(index + 20); }
        }
        else //rest
        {
            if (boardStatus[index + 1] == 0) { neighbors.Add(index + 1); }
            if (boardStatus[index - 1] == 0) { neighbors.Add(index - 1); }
            if (boardStatus[index - 20] == 0) { neighbors.Add(index - 20); }
            if (boardStatus[index + 20] == 0) { neighbors.Add(index + 20); }
        }
        return neighbors;
    }
    
    private double HeuristicCostEstimate(int index1)
    {
        Vector3 start = gridPositions[index1];
        double ans = 9999999;
        for(int y = 585; y <= 594; y++)
        {
            if(boardStatus[y] == 0)
            {
                double dist = Vector3.Distance(start, gridPositions[y]);
                if(dist < ans)
                {
                    ans = dist;
                }
            }
        }
        return ans;
    }
}

class MinHeap<T> where T : IComparable<T>
{
    private List<T> array = new List<T>();

    public void Add(T element)
    {
        array.Add(element);
        int c = array.Count - 1;
        while (c > 0 && array[c].CompareTo(array[c / 2]) == -1)
        {
            T tmp = array[c];
            array[c] = array[c / 2];
            array[c / 2] = tmp;
            c = c / 2;
        }
    }

    public T RemoveMin()
    {
        T ret = array[0];
        array[0] = array[array.Count - 1];
        array.RemoveAt(array.Count - 1);

        int c = 0;
        while (c < array.Count)
        {
            int min = c;
            if (2 * c + 1 < array.Count && array[2 * c + 1].CompareTo(array[min]) == -1)
                min = 2 * c + 1;
            if (2 * c + 2 < array.Count && array[2 * c + 2].CompareTo(array[min]) == -1)
                min = 2 * c + 2;

            if (min == c)
                break;
            else
            {
                T tmp = array[c];
                array[c] = array[min];
                array[min] = tmp;
                c = min;
            }
        }

        return ret;
    }

    public T Peek()
    {
        return array[0];
    }

    public bool Contains(T element)
    {
        for(int x = 0; x < array.Count; x++)
        {
            if(array[x].Equals(element))
            {
                return true;
            }
        }
        return false;
    }

    public int Count
    {
        get
        {
            return array.Count;
        }
    }
}

class PriorityQueue<T>
{
    internal class Node : IComparable<Node>
    {
        public double Priority;
        public T O;
        public int CompareTo(Node other)
        {
            return Priority.CompareTo(other.Priority);
        }
        public bool Equals(Node other)
        {
            return O.Equals(other.O);
        }
        
    }

    private MinHeap<Node> minHeap = new MinHeap<Node>();
	private List<T> elements = new List<T> ();

    public void Add(double priority, T element)
    {
        minHeap.Add(new Node() { Priority = priority, O = element });
		elements.Add (element);
    }

    public T RemoveMin()
    {
		T el = minHeap.RemoveMin().O;
		elements.Remove (el);
		return el; 
    }

    public T Peek()
    {
        return minHeap.Peek().O;
    }

	//currently using a workaround, fix removemin and contains in node / minheap to fix
    public bool Contains(T element)
    { 
        //return minHeap.Contains(new Node() { Priority = 0, O = element });

		return elements.Contains (element);
    }

    public int Count
    {
        get
        {
            return minHeap.Count;
        }
    }
}