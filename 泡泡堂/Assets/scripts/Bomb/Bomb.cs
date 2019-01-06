using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosionPrefab;
    public LayerMask mask;
    public float bombTime = 3;
    public int spout = 6; //水柱大小

    private const int brickSize = 10;
    private bool exploded = false;

    private void Start()
    {
        Invoke("Explode", bombTime);
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosion(Vector3.forward));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.back));
        StartCoroutine(CreateExplosion(Vector3.left));

        GetComponent<MeshRenderer>().enabled = false; //Disable mesh
        exploded = true;
        Destroy(gameObject, .5f);
    }

    private IEnumerator CreateExplosion(Vector3 direction)
    {
        for (int i = 1; i < spout; i++)
        {
            int j = brickSize * i;
            RaycastHit hit;

            Physics.Raycast(transform.position, direction, out hit, j, mask);

            if(!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + j * direction, explosionPrefab.transform.rotation);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(.05f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!exploded && other.CompareTag("Explosion"))
        {
            Explode();
        }
    }
}
