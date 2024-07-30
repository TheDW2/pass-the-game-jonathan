using TMPro;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    /// <summary>
    ///     Rotates to face the target
    /// </summary>
    private const float FaceLerpRate = 8;

    //THESE PROPERTIES SHOULD NOT BE DIRECTLY ACCESSED!
    [Header("Speech Properties")]

    [SerializeField]
    private TMP_Text SpeechBubbleText;

    [SerializeField]
    private Animator SpeechBubbleAnim;

    [Header("Other")]

    [SerializeField]
    private Transform Model;

    [SerializeField]
    private SphereCollider triggerCollider;

    [SerializeField]
    private ResourceManager.PreparedFoodTypes[] possibleOrders;

    [SerializeField]
    public float eatingTimer;

    [SerializeField]
    public TMP_Text countdownText;

    // Countdown timer that resets when correct food is delivered
    [SerializeField]
    public int complaintTimer;

    [Header("Audio")]

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _nomClip;

    public float timeRemaining;

    [SerializeField]
    private ParticleSystem particles;

    public bool PlayerNear { get; private set; }

    public StateMachine FSM { get; private set; }
    public static IdleState IdleState { get; } = new();
    public static ConverseState ConverseState { get; private set; } = new();

    public string OrderText => _orderText;

    private Transform _target;
    private int _showHash;
    private string _orderText;
    private ResourceManager.PreparedFoodTypes _order = (ResourceManager.PreparedFoodTypes)(-1);

    private void Awake()
    {
        if (SpeechBubbleAnim)
        {
            _showHash = Animator.StringToHash("Show");
        }

        _orderText = "Hmmmmm";

        FSM = new StateMachine(IdleState);
    }

    private void Update()
    {
        FSM.CurrentState.Update(this);
        Countdown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerNear = true;
        }
        else if (other.tag == "Food")
        {
            Debug.Log("Food Entered" + other.name);

            var preparedFood = other.GetComponent<PreparedFoodComponent>();
            if (preparedFood != null && preparedFood.FoodComponent == _order)
            {
                // Correct food delivered
                Destroy(other.gameObject);
                timeRemaining = complaintTimer;
                ScoreManager.instance.AddScore(1);

                _order = (ResourceManager.PreparedFoodTypes)(-1);

                eatingTimer = 5;
                _orderText = "Om Nom Nom"; // In case the player exits and reenters the talk radius
                ShowMessage(_orderText);
                _audioSource.PlayOneShot(_nomClip);
                particles.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerNear = false;
        }
    }

    /// <summary>
    ///     Opens this npc's speech bubble, and dispays a message.
    /// </summary>
    /// <param name="message">What text to place in the speech bubble</param>
    public void ShowMessage(string message)
    {
        SpeechBubbleAnim.SetBool(_showHash, true);
        SpeechBubbleText.text = message;
    }

    /// <summary>
    ///     Closes this npc's speech bubble.
    /// </summary>
    public void HideMessage()
    {
        SpeechBubbleAnim.SetBool(_showHash, false);
    }

    public void FaceTarget()
    {
        Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation,
            Quaternion.LookRotation(_target.position - Model.transform.position), FaceLerpRate * Time.deltaTime);
    }

    /// <summary>
    ///     Sets the target to the player by searching for gameObjects that are tagged "Player".
    /// </summary>
    public void TargetPlayer()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Countdown()
    {
        if (timeRemaining > 0 && eatingTimer <= 0)
        {
            timeRemaining -= Time.deltaTime;
            countdownText.text = ((int)timeRemaining).ToString();
            if (timeRemaining <= 0)
            {
                timeRemaining = complaintTimer;
                ShowMessage("Too Slow");
            }
        }

        if (eatingTimer > 0)
        {
            eatingTimer -= Time.deltaTime;
            countdownText.text = "";
            if (eatingTimer <= 0)
            {
                // finished eating
                NewOrder();
            }
        }
    }

    private void NewOrder()
    {
        int index = Random.Range(0, possibleOrders.Length);
        _order = possibleOrders[index];

        // in case we have to update text
        _orderText = ResourceManager.Instance.GetPreparedFoodName(_order);
    }
}