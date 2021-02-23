using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : MonoBehaviour
{


    private float fingerActionSensitivity = Screen.width * 0.05f; //手指动作的敏感度，这里设定为 二十分之一的屏幕宽度.
                                                                  //
    private float fingerBeginX;
    private float fingerBeginY;
    private float fingerCurrentX;
    private float fingerCurrentY;
    private float fingerSegmentX;
    private float fingerSegmentY;
    //
    private int fingerTouchState;
    //
    private int FINGER_STATE_NULL = 0;
    private int FINGER_STATE_TOUCH = 1;
    private int FINGER_STATE_ADD = 2;
    // Use this for initialization
    void Start()
    {
        fingerActionSensitivity = Screen.width * 0.05f;

        fingerBeginX = 0;
        fingerBeginY = 0;
        fingerCurrentX = 0;
        fingerCurrentY = 0;
        fingerSegmentX = 0;
        fingerSegmentY = 0;

        fingerTouchState = FINGER_STATE_NULL;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (fingerTouchState == FINGER_STATE_NULL)
            {
                fingerTouchState = FINGER_STATE_TOUCH;
                fingerBeginX = Input.mousePosition.x;
                fingerBeginY = Input.mousePosition.y;
            }

        }

        if (fingerTouchState == FINGER_STATE_TOUCH)
        {
            fingerCurrentX = Input.mousePosition.x;
            fingerCurrentY = Input.mousePosition.y;
            fingerSegmentX = fingerCurrentX - fingerBeginX;
            fingerSegmentY = fingerCurrentY - fingerBeginY;

        }


        if (fingerTouchState == FINGER_STATE_TOUCH)
        {
            float fingerDistance = fingerSegmentX * fingerSegmentX + fingerSegmentY * fingerSegmentY;

            if (fingerDistance > (fingerActionSensitivity * fingerActionSensitivity))
            {
                //toAddFingerAction();
                fingerTouchState = FINGER_STATE_ADD;

                if (Mathf.Abs(fingerSegmentX) > Mathf.Abs(fingerSegmentY))
                {
                    fingerSegmentY = 0;
                }
                else
                {
                    fingerSegmentX = 0;
                }

                if (fingerSegmentX == 0)
                {
                    if (fingerSegmentY > 0)
                    {
                        Debug.Log("up");
                        Player.player_Instance.playerRotation = Vector3.zero;
                    }
                    else
                    {
                        Debug.Log("down");
                        Player.player_Instance.playerRotation = new Vector3(0, 0, 180);
                    }
                }
                else if (fingerSegmentY == 0)
                {
                    if (fingerSegmentX > 0)
                    {
                        Debug.Log("right");
                        Player.player_Instance.playerRotation = new Vector3(0, 0, -90);
                    }
                    else
                    {
                        Debug.Log("left");
                        Player.player_Instance.playerRotation = new Vector3(0, 0, 90);
                    }
                }


                //重置player方向
                Player.player_Instance.transform.rotation = Quaternion.Euler(Player.player_Instance.playerRotation);//重置对象的rotation 三元转换
                                                                                                                    //如果有方向键输出 移动游戏对象
                if (Player.player_Instance.vertical != 0 || Player.player_Instance.horizontal != 0)
                {
                    //移动：速度
                    Player.player_Instance.transform.Translate(Player.player_Instance.direction * Player.player_Instance.speed * Time.deltaTime);//Time.deltaTime帧率
                }

            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fingerTouchState = FINGER_STATE_NULL;
        }
    }

    //private void toAddFingerAction()
    //{

    //    fingerTouchState = FINGER_STATE_ADD;

    //    if (Mathf.Abs(fingerSegmentX) > Mathf.Abs(fingerSegmentY))
    //    {
    //        fingerSegmentY = 0;
    //    }
    //    else
    //    {
    //        fingerSegmentX = 0;
    //    }

    //    if (fingerSegmentX == 0)
    //    {
    //        if (fingerSegmentY > 0)
    //        {
    //            Debug.Log("up");
    //            Player.player_Instance.playerRotation = Vector3.zero;
    //        }
    //        else
    //        {
    //            Debug.Log("down");
    //            Player.player_Instance.playerRotation = new Vector3(0, 0, 180);
    //        }
    //    }
    //    else if (fingerSegmentY == 0)
    //    {
    //        if (fingerSegmentX > 0)
    //        {
    //            Debug.Log("right");
    //            Player.player_Instance.playerRotation = new Vector3(0, 0, -90);
    //        }
    //        else
    //        {
    //            Debug.Log("left");
    //            Player.player_Instance.playerRotation = new Vector3(0, 0, 90);
    //        }
    //    }


    //    //重置player方向
    //    Player.player_Instance.transform.rotation = Quaternion.Euler(Player.player_Instance.playerRotation);//重置对象的rotation 三元转换
    //                                                                                      //如果有方向键输出 移动游戏对象
    //    if (Player.player_Instance.vertical != 0 || Player.player_Instance.horizontal != 0)
    //    {
    //        //移动：速度
    //        Player.player_Instance.transform.Translate(Player.player_Instance.direction * Player.player_Instance.speed * Time.deltaTime);//Time.deltaTime帧率
    //    }

    //}
}
