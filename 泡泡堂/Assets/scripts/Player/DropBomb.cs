using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject bombPrefabs;
    public bool canDropBomb = true;
    public LayerMask mask;

    private string fireName;
    private Vector3 bombPosition;
    private float Fx;
    private float Fz;

    private void Start()
    {
        fireName = "Fire" + playerNumber;
    }

    private void Update()
    {
        Fx = FixedPosition(transform.position.x);
        Fz = FixedPosition(transform.position.z);

        if (canDropBomb && Input.GetButtonDown(fireName))
        {
            DropBombs();
        }
    }

    private void DropBombs()
    {
        if(bombPrefabs && empty())
        {
            // 当前位置实例 Explosion
            bombPosition = new Vector3(Fx, bombPrefabs.transform.position.y, Fz);
            Instantiate(bombPrefabs, bombPosition, bombPrefabs.transform.rotation);
        }
    }

    private float FixedPosition(float source)
    {
        // 方格大小为10 * 10, 中心位置(~5,~5);
        return Mathf.Floor(source / 10) * 10 + 5;
    }

    private bool empty() 
    {
        RaycastHit hit;

        Physics.Raycast(new Vector3(Fx-10, bombPrefabs.transform.position.y, Fz), Vector3.right, out hit, 10f, mask);

        if (hit.collider)
        {
            return false;
        }
        else 
        { 
            return true;
        }

    }




}
