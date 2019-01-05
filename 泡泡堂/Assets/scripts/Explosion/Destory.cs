using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour
{
    public float liftTime = .5f;

    private void Start()
    {
        // 实例的Explosion在0,5秒后销毁
        Destroy(gameObject, liftTime);
    }
}
