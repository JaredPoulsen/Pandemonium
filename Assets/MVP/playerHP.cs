using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHP : MonoBehaviour
{
    public int hp = 250;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            hp -= 10;

            
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
