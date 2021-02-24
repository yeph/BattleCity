using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TankType
{
    Player = 0,
    Enemy1 = 1,
    Enemy2 = 2,
    Enemy3 = 3,
}

public enum GameState
{
    Ready = 0,
    Running = 1,
    Pause = 2,
    GameOver = 3,
}
public class GameManager : MonoBehaviour
{
    //生成空气墙
    public float x_length = 10.5f;
    public float y_length = 8f;

    public GameObject AirWall;
    public GameObject Maps;
    public GameObject starParent;

    public GameObject born;
    public GameObject[] bornPos;

    public TankType enemyTankType = TankType.Enemy1;
    public Vector3 enemyTankPos = Vector3.zero;

    //public float enemyBornTimer = 0f;
    public float enemyBornTimeInterval = 2f;

    public int MaxEnemyCount = 5;
    public int RestEnemyCount = 30;
    public int CurrentEnemyCount = 0;
    public int PlayerCount = 3;

    public float invokeTime = 0.5f;
    //static的接口
    public static GameManager gameManager_Instance;

    public SpriteRenderer HomeSpriteRenderer;
    public Sprite home_Damaged;

    public GameState game_State = GameState.Ready;

    public GameObject gameOver;

    //奖励物品
    public GameObject star;
    public Vector3 startPos = Vector3.zero;
    public float starBornIntervalTime = 5f;
    public int currentStarCount = 0;

    private void Awake()
    {
        gameManager_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (game_State == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameRestart();
            }
        }
    }

    void CreateAirWall()
    {
        Vector3 pos1; 
        Vector3 pos2; 
        Vector3 pos3; 
        Vector3 pos4;
        for (float i=0;i<x_length;i++ )
        {
            //x =0.5 y=-8.5
             pos1 = new Vector3(i + 0.5f, -(y_length + 0.5f), 0);
             pos2 = new Vector3(-(i + 0.5f), -(y_length + 0.5f), 0);
             pos3 = new Vector3(i + 0.5f, (y_length + 0.5f), 0);
             pos4 = new Vector3(-(i + 0.5f), (y_length + 0.5f), 0);
            if (AirWall != null)
            {
                GameObject airWall1 = Instantiate(AirWall, pos1, AirWall.transform.rotation);//实例化AirWall 在pos1的位置
                GameObject airWall2 = Instantiate(AirWall, pos2, AirWall.transform.rotation);
                GameObject airWall3 = Instantiate(AirWall, pos3, AirWall.transform.rotation);
                GameObject airWall4 = Instantiate(AirWall, pos4, AirWall.transform.rotation);
                airWall1.transform.SetParent(Maps.transform);
                airWall2.transform.SetParent(Maps.transform);
                airWall3.transform.SetParent(Maps.transform);
                airWall4.transform.SetParent(Maps.transform);
            }
           
        }

        for (float j=0;j<=y_length; j++)
        {
            //x=11 y=0.5
            pos1 = new Vector3(x_length + 0.5f,j + 0.5f, 0);//右上
            pos2 = new Vector3(x_length + 0.5f, -(j + 0.5f), 0);//右下
            pos3 = new Vector3(-(x_length + 0.5f), (j + 0.5f), 0);//左上
            pos4 = new Vector3(-(x_length + 0.5f), -(j + 0.5f), 0);//左下
            if (AirWall != null)
            {
                GameObject airWall1 = Instantiate(AirWall, pos1, AirWall.transform.rotation);//实例化AirWall 在pos1的位置
                GameObject airWall2 = Instantiate(AirWall, pos2, AirWall.transform.rotation);
                GameObject airWall3 = Instantiate(AirWall, pos3, AirWall.transform.rotation);
                GameObject airWall4 = Instantiate(AirWall, pos4, AirWall.transform.rotation);
                airWall1.transform.SetParent(Maps.transform);
                airWall2.transform.SetParent(Maps.transform);
                airWall3.transform.SetParent(Maps.transform);
                airWall4.transform.SetParent(Maps.transform);
            }
        }
    }

    //生成自己的tank
    void CreatePlayer()
    {
        if (PlayerCount <= 0)
        {
            GameOver();
        };
        if (born != null && bornPos.Length >= 4 && bornPos[0] != null)
        {
            var obj = Instantiate(born, bornPos[0].transform.position, born.transform.rotation);
            obj.GetComponent<Born>().tankType = TankType.Player;
        }
        PlayerCount--;
    }

    void CreateEnemy()
    {
        if (RestEnemyCount <= 0) return;
        if (CurrentEnemyCount >= MaxEnemyCount) return;
        //0-1 tank1;1-3 tank2;3-6 tank3
        //0-2 2-4 4-6
        float randomEnemy = Random.Range(0, 6);
        float randomPos = Random.Range(0, 6);
        //随机tank
        if (randomEnemy <= 1)
        {
            enemyTankType = TankType.Enemy3;
        }
        if (randomEnemy > 1 && randomEnemy <= 3)
        {
            enemyTankType = TankType.Enemy2;
        }
        if (randomEnemy > 3 && randomEnemy <= 6)
        {
            enemyTankType = TankType.Enemy1;
        }
        //随机位置
        if (bornPos.Length < 4) return;
        if (randomPos <= 2)
        {
            if (bornPos[1] == null) return;
            enemyTankPos = bornPos[1].transform.position;
        }
        if (randomPos > 2 && randomPos <= 4)
        {
            if (bornPos[2] == null) return;
            enemyTankPos = bornPos[2].transform.position;
        }
        if (randomPos > 4 && randomPos <= 6)
        {
            if (bornPos[3] == null) return;
            enemyTankPos = bornPos[3].transform.position;
        }

        var obj = Instantiate(born, enemyTankPos, born.transform.rotation);
        obj.GetComponent<Born>().tankType = enemyTankType;

        CurrentEnemyCount++;
        RestEnemyCount--;
    }

    public void ReCreatePlayer()
    {
        Invoke("CreatePlayer", invokeTime);
    }

    public void GameOver()
    {
        if (HomeSpriteRenderer != null && home_Damaged != null)
        {
            HomeSpriteRenderer.sprite = home_Damaged;
            game_State = GameState.GameOver;
            gameOver.SetActive(true);
            gameOver.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    void GameRestart()
    {
        SceneManager.LoadScene("start");
    }

    void GameStart()
    {
        game_State = GameState.Running;
        CreateAirWall();
        CreatePlayer();
        InvokeRepeating("CreateEnemy", 0.1f, enemyBornTimeInterval);//延迟重复

        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }
        this.GetComponent<AudioSource>().Play();
        InvokeRepeating("CreateStar", starBornIntervalTime, starBornIntervalTime);
    }

    void CreateStar()
    {
        if (currentStarCount >= 1) return;
        GameObject parentObj = GameObject.Find("starParent");
        if (parentObj.transform.Find("Five_Star") != null)
        {
            GameObject child = parentObj.transform.Find("Five_Star").gameObject;
            child.SetActive(true);
            Destroy(child);
        }
        //x;-10 10 y:-7.5 7.5
        float x = Random.Range(-(x_length - 0.5f), x_length - 0.5f);
        float y = Random.Range(-(y_length - 0.5f), y_length - 0.5f);
        startPos = new Vector3(x, y, 0);
        if (star != null)
        {
            var starObj = Instantiate(star, startPos, star.transform.rotation);
            starObj.transform.SetParent(starParent.transform);
            starObj.name = "Five_Star";
            starObj.GetComponent<SpriteRenderer>().sortingOrder = 1; //设置层级
        }
        currentStarCount++;
    }
    
}
