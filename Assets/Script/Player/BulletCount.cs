using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCount : MonoBehaviour
{
    public ThirdPersonController tpc;
    private TextMesh text;
    private int numOfBullet;
    private int magazine;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        numOfBullet = tpc.WeaponInUse.BulletsAmounts;
        magazine = tpc.WeaponInUse.BulletsPerMagazine;

        text.text = numOfBullet.ToString();

        if(numOfBullet > magazine * 0.8)
        {
            text.color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
        } 
        else if (numOfBullet > magazine * 0.6 && numOfBullet <= magazine * 0.8)
        {
            text.color = new Color(128f / 255f, 255f / 255f, 0f / 255f);
        }
        else if (numOfBullet > magazine * 0.4 && numOfBullet <= magazine * 0.6)
        {
            text.color = new Color(255f / 255f, 255f / 255f, 0f / 255f);
        }
        else if (numOfBullet > magazine * 0.2 && numOfBullet <= magazine * 0.4)
        {
            text.color = new Color(255f / 255f, 128f / 255f, 0f / 255f);
        }
        else if (numOfBullet > magazine * 0 && numOfBullet <= magazine * 0.2)
        {
            text.color = new Color(255f / 255f, 60f / 255f, 0f / 255f);
        }
        else if (numOfBullet == 0)
        {
            text.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        }

    }
}
