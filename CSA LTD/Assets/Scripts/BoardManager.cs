﻿using UnityEngine;
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
    { //TODO destination is any tile index 585-594 w/ 0 status
        List<int> path = new List<int>();
        for(int x = 10; x < boardStatus.Count; x=x+20)
        {
            path.Add(x);
        }
        return path;
    }
}
