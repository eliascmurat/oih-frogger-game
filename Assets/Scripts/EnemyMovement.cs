using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int TimeLife { get; set; }
    public int speed;
    public int minSpeed = 3;
    public int maxSpeed = 7;

    private void Start()
    {
        if (EnemyManager.instance.spawnX > 0)
            speed -= Random.Range(minSpeed, maxSpeed);
        else
            speed = Random.Range(minSpeed, maxSpeed);
        
        TimeLife = 10;
        Destroy(gameObject, TimeLife);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
