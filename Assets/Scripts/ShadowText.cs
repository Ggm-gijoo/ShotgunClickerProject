using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShadowText : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private Transform pool = null;
    private Text shadowText = null;

    public void Show(Vector2 moucePosition)
    {
        shadowText = GetComponent<Text>();
        shadowText.text = string.Format("(-{0})", GameManager.Instance.CurrentUser.dPc);

        transform.SetParent(canvas.transform);

        transform.position = Camera.main.ScreenToWorldPoint(moucePosition);
        transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, 0);
        gameObject.SetActive(true);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 50f;

        shadowText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }

    public void Despawn()
    {
        shadowText.DOFade(1f, 0f);
        transform.SetParent(pool);
        gameObject.SetActive(false);
    }
}
