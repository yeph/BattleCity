using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //自动改变方向移动
    public float speed = 2f;
    public Vector3 direction = new Vector3(0, 1, 0);//Y方向的向量

    public Vector3 enemyRotation = Vector3.zero;
    public float randomTime = 2f;
    public float timer = 0f;

    public float bulletSpeed = 3.5f;
    public GameObject bullet;
    public GameObject gun;

    public float fireTimeInterval = 2f;
    public float fireTimer = 0f;

    public float PH = 1f;
    public bool IsDeath = false;

    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        randomTime = Random.Range(0.5f, 1f);

        fireTimeInterval = Random.Range(1f, 2f);
        //Invoke("Fire",fireTimeInterval);//延迟方法
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        this.transform.Translate(direction * speed * Time.deltaTime);
        if (timer >= randomTime)
        {
            changeRotation();
            timer = 0;
            randomTime = Random.Range(0.75f, 1.5f);
        }

        // Fire
        fireTimer += Time.deltaTime;
        if (fireTimer > fireTimeInterval)
        {
            Fire();
            fireTimer = 0;
            fireTimeInterval = Random.Range(1f, 2f);
        }

        if (IsDeath)
        {
            Bomb();
            Destroy(this.gameObject);
        }
    }
    //随机方向
    void changeRotation()
    {
        //0-2 上； 2-5下；5-7左；7-9右；
        float direc = Random.Range(0, 9);
        if (direc <2)
        {
            enemyRotation = new Vector3(0, 0, 0);
        }
        if (direc >= 2 && direc < 5)
        {
            enemyRotation = new Vector3(0, 0, 180);
        }
        if (direc >= 5 && direc < 7)
        {
            enemyRotation = new Vector3(0, 0, 90);
        }
        if (direc >= 7 && direc <= 9)
        {
            enemyRotation = new Vector3(0, 0, -90);
        }

        this.transform.rotation = Quaternion.Euler(enemyRotation);
    }

    //有枪有子弹 开火
    void Fire()
    {
        if (bullet != null && gun != null)
        {
            var obj = Instantiate(bullet, gun.transform.position, this.transform.rotation);
            obj.GetComponent<Bullet>().speed = bulletSpeed;
        }
    }
    //受到攻击
    void BeHit(float damageValue)
    {
        if (PH > 0)
        {
            PH -= damageValue;
        }
        if(PH <= 0)
        {
            IsDeath = true;
        }
    }

    void Bomb()
    {
        if (bomb != null)
        {
            Instantiate(bomb, this.transform.position, bomb.transform.rotation);
        }
    }
}
