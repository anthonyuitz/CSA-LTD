using UnityEngine;
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

    public void towerPlaced(Vector3 vector, int towerCode)
    {
        int index = getIndexFromVector(vector);
        boardStatus[index] = towerCode;
        boardStatus[index + 1] = towerCode;
        boardStatus[index + 20] = towerCode;
        boardStatus[index + 21] = towerCode;

        foreach (Transform child in boardHolder)
        {
            Vector3 bottomleft = new Vector3(vector.x, vector.y, vector.z - 1F);
            Vector3 bottomright = new Vector3(vector.x + 1F, vector.y, vector.z - 1F);
            Vector3 topright = new Vector3(vector.x + 1F, vector.y, vector.z);

            if (child.position.Equals(bottomleft) || child.position.Equals(bottomright) || child.position.Equals(topright))
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

        for (int y = -10; y < columns - 10; y++)
        {
            for (int x = -15; x < rows - 15; x++)
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
