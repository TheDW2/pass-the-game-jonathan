using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Transform particleSpawnPos;

    [SerializeField]
    private Transform bulletSpawnPos;

    [SerializeField]
    private GameObject smokeParticles;

    [SerializeField]
    private GameObject bulletCasing;

    [SerializeField]
    private GameObject playerParent;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _shootClip;

    public event Action OnShoot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.Play("Shoot");
            Instantiate(smokeParticles, particleSpawnPos.position, particleSpawnPos.rotation);
            GameObject casing = Instantiate(bulletCasing, bulletSpawnPos.position, bulletSpawnPos.rotation);
            casing.GetComponent<Rigidbody>().velocity =
                (playerParent.transform.up + playerParent.transform.right - playerParent.transform.forward).normalized;

            _audioSource.PlayOneShot(_shootClip);
            _audioSource.pitch = Random.Range(0.8f, 1.2f);

            OnShoot?.Invoke();
        }
    }
}