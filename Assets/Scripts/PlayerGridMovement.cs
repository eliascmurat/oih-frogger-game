using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    public static PlayerGridMovement instance;
    
    private Vector3 _initialPosition;
    
    [SerializeField] private Transform movePoint;
    [SerializeField] private GameObject gameOverScreen;

    [Header("Speed")]
    [SerializeField] private float speed = 5;
    
    [Header("Grid")]
    [SerializeField] private float horizontalGridSize = 3f;
    [SerializeField] private float verticalGridSize = 2.2f;
    
    [Header("Boundaries")]
    [SerializeField] private float maxX = 9f;
    [SerializeField] private float minX = -9f;
    [SerializeField] private float minY = -4.25f;

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
        _initialPosition = transform.position;
        movePoint.parent = null;
    }

    private void Update()
    {
        HandleInputMovement();
    }

    private void HandleInputMovement()
    {
        var movementAmount = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movementAmount);

        if (!(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)) return;

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontalInput) == 1f)
            Move(new Vector3(horizontalInput, 0, 0), horizontalGridSize);
        else if (Mathf.Abs(verticalInput) == 1f)
            Move(new Vector3(0, verticalInput, 0), verticalGridSize);
        
        ClampPosition();
    }

    private void ClampPosition()
    {
        var position = movePoint.position;
        var clampedX = Mathf.Clamp(position.x, minX, maxX);
        var clampedY = Mathf.Clamp(position.y, minY, float.MaxValue);
        
        position = new Vector3(clampedX, clampedY, position.z);
        movePoint.position = position;
    }

    private void Move(Vector3 direction, float gridSize)
    {
        var newPosition = movePoint.position + direction * gridSize;
        movePoint.position = newPosition;
    }

    public void ResetPlayerPosition()
    {
        transform.position = _initialPosition;
        movePoint.position = _initialPosition;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        gameOverScreen.SetActive(true);
        enabled = false;
        EnemyManager.instance.enabled = false;
    }
}