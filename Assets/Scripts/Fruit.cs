using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Spawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SnakeController>())
        {
            var col = collision.GetComponent<SnakeController>();
            col.EatFruit();
            spawner.SpawnFruit();
            Destroy(this.gameObject);
        }
    }
}
