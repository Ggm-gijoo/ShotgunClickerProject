using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text upgraderNameText2 = null;
    [SerializeField]
    private Text priceText2 = null;
    [SerializeField]
    private Text amountText2 = null;
    [SerializeField]
    private Button purchaseButton2 = null;
    [SerializeField]
    private Image upgraderImage2 = null;
    [SerializeField]
    private Sprite[] upgraderSprite2;

    public Upgrader upgrader2 = null;
    public UIManager uIManager;

    public void Start()
    {
        uIManager = GetComponent<UIManager>();
        if (upgrader2.amount >= 1)
        {
            GameManager.Instance.UI.ShadowHandLeft.SetActive(true);
            GameManager.Instance.UI.ShadowHandRight.SetActive(true);
            
        }
    }

    public void SetValue(Upgrader upgrader2)
    {
        this.upgrader2 = upgrader2;
        UIUpdate();
    }
    public void Update()
    {
        if(upgrader2.price >= GameManager.Instance.CurrentUser.stress)
        {
            upgraderImage2.color = Color.gray;
            priceText2.text = string.Format("<color=#930001>" + "{0} 스트레스" + "</color>", upgrader2.price);
        }
        else
        {
            upgraderImage2.color = Color.white;
            priceText2.text = string.Format("<color=#ffffff>" + "{0} 스트레스" + "</color>", upgrader2.price);
        }
    }
    public void UIUpdate()
    {
        upgraderNameText2.text = upgrader2.upgraderName;

            priceText2.text = string.Format("<color=#ffffff>" + "{0} 스트레스" + "</color>", upgrader2.price);
        amountText2.text = string.Format("{0}", upgrader2.amount);
        if (upgrader2.amount >= 1)
        {
            Destroy(purchaseButton2);
        }
        upgraderImage2.sprite = upgraderSprite2[upgrader2.upgraderNum];
    }
    public void OnClickPurchaseShadowHand()
    {
        if (GameManager.Instance.CurrentUser.stress < upgrader2.price)
        {
            return;
        }
        GameManager.Instance.CurrentUser.stress -= upgrader2.price;
        upgrader2.amount++;
        if (upgrader2.amount >= 1)
        {
            Destroy(purchaseButton2);
            GameManager.Instance.UI.ShadowHandLeft.SetActive(true);
            GameManager.Instance.UI.ShadowHandRight.SetActive(true);
        }
        UIUpdate();
        GameManager.Instance.UI.UpdateEnergyPanel();
    }

}
