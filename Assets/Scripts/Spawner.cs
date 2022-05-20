using UnityEngine;

public class Spawner : MonoBehaviour
{
    public BoxCollider2D playZone;
    private Vector3Int maxSpawnPos;
    private Vector3Int minSpawnPos;
    public GameObject fruitPrefab;

    private void Start()
    {
        var tempMaxSpawnPos = playZone.bounds.max;
        var tempMinSpawnPos = playZone.bounds.min;
        maxSpawnPos = Vector3Int.FloorToInt(tempMaxSpawnPos);
        minSpawnPos = Vector3Int.FloorToInt(tempMinSpawnPos);
        SpawnFruit();
    }

    public void SpawnFruit()
    {
        var x = Random.Range(minSpawnPos.x, maxSpawnPos.x);
        var y = Random.Range(minSpawnPos.y, maxSpawnPos.y);
        Instantiate(fruitPrefab, new Vector3Int(x, y, 0), Quaternion.identity);
    }
}
