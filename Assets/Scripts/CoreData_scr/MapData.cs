using UnityEngine;

namespace RTank.CoreData
{
    [CreateAssetMenu(menuName = "Map Data", fileName = "New Map Data")]
    public class MapData : ScriptableObject
    {
        [SerializeField] int rows;
        [SerializeField] int columns;

        long obstaclePositions;

        public int Rows => rows;
        public int Columns => columns;
        public int TotalTiles => rows * columns;
        public float MidRow => (rows - 1) * 0.5f;
        public float MidColumn => (columns - 1) * 0.5f;

        public void SetObstaclePositions(long l) => obstaclePositions = l;
        public void UpdateMap(long l) => obstaclePositions |= l;  

        public int GetFlatArray(int x, int z) => x * columns + z;

        public Vector3 GetCoordinate(int index, float yPosition)
        {
            int row = index / columns;
            int column = index % columns;

            return new Vector3(row, yPosition, column);
        }

        public bool IsTileOccupied(Vector3 point)
        {
            int tile = (int)Mathf.Pow(2, GetFlatArray((int)point.x, (int)point.z));

            return (tile & obstaclePositions) != 0;
        }
    }
}