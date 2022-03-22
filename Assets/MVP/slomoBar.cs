using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slomoBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public ThirdPersonController player;
    // Update is called once per frame
    void Update()
    {
        slider.maxValue = 5;
        slider.value = player.SlowedTime;

    }
}
