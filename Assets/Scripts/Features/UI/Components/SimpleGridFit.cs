using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteAlways]
public class SimpleGridFit : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private float _cellToSpaceRatio = 0.1f;
    [SerializeField] private float _aspectRatio = 1f;
    [SerializeField] private int _columns = 7;

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
        Calculate();
        TurnOfGrid();
    }

    [ContextMenu("Recalculate Grid")]
    public void Calculate()
    {
        if (_grid == null || !gameObject.activeInHierarchy) return;


        var rect = (RectTransform)transform;
        float width = Mathf.Min(rect.rect.width, rect.rect.height);

        if (width <= 0) return;

        float cell = width / (_columns + _cellToSpaceRatio);

        _grid.enabled = true;
        _grid.constraintCount = _columns;
        _grid.cellSize = new Vector2(cell * (1 - _cellToSpaceRatio), (cell * (1 - _cellToSpaceRatio)) * _aspectRatio);
        _grid.spacing = new Vector2(cell * _cellToSpaceRatio, cell * _cellToSpaceRatio);
    }

    private IEnumerator TurnOfGrid()
    {
        yield return new WaitForEndOfFrame();
        _grid.enabled = false;
    }

}

[CustomEditor(typeof(SimpleGridFit))]
public class SimpleGridFitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Recalculate Grid"))
        {
            ((SimpleGridFit)target).Calculate();
        }
    }
}
