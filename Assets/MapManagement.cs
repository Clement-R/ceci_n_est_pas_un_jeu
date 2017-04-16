using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagement : MonoBehaviour {
    static public int horizontalSize = 32;
    static public int verticalSize = 18;
    static public List<List<Vector2>> capturePaths = new List<List<Vector2>>();

    static public List<List<Vector2>> buildingsPositions = new List<List<Vector2>>();
    public List<GameObject> buildings = new List<GameObject>();

    private int[,] map = new int[18, 32] {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                          {0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0},
                                          {0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0},
                                          {0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0},
                                          {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                          {0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0},
                                          {0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0},
                                          {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                          {0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0},
                                          {0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0},
                                          {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                          {0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0},
                                          {0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0},
                                          {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                          {0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0},
                                          {0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0},
                                          {0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0},
                                          {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

    private List<Vector2> processedTiles = new List<Vector2>();

    void Awake() {

        /*
        // Search for all possible cpature path
        for (int i = 0; i < 18; i++) {
            for (int j = 0; j < 32; j++) {
                int cell = map[i, j];

                if(cell == 1) {
                    Vector2 cellPos = new Vector2(i, j);

                    // If this tile wasn't already processed
                    if(!processedTiles.Contains(cellPos)) {
                        // Check if there is unprocessed cells (1) in its neighborhood
                        checkNeighbor(cellPos);
                            // If so, we add this 
                        processedTiles.Add(cellPos);
                    }
                }
            }
        }
        */

        /*
        foreach (var cell in getNeighbours(new Vector2(1, 1))) {
            Debug.Log(cell.y + " : " + cell.x);
        }
        */

        buildingsPositions.Add(new List<Vector2> {new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(0,2),
            new Vector2(1,0),
            new Vector2(1,2),
            new Vector2(1,3),
            new Vector2(2,0),
            new Vector2(2,3),
            new Vector2(3,0),
            new Vector2(3,3),
            new Vector2(4,0),
            new Vector2(4,1),
            new Vector2(4,2),
            new Vector2(4,3)
        });
    }

    List<Vector2> getNeighbours(Vector2 pos) {
        List<Vector2> neighbours = new List<Vector2>();
        List<Vector2> temp = new List<Vector2>();
        neighbours.Add(pos);

        // For all the cells we check their neighbors and add them if they're equal to 1
        // and they're not already processed
        bool newCellAdded = true;
        while (newCellAdded) {
            newCellAdded = false;

            foreach (var cell in neighbours) {
                // Check all 8 neighbours
                Debug.Log("Get neigh : " + cell.y + " ; " + cell.x);
                for (int i = -1; i <= 1; i++) {
                    for (int j = -1; j <= 1; j++) {

                        if (i != 0 || j != 0) {

                            int y = Mathf.RoundToInt(cell.y) + i;
                            int x = Mathf.RoundToInt(cell.x) + j;

                            Vector2 cellPos = new Vector2(y, x);
                            int cellValue = map[y, x];
                                
                            if (cellValue == 1 && !processedTiles.Contains(cellPos) && !neighbours.Contains(cellPos)) {
                                Debug.Log(y + " ; " + x);
                                newCellAdded = true;
                                temp.Add(cellPos);
                                processedTiles.Add(cellPos);
                            }
                        }

                    }
                }
            }

            foreach (var obj in temp) {
                if(!neighbours.Contains(obj)) {
                    neighbours.Add(obj);
                }
            }
        }
        Debug.Log("------------------");
        return neighbours;
    }
}
