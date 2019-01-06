using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public int playerNumber = 1;
    public new Animation animation;

    //人物当前行走的方向状态  
    public int state = 0;
    public int moveSpeed = 2;
    public LayerMask wallMask;
    public LayerMask bombMask;

    private float x;
    private float z;
    private string horizontialName;
    private string verticalName;

    private const float brickLength = 10;
    private float rayDistance;

    private const int UP = 0;
    private const int RIGHT = 1;
    private const int DOWN = 2;
    private const int LEFT = 3;

    //初始化人物位置  
    private void Awake()
    {
        state = UP;
    }

    private void Start()
    {
        rayDistance = brickLength / 2 + 0.5f;
        horizontialName = "Horizontal" + playerNumber;
        verticalName = "Vertical" + playerNumber;
    }

    private void Update()
    {
        //获取控制的方向， 上下左右， 
        // GetAxisRaw 操作比 GetAxis更流畅
        x = Input.GetAxisRaw(horizontialName);
        z = Input.GetAxisRaw(verticalName);

        if (z == -1)
        {
            setState(DOWN);
        }
        else if (z == 1)
        {
            setState(UP);
        }
        if (x == 1)
        {
            setState(RIGHT);
        }
        else if (x == -1)
        {
            setState(LEFT);
        }

        if (z == 0 && x == 0)
        {
            animation.Play("idle");
        }


    }

    private void setState(int newState)
    {
        //根据当前人物方向与上一次备份的方向计算出模型旋转的角度  
        //float rotateValue = (newState - state) * 90;
        Vector3 transformValue = new Vector3();

        //播放行走动画  
        animation.Play("run");

        //transform.Rotate(Vector3.up, rotateValue);
        transform.rotation = Quaternion.Euler(new Vector3(0, newState * 90, 0));
        //state = newState;

        if (isWall(newState) || isBomb(newState)) return;
        //模型移动的位置数值  
        switch (newState)
        {
            case UP:
                transformValue = Vector3.forward * Time.deltaTime;
                break;
            case DOWN:
                transformValue = Vector3.back * Time.deltaTime;
                break;
            case LEFT:
                transformValue = Vector3.left * Time.deltaTime;
                break;
            case RIGHT:
                transformValue = Vector3.right * Time.deltaTime;
                break;
        }

        //移动人物  
        transform.Translate(transformValue * moveSpeed, Space.World);

    }

    private bool isWall(int state)
    {
        RaycastHit hit;
        if(state == 0)
            Physics.Raycast(transform.position, Vector3.forward, out hit, rayDistance, wallMask);
        else if(state == 1)
            Physics.Raycast(transform.position, Vector3.right, out hit, rayDistance, wallMask);
        else if(state == 2)
            Physics.Raycast(transform.position, Vector3.back, out hit, rayDistance, wallMask);
        else
            Physics.Raycast(transform.position, Vector3.left, out hit, rayDistance, wallMask);

        if (hit.collider)
            return true;
        else
            return false;
    }

    private bool isBomb(int state)
    {
        RaycastHit hit;
        if (state == 0)
            Physics.Raycast(transform.position, Vector3.forward, out hit, rayDistance, bombMask);
        else if (state == 1)
            Physics.Raycast(transform.position, Vector3.right, out hit, rayDistance, bombMask);
        else if (state == 2)
            Physics.Raycast(transform.position, Vector3.back, out hit, rayDistance, bombMask);
        else
            Physics.Raycast(transform.position, Vector3.left, out hit, rayDistance, bombMask);

        if (hit.collider)
            return true;
        else
            return false;
    }

}
