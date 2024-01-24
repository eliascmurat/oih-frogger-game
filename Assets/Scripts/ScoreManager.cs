using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    public Text scoreText;
    public int currentScore;
    
    private void Awake() 
    { 
        if(Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    private void Update()
    {
        scoreText.text = "SCORE: " + currentScore;
    }
}
