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
        Debug.Log(hp);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("hit");
            hp -= 25;

            
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            hp -= 100;


            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
