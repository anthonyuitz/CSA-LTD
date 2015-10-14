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
    private List<GameObject> buttonUIList;
    private GameObject livesText;
    private GameObject goldText;
    private int lives;
    private int gold;
    private bool towerMenuOpen;
    private int towerMenuSelect;
    private bool enemyMenuOpen;

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

        buttonUIList = new List<GameObject>();

        SetUpUI();

        InitGame();
    }

    void InitGame()
    {
        boardScript.SetUpScene();
        lives = 50;
        gold = 100;
        towerMenuOpen = false;
        enemyMenuOpen = false;
    }

    // Update is called once per frame
    void Update () {
        checkForMenuInput();
	}

    void checkForMenuInput()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (towerMenuOpen) { towerMenuOpen = false; closeMenuUI(); }

            else { towerMenuOpen = true; createTowerMenuUI(); }
           
            enemyMenuOpen = false;
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            if (enemyMenuOpen) { enemyMenuOpen = false; closeMenuUI(); }

            else { enemyMenuOpen = true; createEnemyMenuUI(); }

            towerMenuOpen = false;
        }

        if ((towerMenuOpen || enemyMenuOpen) && Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if(getUIButtonIndex(c) != -1)
                {
                    buttonUIClicked(c);
                }
            }
        }
   }

    public void loseLives(int x)
    {
        lives -= x;
        livesText.GetComponent<Text>().text = "" + lives;
        if (lives == 0)
            gameover(true);
    }

    public void changeGold(int x)
    {
        gold += x;
        goldText.GetComponent<Text>().text = "" + gold;
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

    void SetUpUI()
    {
        UICanvas = Instantiate(CanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        livesText = UICanvas.transform.FindChild("PlayerInfoUI").FindChild("LivesUI").FindChild("LivesText").gameObject;
        goldText = UICanvas.transform.FindChild("PlayerInfoUI").FindChild("GoldUI").FindChild("GoldText").gameObject;

        populateButtonUIList();

        closeMenuUI();
        
    }

    void populateButtonUIList()
    {
        GameObject q = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonQ").gameObject;
        q.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('q'));
        buttonUIList.Add(q);
        GameObject w = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonW").gameObject;
        w.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('w'));
        buttonUIList.Add(w);
        GameObject e = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonE").gameObject;
        e.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('e'));
        buttonUIList.Add(e);
        GameObject r = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonR").gameObject;
        r.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('r'));
        buttonUIList.Add(r);
        GameObject a = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonA").gameObject;
        a.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('a'));
        buttonUIList.Add(a);
        GameObject s = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonS").gameObject;
        s.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('s'));
        buttonUIList.Add(s);
        GameObject d = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonD").gameObject;
        d.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('d'));
        buttonUIList.Add(d);
        GameObject f = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonF").gameObject;
        f.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('f'));
        buttonUIList.Add(f);
        GameObject z = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonZ").gameObject;
        z.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('z'));
        buttonUIList.Add(z);
        GameObject x = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonX").gameObject;
        x.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('x'));
        buttonUIList.Add(x);
        GameObject c = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonC").gameObject;
        c.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('c'));
        buttonUIList.Add(c);
        GameObject v = UICanvas.transform.FindChild("SelectorUI").FindChild("ButtonV").gameObject;
        v.GetComponent<Button>().onClick.AddListener(() => buttonUIClicked('v'));
        buttonUIList.Add(v);
    }

    void buttonUIClicked(char s)
    {
        if (enemyMenuOpen)
        {
            Vector3 position = new Vector3(Random.Range(-4.0F, 4.0F), 0.5F, Random.Range(15.5F, 18.5F));
            GameObject instance = Instantiate(obj, position, Quaternion.identity) as GameObject;
            GameObject b = buttonUIList[getUIButtonIndex(s)];
            instance.GetComponent<Renderer>().material.color = b.GetComponent<Image>().color;
            instance.transform.SetParent(enemyHolder);
        }
    }

    int getUIButtonIndex(char c)
    {
        if (c == 'q')
        {
            return 0;
        }
        else if (c == 'w')
        {
            return 1;
        }
        else if (c == 'e')
        {
            return 2;
        }
        else if (c == 'r')
        {
            return 3;
        }
        else if (c == 'a')
        {
            return 4;
        }
        else if (c == 's')
        {
            return 5;
        }
        else if (c == 'd')
        {
            return 6;
        }
        else if (c == 'f')
        {
            return 7;
        }
        else if (c == 'z')
        {
            return 8;
        }
        else if (c == 'x')
        {
            return 9;
        }
        else if (c == 'c')
        {
            return 10;
        }
        else if (c == 'v')
        {
            return 11;
        }
        return -1;
    }

    string getUIButtonText(int c)
    {
        if (c == 0)
        {
            return "Q";
        }
        else if (c == 1)
        {
            return "W";
        }
        else if (c == 2)
        {
            return "E";
        }
        else if (c == 3)
        {
            return "R";
        }
        else if (c == 4)
        {
            return "A";
        }
        else if (c == 5)
        {
            return "S";
        }
        else if (c == 6)
        {
            return "D";
        }
        else if (c == 7)
        {
            return "F";
        }
        else if (c == 8)
        {
            return "Z";
        }
        else if (c == 9)
        {
            return "X";
        }
        else if (c == 10)
        {
            return "C";
        }
        else if (c == 11)
        {
            return "V";
        }
        return "-1";
    }

    void createTowerMenuUI()
    {

    }

    void createEnemyMenuUI()
    {
        for (int x = 0; x < buttonUIList.Count; x++)
        {
            Color c = buttonUIList[x].GetComponent<Image>().color;
            buttonUIList[x].GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1);
            buttonUIList[x].transform.FindChild("Text").GetComponent<Text>().text = getUIButtonText(x);
        }
    }

    void closeMenuUI()
    {
        for (int x = 0; x < buttonUIList.Count; x++)
        {
            Color c = buttonUIList[x].GetComponent<Image>().color;
            buttonUIList[x].GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0);
            buttonUIList[x].transform.FindChild("Text").GetComponent<Text>().text = "";
        }
    }
}
