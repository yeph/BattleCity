using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using System;

public class Player : MonoBehaviour
{
    //Input控制器的使用
    public float horizontal = 0f;
    public float vertical = 0f;
    public float speed = 2f;

    public Vector3 startPos = new Vector3(0, 0, 0);
    public Vector3 endPos = new Vector3(0, 0, 0);

    public Vector3 direction = new Vector3(0, 0, 0);
    public bool isVerticalAvailable = false;

    public Vector3 playerRotation = Vector3.zero;

    public float bulletSpeed = 4f;
    public GameObject bullet;
    public GameObject gun;

    public float PH = 1f;
    public bool IsDeath = false;

    public GameObject bomb;

    public AudioClip AC;

    public static Player player_Instance;

    private void Awake()
    {
        player_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager_Instance.game_State == GameState.Running)
        {
            //判断键盘输入的 方向
            Gesture gesture = EasyTouch.current;
            float a = 0;
            float b = 0;
            if (gesture != null && EasyTouch.EvtType.On_TouchStart == gesture.type)
            {
                Debug.Log("开始位置" + gesture.startPosition.ToString());
                startPos = gesture.startPosition;
            }
            if (gesture != null && EasyTouch.EvtType.On_TouchUp == gesture.type)
            {
                a = 0;
                b = 0;
            }
            if (gesture != null && EasyTouch.EvtType.On_Swipe == gesture.type)
            {   //Left, Right, Up, Down
                Debug.Log("结束位置" + gesture.position.ToString());
                endPos = gesture.position;
                a = endPos.x - startPos.x;
                b = endPos.y - startPos.y;
                double angleOfLine = Math.Atan2(b, a) * 180 / Math.PI;
                //Debug.Log("角度" + angleOfLine);
                if (45 < angleOfLine && angleOfLine < 135)//向上
                {
                    playerRotation = Vector3.zero;
                }
                else if ((-45 <= angleOfLine && angleOfLine < 0) || (0 < angleOfLine && angleOfLine <= 45))//向右
                {
                    playerRotation = new Vector3(0, 0, -90);
                }
                else if ((-135 >= angleOfLine && angleOfLine > -180) || (angleOfLine >= 135 && angleOfLine < 180))//向左
                {
                    playerRotation = new Vector3(0, 0, 90);
                }
                else if (angleOfLine > -135 && angleOfLine < -45)//向下
                {
                    playerRotation = new Vector3(0, 0, 180);
                }
            }
            //重置player方向
            this.transform.rotation = Quaternion.Euler(playerRotation);

            if (a != 0 || b != 0)
            {
                //移动：速度
                this.transform.Translate(direction * speed * Time.deltaTime);//Time.deltaTime帧率
            }
            ////通过键盘按下或者松开 来获取水平/垂直的方向
            //if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            //{
            //    isVerticalAvailable = true;
            //}
            //if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            //{
            //    isVerticalAvailable = false;
            //}
            //if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            //{
            //    isVerticalAvailable = false;
            //}
            //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            //{
            //    isVerticalAvailable = true;
            //}

            //通过input控制器来判断垂直/水平方向
            //horizontal = Input.GetAxis("Horizontal"); //左右方向键 返回-1，0，1的值逐渐变化的值
            //vertical = Input.GetAxis("Vertical");
            //horizontal = Input.GetAxisRaw("Horizontal"); //左右方向键 只返回-1，0，1的值
            //vertical = Input.GetAxisRaw("Vertical");
            ////垂直方向
            //if (isVerticalAvailable)
            //{
            //    //上
            //    if (vertical > 0)
            //    {
            //        playerRotation = Vector3.zero;
            //    }
            //    //下
            //    if (vertical < 0)
            //    {
            //        playerRotation = new Vector3(0, 0, 180);
            //    }
            //}
            ////水平方向
            //if (!isVerticalAvailable)
            //{
            //    //右
            //    if (horizontal > 0)
            //    {
            //        playerRotation = new Vector3(0, 0, -90);
            //    }
            //    //左
            //    if (horizontal < 0)
            //    {
            //        playerRotation = new Vector3(0, 0, 90);
            //    }
            //}

            ////重置player方向
            //this.transform.rotation = Quaternion.Euler(playerRotation);//重置对象的rotation 三元转换
            //                                                           //如果有方向键输出 移动游戏对象
            //if (vertical != 0 || horizontal != 0)
            //{
            //    //移动：速度
            //    this.transform.Translate(direction * speed * Time.deltaTime);//Time.deltaTime帧率
            //}

            //发射子弹
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Fire();
            }

        }


        if (IsDeath)
        {
            Bomb();
            //延迟重新生成新的Player
            GameManager.gameManager_Instance.ReCreatePlayer();
            Destroy(this.gameObject);
        }
    }
    //有枪有子弹 开火
    public void Fire()
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
        if (PH <= 0)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("碰到星星了！");
            //子弹加速dw
            bulletSpeed += 1;
            if (GameManager.gameManager_Instance.currentStarCount == 1)
            {
                GameManager.gameManager_Instance.currentStarCount -= 1;
            }
            //Destroy(collision.gameObject);
            //collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.SendMessage("DestroyStar");
            AudioSource.PlayClipAtPoint(AC, transform.localPosition);
            collision.gameObject.SetActive(false);
        }
    }

}
