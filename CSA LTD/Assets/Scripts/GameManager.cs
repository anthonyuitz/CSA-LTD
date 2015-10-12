using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject obj;
    private Transform enemyHolder;
	private List<Vector3> spawnPath;
    public Transform towerHolder;
    public GameObject CanvasPrefab;
    public GameObject UICanvas;
    private int lives;
    private int gold;

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

        UICanvas = Instantiate(CanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        

        InitGame();
    }

    void InitGame()
    {
        boardScript.SetUpScene();
        lives = 50;
        gold = 100;
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

    public void loseLives(int x)
    {
        lives -= x;
        UICanvas.transform.FindChild("PlayerInfoUI").FindChild("LivesUI").FindChild("LivesText").gameObject.GetComponent<Text>().text = "" + lives;
        if (lives == 0)
            gameover(true);
    }

    public void changeGold(int x)
    {
        gold += x;
        UICanvas.transform.FindChild("PlayerInfoUI").FindChild("GoldUI").FindChild("GoldText").gameObject.GetComponent<Text>().text = "" + gold;
    }

    void gameover(bool lost)
    {
        if(lost)
        {
            //put what happens when you lose the game here
        }
        else
        {
            //put what happens when you win the game here
        }
    }

}
