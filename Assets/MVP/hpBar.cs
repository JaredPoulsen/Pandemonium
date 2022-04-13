using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    // Start is called before the first frame update
    //public Slider slider;
    public ThirdPersonController player;
    public float lerpSpeed;
    public Image healthBar;
    public float maxHealth = 100f;
    // Update is called once per frame

    void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (player.Health / 100), lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (player.Health / 100));
        healthBar.color = healthColor;
    }
}
