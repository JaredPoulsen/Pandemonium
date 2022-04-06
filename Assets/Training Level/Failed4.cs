using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failed4 : MonoBehaviour
{
    public EnemyBase enemy;
    public ThirdPersonController player;

    private GameObject gun;
    private Vector3 gunPos;
    private Quaternion gunRot;
    public GameObject prefab;
    bool isSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {

            if (enemy.dead == true && enemy.slow == false)
            {
                player.Health -= 100;
            }
        }
        gun = GameObject.FindGameObjectWithTag("4thgun");
        gunPos = gameObject.transform.position;
        gunRot = gameObject.transform.rotation;

        if (gun == null && isSpawn == false)
        {
            isSpawn = true;
            StartCoroutine(RespawnItem());

        }

    }
    IEnumerator RespawnItem()
    {
        if (isSpawn)
        {
            int respawnTime = 2;
            yield return new WaitForSeconds(respawnTime);
            GameObject pb = Instantiate(prefab, gunPos, gunRot);
            pb.tag = "4thgun";

        }
        isSpawn = false;
    }
}
