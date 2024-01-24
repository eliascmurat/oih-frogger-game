using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject enemyGameObject;
    
    public float cooldownTimerBase;
    public float cooldownTimer;
    
    public int spawnX;
    public float spawnY;

    private int SpawnLeft { get; set; }
    private int SpawnRight { get; set; }
    
    private float SpawnTopLine { get; set; }
    private float SpawnMidLine { get; set; }
    private float SpawnBottomLine { get; set; }
    
    private List<int> _spawnLocationsX;
    private List<float> _spawnLocationsY;

    private void Awake() 
    { 
        if(instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    private void Start()
    {
        cooldownTimer = cooldownTimerBase;
        
        SpawnLeft = -12;
        SpawnRight = 12;
        
        SpawnTopLine = 2.45f;
        SpawnMidLine = 0.18f;
        SpawnBottomLine = -2.1f;
        
        _spawnLocationsX = new List<int> { SpawnLeft, SpawnRight };
        _spawnLocationsY = new List<float> { SpawnTopLine, SpawnMidLine, SpawnBottomLine };
    }
    
    private void FixedUpdate()
    {
        Cooldown();
    }

    private void Cooldown()
    {
        if (cooldownTimer > 0){
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            SpawnObstacle();
            cooldownTimer = cooldownTimerBase - ScoreManager.Instance.currentScore / 10f;
        }
    }

    private void SpawnObstacle()
    {
        SpawnAtRandomLocation();
        Instantiate(enemyGameObject, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
    }
    
    private void SpawnAtRandomLocation()
    {
        spawnX = _spawnLocationsX[Random.Range(0, _spawnLocationsX.Count)];
        spawnY = _spawnLocationsY[Random.Range(0, _spawnLocationsY.Count)];
    }
}
