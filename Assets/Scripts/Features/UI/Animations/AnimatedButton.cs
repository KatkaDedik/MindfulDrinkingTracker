using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _hoverScale = 1.1f;
    [SerializeField] private float _duration = 0.15f;
    private Button _button;

    private Tween _scaleTween;

    private void Awake()
    {
        if (!TryGetComponent(out _button))
        {
            Debug.LogWarning("AnimatedButton requires a Button component on the same GameObject.");
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (_button != null)
            _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClicked);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateScale(_hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateScale(1f);
    }

    private void AnimateScale(float target)
    {
        _scaleTween?.Kill();

        _scaleTween = transform.DOScale(target, _duration)
            .SetEase(Ease.OutQuad);
    }

    private void OnClicked()
    {
        _scaleTween?.Kill();

        transform.localScale = Vector3.one;

        transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 10, 1)
            .OnComplete(() => transform.localScale = Vector3.one);
    }
}
