using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]
public class Score : ScriptableObject
{
    public int initValue;
    public int value;


    private void OnEnable()
    {
        value = initValue;
    }


}
   
  

