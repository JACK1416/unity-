using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject bombPrefabs;
    public bool canDropBomb = true;

    private string fireName;
    private Vector3 bombPosition;

    private void Start()
    {
        fireName = "Fire" + playerNumber;
    }

    private void Update()
    {
        if (canDropBomb && Input.GetButtonDown(fireName))
        {
            DropBombs();
        }
    }

    private void DropBombs()
    {
        if(bombPrefabs)
        {
            // 当前位置实例 Explosion
            bombPosition = new Vector3(FixedPosition(transform.position.x), 
                                       bombPrefabs.transform.position.y, FixedPosition(transform.position.z));
            Instantiate(bombPrefabs, bombPosition, bombPrefabs.transform.rotation);
        }
    }

    private float FixedPosition(float source)
    {
        // 方格大小为10 * 10, 中心位置(~5,~5);
        return Mathf.Floor(source / 10) * 10 + 5;
    }



}
