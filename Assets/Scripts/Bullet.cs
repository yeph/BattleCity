using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Player = 0,
    Enemy = 1,
}
public class Bullet : MonoBehaviour
{

    public float speed = 2f;
    public Vector3 direction = new Vector3(0, 1, 0);//Y方向的向量

    public BulletType bulletType = BulletType.Player;
    public float damageValue = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (bulletType == BulletType.Player)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(direction * speed * Time.deltaTime);
    }
    //this.gameObject碰撞到collider时候 触发
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.Player)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
               // Debug.Log("打中敌方坦克了！");
                collision.gameObject.SendMessage("BeHit", damageValue);
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                //Debug.Log("打中墙了！");
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("AirWall"))
            {
               // Debug.Log("打中空气墙了！");
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("GoldWall"))
            {
               // Debug.Log("打中铁墙了！");
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("Home"))
            {
               // Debug.Log("打中家了！");
                GameManager.gameManager_Instance.GameOver();
                Destroy(this.gameObject);
            }
        }

        if (bulletType == BulletType.Enemy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
              //  Debug.Log("敌人打到我了！");
                collision.gameObject.SendMessage("BeHit", damageValue);
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("AirWall"))
            {
               // Debug.Log("打中空气墙了！");
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("Wall"))
            {
               // Debug.Log("打中墙了！");
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("GoldWall"))
            {
               // Debug.Log("打中铁墙了！");
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("Home"))
            {
               // Debug.Log("打中家了！");
                GameManager.gameManager_Instance.GameOver();
                Destroy(this.gameObject);
            }
        }

    }
}
