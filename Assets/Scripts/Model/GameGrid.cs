using System.Collections.Generic;
using UnityEngine;

public class GridCase {
    public int index;
    public Vector3 position;
    public bool free;

    public GridCase(int index){
        this.index = index;
        position = new Vector3 ();
        free = true;
    }
}

public class GameGrid {

    Transform planetCenter;

    readonly int minDistance;
    readonly int depth;
    readonly int angle;

    int spaceAngleSize;
    readonly List<GridCase> grid;

    public GameGrid(Transform center) {
        planetCenter = center;

        minDistance = 6;
        depth = 2;
        angle = 26;

        spaceAngleSize = 7;
        grid = new List<GridCase>();
        PopulateGrid ();
    }

    void PopulateGrid(){
        float stepAngle = angle / (float)spaceAngleSize;
        float startAngle = -angle / 2 + stepAngle / 2;

        int index = 0;
        for (int d = 0; d < depth; d++){
            for (int i = 0; i < spaceAngleSize; i++, index++) {
                GridCase c = new GridCase (index);
                c.position.Set (0f, minDistance + d, 0f);
                c.position = RotateAroundPoint (c.position, planetCenter.position, new Vector3 (0f, 0f, startAngle + stepAngle * i));
                grid.Add (c);
            }
        }
    }

    Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Vector3 euler){
        return Quaternion.Euler(euler) * (point - pivot) + pivot;
    }

    public void FreeSpaceAtIndex(int index){
        grid[index].free = true;
    }

    public bool HaveSpace(int depth) {
        for (int i = depth * spaceAngleSize; i < depth * spaceAngleSize + spaceAngleSize; i++) {
            if (grid[i].free == true) {
                return true;
            }
        }

        return false;
    }

    public GridCase FindSpace(int depth){
        int index;
        do {
            index = Random.Range (depth * spaceAngleSize, depth * spaceAngleSize + spaceAngleSize);
        } while (grid[index].free == false);

        grid[index].free = false;

        return grid[index];
    }
}
