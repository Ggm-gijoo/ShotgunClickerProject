using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyText : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private Transform pool = null;
    private Text energyText = null;

    public void Show(Vector2 moucePosition)
    {
        energyText = GetComponent<Text>();
        energyText.text = string.Format("-{0}", GameManager.Instance.CurrentUser.dPc);

        transform.SetParent(canvas.transform);

        transform.position = Camera.main.ScreenToWorldPoint(moucePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        gameObject.SetActive(true);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 50f;

        energyText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }

    public void Despawn()
    {
        energyText.DOFade(1f, 0f);
        transform.SetParent(pool);
        gameObject.SetActive(false);
    }
}
