using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject obj;
    private Transform enemyHolder;
    public BoardManager boardScript;

    void Awake()
    {
        enemyHolder = new GameObject("Enemies").transform;
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetUpScene();
    }

    // Update is called once per frame
    void Update () {
	    if(Input.GetKeyDown(KeyCode.X))
        {
            Vector3 position = new Vector3(Random.Range(-4.0F, 4.0F), 0.5F, Random.Range(15.5F, 18.5F));
            GameObject instance = Instantiate(obj, position, Quaternion.identity) as GameObject;

            instance.transform.SetParent(enemyHolder);   
        }
	}
}
