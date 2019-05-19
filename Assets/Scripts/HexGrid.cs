using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public GameObject cellPrefab;
    public bool debug = false;
    public GameObject cellLabelPrefab;

    private Canvas _gridCanvas;
    private List<HexCell> _cells;
    private void Awake()
    {
        _gridCanvas = GetComponentInChildren<Canvas>();
    }

    private void Start()
    {
    }
    
//    private void CreateCell(int x, int y, int i)
//    {
//        float offsetX = -width / 2f - 1;
//        float offsetY = -height / 2f - 1;
//
//        Vector3 position = Vector3.zero;
//        
//        position.x = x * HexMetrics.OuterRadius * 1.5f + offsetX;
//        position.z = (y + x * .5f - Mathf.Floor(x / 2f)) * HexMetrics.InnerRadius * 2 + offsetY;
//        
//        HexCell cell = _cells[i] = Instantiate(cellPrefab).GetComponent<HexCell>();
//
//        Transform cellTransform = cell.transform;
//        cellTransform.SetParent(transform, false);
//        cellTransform.localPosition = position;
//        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x,y);
//
//        //DEBUG
//        if (debug)
//        {
//            Text label = Instantiate(cellLabelPrefab).GetComponent<Text>();
//            label.rectTransform.SetParent(_gridCanvas.transform, false);
//            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
//            label.text = cell.coordinates.ToString();
//        }
//    }
}