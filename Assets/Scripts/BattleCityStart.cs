using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleCityStart : MonoBehaviour
{
    public GameObject player;
    public GameObject[] playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if (player != null && playerPos.Length >= 2)
            {
                Vector3 pos1 = playerPos[0].transform.position;
                Vector3 pos2 = playerPos[1].transform.position;
                player.transform.position = player.transform.position == pos1 ? pos2 : pos1;
            }
        }
        //Enter键
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("BattleCity");
        }
    }
}
