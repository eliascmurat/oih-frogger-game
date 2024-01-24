using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerGridMovement.instance.ResetPlayerPosition();
        ScoreManager.Instance.currentScore++;
    }
}
