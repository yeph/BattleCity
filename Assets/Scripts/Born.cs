using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    // 0=Player 1=Enemy1 2=Enemy2 3=Enemy3
    public GameObject[] tankList;
    public float invekeTime = 0.5f;

    public TankType tankType =  TankType.Player;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateTank", invekeTime);
        Destroy(this.gameObject, invekeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Player Enemy
    void CreateTank()
    {
        GameObject obj =  null;
        if (tankList.Length < 4)
        {
            return;
        }
        switch (tankType)
        {
            case TankType.Player:
                if (tankList[0] == null) return;
                 obj = Instantiate(tankList[0],this.transform.position,tankList[0].transform.rotation);
                obj.name = "Player";
                break;
            case TankType.Enemy1:
                 obj = Instantiate(tankList[1], this.transform.position, tankList[1].transform.rotation);
                obj.name = "Enemy1";
                break;
            case TankType.Enemy2:
                 obj = Instantiate(tankList[2], this.transform.position, tankList[2].transform.rotation);
                obj.name = "Enemy2";
                break;
            case TankType.Enemy3:
                 obj = Instantiate(tankList[3], this.transform.position, tankList[3].transform.rotation);
                obj.name = "Enemy3";
                break;
        }
    }
}
