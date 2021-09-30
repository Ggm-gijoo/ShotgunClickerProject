using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel;


    public void OnclickOption()
    {
        if (OptionPanel.activeSelf == false)
        {
            OptionPanel.SetActive(true);
        }
    }

    public void OnContinueGame()
    {
        if (OptionPanel.activeSelf == true)
        {
            OptionPanel.SetActive(false);
        }
    }



}
