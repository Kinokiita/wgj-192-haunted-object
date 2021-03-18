using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentController : MonoBehaviour
{
    [SerializeField] FoV fov;
    public GameObject pathObject;
    private List<GameObject> localPathObject = new List<GameObject>();

    [SerializeField] private int curPathPointIndex;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private Vector3 newVector;

    [SerializeField] private float Timer;

    [SerializeField] public float movementSpeed = 2;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isIdle;
    //[SerializeField] private bool isStuned;
    [SerializeField] private bool isFacingRight;

    private float verticalMove;
    private float horizontalMove;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsStuned = Animator.StringToHash("isStuned");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    private static readonly int Vertical = Animator.StringToHash("vertical");
    private static readonly int Horizontal = Animator.StringToHash("horizontal");

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private Vector3 _velocity = Vector3.zero;

    void Start()
    {

    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        
        for (int i = 0; i < pathObject.transform.childCount; i++)
        {
            localPathObject.Add(pathObject.transform.GetChild(i).gameObject);
        }

        if (localPathObject.Count > 0)
        {
            curPathPointIndex = 0;
            oldPosition = transform.position;
            newPosition = localPathObject[curPathPointIndex].transform.position;
        }


    }

    void Update()
    {

        Vector3 aimDir = (newPosition - transform.position).normalized;
        fov.SetAimDirection(aimDir);
        fov.SetOrigin(transform.position);

        if (localPathObject.Count > 0)
        {
            if (transform.position != newPosition)
            {
                sendAgentToVector(oldPosition, newPosition);
            }
            else
            {
                verticalMove = 0;
                horizontalMove = 0;
                oldPosition = transform.position;
                if (localPathObject.Count > curPathPointIndex+1)
                {
                    curPathPointIndex += 1;
                }
                else
                {
                    curPathPointIndex = 0;
                }
                
                newPosition = localPathObject[curPathPointIndex].transform.position;
            }
        }

        updatePlayerFace();
    }

    public void setNewPosition(Vector3 newVector)
    {
        newPosition = newVector;
    }

    private void sendAgentToVector(Vector3 oldV, Vector3 newV)
    {
        transform.position = Vector3.SmoothDamp(oldV, newV, ref _velocity, 0.1f, movementSpeed);
        oldPosition = transform.position;
    }


    private void updatePlayerFace()
    {
        verticalMove = (oldPosition.y == newPosition.y) ? 0 : (oldPosition.y < newPosition.y) ? 1 : -1;
        horizontalMove = (oldPosition.x == newPosition.x) ? 0 : (oldPosition.x < newPosition.x) ? 1 : -1;

        if (Math.Abs(horizontalMove) > 0.1F || Math.Abs(verticalMove) > 0.1F)
        {
            isRunning = true;
            isIdle = false;
            //isStuned = false;
        }
        else
        {
            //isStuned = true;
            isRunning = false;
            isIdle = true;
        }


        if (horizontalMove > 0 && isFacingRight || horizontalMove < 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Flip();
        }

        _animator.SetBool(IsRunning, isRunning);
        _animator.SetBool(IsIdle, isIdle);
        //_animator.SetBool(IsStuned, isStuned);
        _animator.SetFloat(Vertical, verticalMove);
        _animator.SetFloat(Horizontal, horizontalMove);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        var transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
}
