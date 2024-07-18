using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Enemy stats scale with difficulty
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    float speed = .2f;

    int health = 5;

    TextMeshProUGUI healthDisplay;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        healthDisplay = GetComponentInChildren<TextMeshProUGUI>();

        //speed = Mathf.Clamp(speed * GameManger.instance.difficulty, 0.2f, 0.5f);
        health = (int)(5 * Mathf.Pow(1.2f, GameManger.instance.difficulty));
        healthDisplay.text = health.ToString();
    }

    void Update()
    {
        controller.Move(transform.right * speed * Time.deltaTime);
    }

    public void DamageEnemy(int damage)
    {
        if (health <= 0) return;
        health -= damage;
        healthDisplay.text = health.ToString();
        if (health > 0) return;

        ScoreManager.instance.AddKill();
        Destroy(gameObject);
    }
}
