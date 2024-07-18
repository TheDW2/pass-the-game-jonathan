using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If an enemy reaches this the game ends
public class EnemyGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBehavior>() == null) return;

        GameManger.instance.QueueNotif($"You lose\n Final Score {ScoreManager.instance.GetKills()}");
        Invoke("RestartGame", 3);
    }

    void RestartGame()
    {
        GameManger.instance.Restart();
    }
}
