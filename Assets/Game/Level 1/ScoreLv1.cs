using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreLv1", menuName = "ScriptableObjects/ScoreLv1", order = 2)]
public class ScoreLv1 : ScriptableObject
{
    public int initValue;
    public int value;


    private void OnEnable()
    {
        value = initValue;
    }
}
