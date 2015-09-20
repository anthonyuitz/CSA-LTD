using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject obj;
    private Transform enemyHolder;
	private List<Vector3> spawnPath;
    public Transform towerHolder;

    public BoardManager boardScript;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        enemyHolder = new GameObject("Enemies").transform;
        boardScript = GetComponent<BoardManager>();
        towerHolder = new GameObject("Towers").transform;

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

            //instance.GetComponent<EnemyMovement>().calculatePath();

            instance.transform.SetParent(enemyHolder);   
        }
	}


}
