using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public int columns = 20;
    public int rows = 30;

    public GameObject tile;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();


    void InitializeList()
    {
        gridPositions.Clear();

        for (int y = -10; y < columns - 10; y++)
        {
            for (int x = -15; x < rows - 15; x++)
            { 
                gridPositions.Add(new Vector3(y + .5F, 0.01F, x + .5F));
            }
        }
    }

    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;

        for (int y = -10; y < columns - 10; y++)
        {
            for (int x = -15; x < rows - 15; x++)
            {
                GameObject instance = Instantiate(tile, new Vector3(y + .5F, 0.01F, x + .5F), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }


	public void SetUpScene()
    {
        BoardSetUp();
        InitializeList();
    }
}
