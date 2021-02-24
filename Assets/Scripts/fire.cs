using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fire : MonoBehaviour
{
    public void clickBtn()
    {
        GameObject.Find("Player").GetComponent<Player>().Fire();
        Debug.Log("+++++++++++++++++++"+ GameObject.Find("Player").ToString());
        
    }

    public void reStartGame()
    {
        SceneManager.LoadScene("start");
    }
}
