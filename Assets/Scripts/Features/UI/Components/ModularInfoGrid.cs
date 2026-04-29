
using UnityEngine;

public enum InfoWidgetType
{
    Promile,
    SoberIn,
    Drinks,
    Water,
    Goal,
}

[System.Serializable]
public struct InfoWidget
{
    public InfoWidgetType WidgetType;
    public InfoWidgetUIControllerBase WidgetController;
}

[RequireComponent(typeof(RectTransform))]
public class ModularInfoGrid : MonoBehaviour
{
    [SerializeField] private RectTransform _mainPannelRectTransform;
    [SerializeField] private RectTransform[] _sidePannelRectTransforms;
    [SerializeField] private float _spacing = 10f;
    [SerializeField] private InfoWidget[] _infoWidgets;
    private SessionService _sessionService;

    public void Initialize(DrinkingGoal goal, SessionService sessionService)
    {
        _sessionService = sessionService;
        RecalculateChildPanels();
        switch (goal)
        {
            case DrinkingGoal.StayInControl:
                AssignWidgetsToSlots(InfoWidgetType.Promile);
                break;
            case DrinkingGoal.LimitDrinks:
                AssignWidgetsToSlots(InfoWidgetType.Drinks);
                break;
            case DrinkingGoal.DriveTomorrow:
                AssignWidgetsToSlots(InfoWidgetType.SoberIn);
                break;
            case DrinkingGoal.JustTrack:
                AssignWidgetsToSlots(InfoWidgetType.Promile);
                break;
            case DrinkingGoal.None:
                AssignWidgetsToSlots(InfoWidgetType.Promile);
                break;
            default:
                AssignWidgetsToSlots(InfoWidgetType.Promile);
                break;
        }

    }

    private void RecalculateChildPanels()
    {
        var parentPannelRectTransform = GetComponent<RectTransform>();
        var size = ((parentPannelRectTransform.rect.width - 3 * _spacing) / 4f);
        parentPannelRectTransform.sizeDelta = new Vector2(parentPannelRectTransform.sizeDelta.x, size * 2 + _spacing);

        _mainPannelRectTransform.sizeDelta = new Vector2(size * 2 + _spacing, size * 2 + _spacing);
        for (int i = 0; i < _sidePannelRectTransforms.Length; i++)
        {
            _sidePannelRectTransforms[i].sizeDelta = new Vector2(size, size);
        }
    }

    private void AssignWidgetsToSlots(InfoWidgetType priorityWidget)
    {
        int freeSlotIndex = 0;
        for (int i = 0; i < _infoWidgets.Length; i++)
        {
            InfoWidget widget = _infoWidgets[i];
            widget.WidgetController.gameObject.SetActive(true);
            if (widget.WidgetType == priorityWidget)
            {
                widget.WidgetController.gameObject.transform.SetParent(_mainPannelRectTransform, false);
                widget.WidgetController.Initialize(true, _sessionService);
            }
            else
            {
                widget.WidgetController.gameObject.transform.SetParent(_sidePannelRectTransforms[freeSlotIndex], false);
                widget.WidgetController.Initialize(false, _sessionService);
                freeSlotIndex++;
            }
        }
    }

    public void UpdateWidgets()
    {
        foreach(var widget in _infoWidgets)
        {
            widget.WidgetController.UpdateWidget();
        }
    }
}

