using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    public Transform ModelTransform;
    public float RotationSpeed = 8;

    [Header("Movement Settings")]

    public float Acceleration;

    public float Speed;

    [SerializeField]
    public PlayerShooting _playerShooting;

    [SerializeField]
    private float _shootLaunchVel;

    [SerializeField]
    private float _shootVerticalVel;

    public Rigidbody _rigid;
    private CapsuleCollider _col;
    private Vector3 _targetVector;

    int damage = 1;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _playerShooting.OnShoot += OnShoot;
    }

    // Update is called once per frame
    private void Update()
    {
        // collect inputs
        _targetVector.x = Input.GetAxis("Horizontal");
        _targetVector.z = Input.GetAxis("Vertical");

        // point model in direction of velocity
        // sqrMagnitude is a less expensive calculation than magnitude
        if (_rigid.velocity.sqrMagnitude > 0.1f)
        {
            ModelTransform.rotation = Quaternion.Lerp(ModelTransform.rotation, Quaternion.LookRotation(_rigid.velocity),
                RotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // Lerps the velocity to the clamped target, prevents the player from moving faster diagonally
        float velY = _rigid.velocity.y;
        Vector3 vel = _rigid.velocity;
        vel.y = 0;
        vel = Vector3.Lerp(vel, Vector3.ClampMagnitude(_targetVector, 1) * Speed,
            Acceleration * Time.fixedDeltaTime);

        _rigid.velocity = new Vector3(vel.x, velY, vel.z);
    }

    private void OnDestroy()
    {
        _playerShooting.OnShoot -= OnShoot;
    }

    private void OnShoot()
    {
        _rigid.velocity += -ModelTransform.forward * _shootLaunchVel + Vector3.up * _shootVerticalVel;

        RaycastHit hit;
        Debug.DrawRay(ModelTransform.position, ModelTransform.forward * 20, Color.red, 5);
        if (!Physics.Raycast(ModelTransform.position, ModelTransform.forward, out hit, 20, LayerMask.GetMask("Enemy"))) return;

        EnemyBehavior enemy = hit.collider.GetComponent<EnemyBehavior>();
        if (enemy == null) return;

        Debug.Log("hit");
        enemy.DamageEnemy(damage); // scale this with an upgrade or something?
    }

    public void IncreaseDamage(int amount)
    {
        damage += amount;
        _playerShooting.transform.localScale *= 1.02f;
    }
}