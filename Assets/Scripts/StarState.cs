using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyStar()
    {
        Debug.Log("-------------------------");
        Invoke("DestroyStars", 1f);//延迟
    }

    private void DestroyStars()
    {
        Debug.Log("-------------------------1");
        //Destroy(this.gameObject);
    }
}
