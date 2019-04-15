public class SpacePartition {

    int spaceAngleSize;
    public int halfSpaceAngleSize;
    bool[] enemyPartitionSpace;

    public SpacePartition() {
        spaceAngleSize = 7;
        halfSpaceAngleSize = spaceAngleSize / 2;
        enemyPartitionSpace = new bool[spaceAngleSize];
        for (int i = 0; i < spaceAngleSize; i++) {
            enemyPartitionSpace.SetValue (true, i);
        }
    }

    public void freeSpaceAtIndex(int index){
        enemyPartitionSpace[index] = true;
    }

    public bool haveSpace() {
        for (int i = 0; i < enemyPartitionSpace.Length; i++) {
            if (enemyPartitionSpace[i] == true) {
                return true;
            }
        }

        return false;
    }

    public int findSpace(){
        int angleIndex;
        do {
            angleIndex = UnityEngine.Random.Range (0, spaceAngleSize);
        } while (enemyPartitionSpace[angleIndex] == false);

        enemyPartitionSpace[angleIndex] = false;

        return angleIndex;
    }
}
