using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent Landed;
    public UnityEvent Dead;
    public UnityEvent GetCoin;

    [SerializeField] private float _jumpForce;
    [SerializeField] private ContactFilter2D _platform;   
    
    private Rigidbody2D _body;
    private bool _isOnPlatform => _body.IsTouching(_platform);
    
    public AudioSource DeadSound;
    public AudioSource LandSound;

    void Start()
    {        
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (_isOnPlatform == true /*&& Input.touchCount == 1*/)
        _body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);          
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER ENTER");        
        if (other.gameObject.tag == "Coin")
        {
            Debug.Log("GOT COIN");
            Destroy(other.gameObject);
            GetCoin?.Invoke();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        //if (collisionObject.CompareTag("Platform") || collisionObject.CompareTag("Untagged"))
        //{
        //    LandSound.Play();
        //}

        if (collisionObject.transform.parent != null)
        {
            if (collisionObject.transform.parent.TryGetComponent(out Platform platform))
            {
                platform.StopMovement();
            }
        }
        
        if (collisionObject.CompareTag("Wall"))
        {
            DeadSound.Play();
            Time.timeScale = 0;
            Dead?.Invoke();
        }
        else if (collisionObject.CompareTag("Platform"))
        {
            collisionObject.tag = "Untagged";
            Landed?.Invoke();
        }
    }    
}
