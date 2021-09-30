using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text energyText = null;
    [SerializeField]
    private Text stressText = null;
    [SerializeField]
    private Animator rightHandAnimator = null;
    [SerializeField]
    private Animator leftHandAnimator = null;
    [SerializeField]
    private Animator shadowRightHandAnimator = null;
    [SerializeField]
    private Animator shadowLeftHandAnimator = null;
    [SerializeField]
    private EnergyText energyTextTemplate = null;
    [SerializeField]
    private ShadowText shadowTextTemplate = null;
    [SerializeField]
    private GameObject handUpgradePanelTamplate = null;
    [SerializeField]
    private GameObject upgradePanelTamplate = null;
    [SerializeField]
    private Transform pool = null;
    AudioSource[] audioSource;

    public int KeyboardNum;
    public int KeyboardNum1;
    public int KeyboardNum2;

    public GameObject Keyboard;
    public GameObject Keyboard1;
    public GameObject Keyboard2;
    public GameObject Keyboard3;
    public GameObject ShadowHandLeft = null;
    public GameObject ShadowHandRight = null;
    public GameObject Teemo = null;
    public GameObject TeemoIcon = null;


    private List<UpgradeHandPanel> upgradePanels = new List<UpgradeHandPanel>();
    private List<UpgradePanel> upgradePanels2 = new List<UpgradePanel>();

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        CreatePanels();
        CreatePanels2();
        UpdateEnergyPanel();
        ChangeSprite();
        
    }
    public void LeftHandOn()
    {
        ShadowHandLeft.SetActive(true);
    }

    public void OnClickRightHand()
    {
        int r = Random.Range(0, 100);
        if (r < 10)
        {
            GameManager.Instance.CurrentUser.dPc *= 2;
            GameManager.Instance.CurrentUser.keyboardHP -= GameManager.Instance.CurrentUser.dPc;
        }
        else
        {
            GameManager.Instance.CurrentUser.keyboardHP -= GameManager.Instance.CurrentUser.dPc;
        }
        GameManager.Instance.CurrentUser.stress += GameManager.Instance.CurrentUser.sPc;
        GameManager.Instance.CurrentUser.totalClick+=GameManager.Instance.CurrentUser.dPc;
        rightHandAnimator.Play("Click2");
        audioSource[0].Play();
        EnergyText newText = null;

        if (pool.childCount > 0)
        {
            newText = pool.GetChild(0).GetComponent<EnergyText>();
        }
        else
        {
            newText = Instantiate(energyTextTemplate, energyTextTemplate.transform.parent);
        }


        KeyboardDead();
        ChangeSprite();
        UpdateEnergyPanel();

        newText.Show(Input.mousePosition);
        if (r < 10)
        {
            GameManager.Instance.CurrentUser.dPc /= 2;
        }
        if(ShadowHandRight.activeSelf == true)
        {
            OnShadowHandRight();
            ShadowText newText2 = null;

            if (pool.childCount > 0)
            {
                newText2 = pool.GetChild(0).GetComponent<ShadowText>();
            }
            else
            {
                newText2 = Instantiate(shadowTextTemplate, shadowTextTemplate.transform.parent);
            }

            KeyboardDead();
            ChangeSprite();
            UpdateEnergyPanel();

            newText2.Show(Input.mousePosition);
        }
    }

    public void OnShadowHandRight()
    {
        GameManager.Instance.CurrentUser.keyboardHP -= GameManager.Instance.CurrentUser.dPc;
        GameManager.Instance.CurrentUser.totalClick += GameManager.Instance.CurrentUser.dPc;
        shadowRightHandAnimator.Play("ShadowClick2");
        audioSource[0].Play();

    }

    public void OnClickLeftHand()
    {
        int r = Random.Range(0, 100);
        if (r < 10)
        {
            GameManager.Instance.CurrentUser.dPc *= 2;
            GameManager.Instance.CurrentUser.keyboardHP -= GameManager.Instance.CurrentUser.dPc;
            
        }
        else
        {
            GameManager.Instance.CurrentUser.keyboardHP -= GameManager.Instance.CurrentUser.dPc;
        }
        GameManager.Instance.CurrentUser.stress += GameManager.Instance.CurrentUser.sPc;
        GameManager.Instance.CurrentUser.totalClick += GameManager.Instance.CurrentUser.dPc;
        leftHandAnimator.Play("Click1");
        audioSource[0].Play();
        EnergyText newText = null;

        if (pool.childCount > 0)
        {
            newText = pool.GetChild(0).GetComponent<EnergyText>();
        }
        else
        {
            newText = Instantiate(energyTextTemplate, energyTextTemplate.transform.parent);
        }


        KeyboardDead();
        ChangeSprite();
        UpdateEnergyPanel();
        newText.Show(Input.mousePosition);
        if (r < 10)
        {
            GameManager.Instance.CurrentUser.dPc /= 2;
        }
        if(ShadowHandLeft.activeSelf == true)
        {
            OnShadowHandLeft();
            ShadowText newText2 = null;

            if (pool.childCount > 0)
            {
                newText2 = pool.GetChild(0).GetComponent<ShadowText>();
            }
            else
            {
                newText2 = Instantiate(shadowTextTemplate, shadowTextTemplate.transform.parent);
            }

            KeyboardDead();
            ChangeSprite();
            UpdateEnergyPanel();

            newText2.Show(Input.mousePosition);
        }
    }
    public void OnShadowHandLeft()
    {
        GameManager.Instance.CurrentUser.keyboardHP -= GameManager.Instance.CurrentUser.dPc;
        GameManager.Instance.CurrentUser.totalClick += GameManager.Instance.CurrentUser.dPc;
        shadowLeftHandAnimator.Play("ShadowClick1");
        audioSource[0].Play();

    }

    public void UpdateEnergyPanel()
    {
        stressText.text = string.Format("스트레스:{0}", GameManager.Instance.CurrentUser.stress);
        if (Keyboard.activeSelf == true)
        {
            energyText.text = string.Format("키보드"+"\n내구도 : {0}", GameManager.Instance.CurrentUser.keyboardHP);
        }
        if (Keyboard1.activeSelf == true)
        {
            energyText.text = string.Format("<color=#930001>" + "벽돌 키보드" + "</color>" +"\n내구도 : {0}", GameManager.Instance.CurrentUser.keyboardHP);
        }
        if (Keyboard2.activeSelf == true)
        {
            energyText.text = string.Format("<color=#ffff00>" + "황금 키보드" + "</color>" +"\n내구도 : {0}", GameManager.Instance.CurrentUser.keyboardHP);
        }
        if (Keyboard3.activeSelf == true)
        {
            energyText.text = string.Format("<color=#ff0000>" + "무" + "</color>" + "<color=#ffff00>" + "지" + "</color>" + "<color=#00ff00>" + "개" + "</color>" + "<color=#00ffff>" + " 키" + "</color>" + "<color=#0000ff>" + "보" + "</color>" + "<color=#ff00ff>" + "드" + "</color>" + "\n내구도 : {0}", GameManager.Instance.CurrentUser.keyboardHP);
        }

    }

    private void CreatePanels()
    {
        GameObject newPanel = null;
        UpgradeHandPanel newPanelComponent = null;
        foreach (Upgrader upgrader in GameManager.Instance.CurrentUser.upgraderList)
        {
            newPanel = Instantiate(handUpgradePanelTamplate, handUpgradePanelTamplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgradeHandPanel>();
            newPanelComponent.SetValue(upgrader);
            newPanel.SetActive(true);
            upgradePanels.Add(newPanelComponent);
        }
    }
    private void CreatePanels2()
    {
        GameObject newPanel = null;
        UpgradePanel newPanelComponent = null;
        foreach (Upgrader upgrader in GameManager.Instance.CurrentUser.upgraderList2)
        {
            newPanel = Instantiate(upgradePanelTamplate, upgradePanelTamplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgradePanel>();
            newPanelComponent.SetValue(upgrader);
            newPanel.SetActive(true);
            upgradePanels2.Add(newPanelComponent);
        }
    }

    public void KeyboardDead()
    {
        if (Keyboard.activeSelf == true)
        {
            if (GameManager.Instance.CurrentUser.keyboardHP <= 0)
            {
                GameManager.Instance.CurrentUser.keyboardHP += 90000;
            }
        }

        if (Keyboard1.activeSelf == true)
        {
            if (GameManager.Instance.CurrentUser.keyboardHP <= 0)
            {
                GameManager.Instance.CurrentUser.keyboardHP += 900000;
            }
        }

        if (Keyboard2.activeSelf == true)
        {
            if (GameManager.Instance.CurrentUser.keyboardHP <= 0)
            {
                GameManager.Instance.CurrentUser.keyboardHP += 3000000;
            }
        }
    }

    public void ChangeSprite()
    {
        if (GameManager.Instance.CurrentUser.totalClick >= KeyboardNum)
        {

            Keyboard.SetActive(false);
            Keyboard1.SetActive(true);

        }
        if (GameManager.Instance.CurrentUser.totalClick >= KeyboardNum1)
        {

            Keyboard1.SetActive(false);
            Keyboard2.SetActive(true);

        }
        if (GameManager.Instance.CurrentUser.totalClick >= KeyboardNum2)
        {

            Keyboard2.SetActive(false);
            Keyboard3.SetActive(true);
        }
       
    }

}
