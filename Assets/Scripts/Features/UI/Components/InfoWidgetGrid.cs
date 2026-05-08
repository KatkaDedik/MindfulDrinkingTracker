
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
public class InfoWidgetGrid : MonoBehaviour
{
    [SerializeField] private RectTransform _mainPannelRectTransform;
    [SerializeField] private RectTransform[] _sidePannelRectTransforms;
    [SerializeField] private float _spacing = 10f;
    
    public void Initialize(DrinkingGoal goal, InfoWidget[] infoWidgets)
    {
        RecalculateChildPanels();
        switch (goal)
        {
            case DrinkingGoal.StayInControl:
                AssignWidgetsToSlots(InfoWidgetType.Promile, infoWidgets);
                break;
            case DrinkingGoal.LimitDrinks:
                AssignWidgetsToSlots(InfoWidgetType.Drinks, infoWidgets);
                break;
            case DrinkingGoal.DriveTomorrow:
                AssignWidgetsToSlots(InfoWidgetType.SoberIn, infoWidgets);
                break;
            default:
                AssignWidgetsToSlots(InfoWidgetType.Promile, infoWidgets);
                break;
        }
    }

    private void AssignWidgetsToSlots(InfoWidgetType priorityWidget, InfoWidget[] infoWidgets)
    {
        int freeSlotIndex = 0;
        for (int i = 0; i < infoWidgets.Length; i++)
        {
            InfoWidget widget = infoWidgets[i];
            if (widget.WidgetType == priorityWidget)
            {
                widget.WidgetController.gameObject.transform.SetParent(_mainPannelRectTransform, false);
                widget.WidgetController.SetMainWidget();
            }
            else
            {
                widget.WidgetController.gameObject.transform.SetParent(_sidePannelRectTransforms[freeSlotIndex], false);
                widget.WidgetController.SetSubWidget();
                freeSlotIndex++;
            }
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

}

