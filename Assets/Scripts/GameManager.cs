using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";

    [SerializeField]
    private User user = null;

    

    public User CurrentUser { get { return user; } }
    private UIManager uiManager = null;
    AudioSource[] audioSource;


    public UIManager UI
    {
        get
        {
            return uiManager;
        }
    }

    private void Awake()
    {
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson", 1f, 60f);
        InvokeRepeating("EarnEnergyPerSecond", 0f, 2f);
        LoadFromJson();
        uiManager = GetComponent<UIManager>();
        audioSource = GetComponents<AudioSource>();
    }

    private void EarnEnergyPerSecond()
    {
        foreach (Upgrader upgrader in user.upgraderList)
        {
            if (upgrader.sPs > 10)
            {
                GameManager.Instance.UI.Teemo.SetActive(true);
            }

            int r = Random.Range(0, 100);
            if (r < 10)
            {
                user.stress += upgrader.sPs * 2;
                if (upgrader.sPs > 10)
                {
                    StartCoroutine(TeemoCrit());
                }
            }
            else
            {
                if (upgrader.amount >= 1)
                {
                    user.stress += upgrader.sPs;
                }
            }
        }
        UI.UpdateEnergyPanel();
    }

    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
    }
    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }

    IEnumerator TeemoCrit()
    {
        GameManager.Instance.UI.TeemoIcon.SetActive(true);
        audioSource[1].Play();
        yield return new WaitForSeconds(1.2f);
        GameManager.Instance.UI.TeemoIcon.SetActive(false);
        StopCoroutine(TeemoCrit());
    }

}
