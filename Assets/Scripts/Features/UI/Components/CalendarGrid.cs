using System.Collections;

using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class SimpleGridFit : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private float _cellToSpaceRatio = 0.1f;
    private const int _columns = 7;

    private void Reset()
    {
        _grid = GetComponent<GridLayoutGroup>();
    }

    private void OnEnable()
    {
        Calculate();
    }

    private void Start()
    {
        Calculate(); // runtime (mobile)
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Calculate(); // editor auto update
    }
#endif

    [ContextMenu("Recalculate Grid")]
    public void Calculate()
    {
        if (_grid == null) return;

        var rect = (RectTransform)transform;
        float width = Mathf.Min(rect.rect.width, rect.rect.height);

        if (width <= 0) return;

        float cell = width / (_columns + _cellToSpaceRatio);

        _grid.enabled = true;

        _grid.cellSize = new Vector2(cell * (1 - _cellToSpaceRatio), cell * (1 - _cellToSpaceRatio));
        _grid.spacing = new Vector2(cell * _cellToSpaceRatio, cell * _cellToSpaceRatio);
        StartCoroutine(TurnOfGrid());
    }

    private IEnumerator TurnOfGrid()
    {
        yield return new WaitForEndOfFrame();
        _grid.enabled = false;
    }

}
