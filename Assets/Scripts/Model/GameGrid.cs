using System.Collections.Generic;

public class GridCase {
    public bool free;

    public GridCase(){
        free = true;
    }
}

public class GameGrid {

    int spaceAngleSize;
    public int halfSpaceAngleSize;
    readonly List<GridCase> grid;

    public GameGrid() {
        spaceAngleSize = 7;
        halfSpaceAngleSize = spaceAngleSize / 2;
        grid = new List<GridCase>();
        for (int i = 0; i < spaceAngleSize; i++) {
            grid.Add (new GridCase());
        }
    }

    public void FreeSpaceAtIndex(int index){
        grid[index].free = true;
    }

    public bool HaveSpace() {
        for (int i = 0; i < grid.Count; i++) {
            if (grid[i].free == true) {
                return true;
            }
        }

        return false;
    }

    public int FindSpace(){
        int angleIndex;
        do {
            angleIndex = UnityEngine.Random.Range (0, spaceAngleSize);
        } while (grid[angleIndex].free == false);

        grid[angleIndex].free = false;

        return angleIndex;
    }
}
