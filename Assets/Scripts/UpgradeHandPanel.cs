using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandPanel : MonoBehaviour
{
    [SerializeField]
    private Text upgraderNameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Button purchaseButton = null;
    [SerializeField]
    private Image upgraderImage = null;
    [SerializeField]
    private Sprite[] upgraderSprite;

    private Upgrader upgrader = null;


    public void SetValue(Upgrader upgrader)
    {
        this.upgrader = upgrader;
        UIUpdate();
    }

    public void Update()
    {
        if (upgrader.price >= GameManager.Instance.CurrentUser.stress)
        {
            upgraderImage.color = Color.gray;
            priceText.text = string.Format("<color=#930001>" + "{0} 스트레스" + "</color>", upgrader.price);
        }
        else
        {
            upgraderImage.color = Color.white;
            priceText.text = string.Format("<color=#ffffff>" + "{0} 스트레스" + "</color>", upgrader.price);
        }
    }

    public void UIUpdate()
    {
        upgraderNameText.text = upgrader.upgraderName;
         priceText.text = string.Format("<color=#ffffff>" + "{0} 스트레스" + "</color>", upgrader.price);
        amountText.text = string.Format("{0}", upgrader.amount);
        upgraderImage.sprite = upgraderSprite[upgrader.upgraderNum];
    }

    public void OnClickPurchaseHand()
    {
        if (GameManager.Instance.CurrentUser.stress < upgrader.price)
        {
            return;
        }

        GameManager.Instance.CurrentUser.stress -= upgrader.price;
        upgrader.price = (long)(upgrader.price * 1.2f);
        upgrader.sPs = (long)(upgrader.sPs * 1.1);
        if (upgrader.upgraderNum == 0)
        {
            GameManager.Instance.CurrentUser.dPc = (long)(GameManager.Instance.CurrentUser.dPc * 1.34f);
        }
        upgrader.amount++;
        UIUpdate();
        GameManager.Instance.UI.UpdateEnergyPanel();
    }

}
