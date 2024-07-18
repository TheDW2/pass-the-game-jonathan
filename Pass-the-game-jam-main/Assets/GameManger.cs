using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// it was 4 am and I totally misspelled this godspeed
public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    public TextMeshProUGUI notification;
    Queue<string> notifs = new Queue<string>();

    public PlayerMovement player;

    public float difficulty = 1;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        notification = GameObject.Find("Notification").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        QueueNotif("Feed the Mob (the bad one)");
        QueueNotif("Using these supplies");
        QueueNotif("To get upgrades");
        QueueNotif("To fight the Mob (the worse one)");
        QueueNotif("Go");
    }

    public void QueueNotif(string notif)
    {
        notifs.Enqueue(notif);
        if (notifs.Count != 1) return;
        Invoke("NextNotif", 0);
    }

    void NextNotif()
    {
        notification.text = notifs.Dequeue();

        if (notifs.Count > 0)
        {
            Invoke("NextNotif", 1.5f);
        }
        else
        {
            Invoke("ClearNotif", 1.5f);
        }
    }

    void ClearNotif()
    {
        if (notifs.Count > 0) return;
        notification.text = "";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LockPlayer()
    {
        player.enabled = !player.enabled;
        player._playerShooting.enabled = !player._playerShooting.enabled;
        player._rigid.constraints = (player.enabled) ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
    }
}
