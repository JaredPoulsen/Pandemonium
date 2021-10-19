using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenestuff : MonoBehaviour
{
    public void GotoLevel(int level)
    {
        Debug.Log("Loading scene " + level);
        SceneManager.LoadScene(level);
        //Allows me to be able to change the scene
    }
}
