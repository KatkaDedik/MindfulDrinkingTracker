using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GoalGridController : MonoBehaviour
{
    [SerializeField] private RectTransform _rootTransform;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private float _cellToSpaceRatio = 0.1f;
    [SerializeField] private float _aspectRatio = 1f;

    private void OnEnable()
    {
        Calculate();
    }

    private void Start()
    {
        Calculate();
        TurnOfGrid();
    }


    public void Calculate()
    {
        if (_grid == null || !_rootTransform.gameObject.activeInHierarchy) return;

        var rect = (RectTransform)transform;
        float width = rect.rect.width;

        if (width <= 0) return;

        float cellWithSpace = (width / (2 + _cellToSpaceRatio));
        float cellWidth = cellWithSpace * (1 - _cellToSpaceRatio);
        float cellHeight = cellWidth * _aspectRatio;
        float spacing = cellWithSpace * _cellToSpaceRatio;
        _rootTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (cellHeight + spacing) * 2);

        _grid.enabled = true;
        _grid.constraintCount = 2;
        _grid.cellSize = new Vector2(cellWidth, cellHeight);
        _grid.spacing = new Vector2(spacing, spacing);
    }

    private IEnumerator TurnOfGrid()
    {
        yield return new WaitForEndOfFrame();
        _grid.enabled = false;
    }
}
