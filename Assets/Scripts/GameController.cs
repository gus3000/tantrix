using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public TextAsset tilesCsvFile;
    public GameObject tilePrefab;
    public GameObject placeholderPrefab;

    private Queue<TileInfo.LineColor[]> bag;
    private List<GameObject> placeholders;
    public Dictionary<HexCoordinates, HexCell> Cells { get; private set; }

    private void Awake()
    {
        bag = new Queue<TileInfo.LineColor[]>();
        placeholders = new List<GameObject>();
        Cells = new Dictionary<HexCoordinates, HexCell>();

        TileInfo.LineColor[][] tiles = TileInfo.GetTilesFromText(tilesCsvFile.text);

        foreach (TileInfo.LineColor[] tile in tiles)
        {
            bag.Enqueue(tile);
//            Debug.Log(string.Join(",", tile));
        }

        bag = new Queue<TileInfo.LineColor[]>(bag.OrderBy(colors => UnityEngine.Random.value));

//        Cells.Add(HexCoordinates.FromOffsetCoordinates(0, 0), CreateTile(0, 0, bag.Dequeue()));

//        for(int i=0; i<5; ++i)
//        for (int j = 0; j < 5; ++j)
//        {
//            GameObject g = Instantiate(placeholderPrefab);
//            g.name = i + "," + j;
//            var hc = new HexCoordinates(i, j);
//            g.transform.position = HexMetrics.CellCoords(hc.X, hc.Y);
//        }


        StartCoroutine(PlaceTiles());
    }

    private IEnumerator PlaceTiles()
    {
        int x = 0, y = 0;
        const int maxX = 6;
        while (bag.Count > 0)
        {
            TileInfo.LineColor[] tile = bag.Dequeue();
            for (int i = 0; i < 6; ++i)
            {
                TileInfo.LineColor[] rotation = TileInfo.GetRotation(tile, i);
                HexCell cell = CreateTile(x, y, rotation);
                Cells.Add(cell.coordinates, cell);
                x++;
                if (x >= maxX)
                {
                    x = 0;
                    y++;
                }

                HexCoordinates[] placements = CorrectPlacements(tile);
                foreach (GameObject g in placeholders)
                    Destroy(g);
                placeholders = new List<GameObject>();
                foreach (HexCoordinates coor in placements)
                {
                    GameObject g = Instantiate(placeholderPrefab);
                    g.transform.position = HexMetrics.CellCoords(coor.X, coor.Y);
                    g.name = "Placeholder " + coor;
                    placeholders.Add(g);
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }

    private HexCoordinates[] CorrectPlacements(TileInfo.LineColor[] tile) //TODO handle rotation
    {
        Debug.Log("Checking correct placements");
        List<HexCoordinates> correctPlacements = new List<HexCoordinates>();


        foreach (KeyValuePair<HexCoordinates, HexCell> entry in Cells)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("for Cell " + entry.Value.coordinates + " :\n");
            foreach (HexCoordinates hc in entry.Key.Neighbours())
            {
                if (Cells.ContainsKey(hc) || correctPlacements.Contains(hc))
                    continue;

                sb.Append("\t" + hc + "\n");
                correctPlacements.Add(hc);
            }

            Debug.Log(sb);
        }

        return correctPlacements.ToArray();
    }

    private HexCell CreateTile(int x, int y, TileInfo.LineColor[] colors)
    {
        GameObject g = Instantiate(tilePrefab);
        g.transform.position = HexMetrics.CellCoords(x, y);

        HexCell cell = g.GetComponent<HexCell>();
        cell.SideColor = colors;
        cell.coordinates = new HexCoordinates(x, y);
        cell.InitCurves();

        g.name = "Tile " + cell.coordinates;
        return cell;
    }
}