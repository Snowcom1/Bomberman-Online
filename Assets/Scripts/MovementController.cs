using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;



    public AnimateSpriteRenderer srUp;
    public AnimateSpriteRenderer srDown;
    public AnimateSpriteRenderer srLeft;
    public AnimateSpriteRenderer srRight;
    public AnimateSpriteRenderer srDeath;
    private AnimateSpriteRenderer activeSr;

     // public JoystickMovement movementJoystick;


    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        moveUp = false;
        moveDown = false;
        moveRight = false;
        moveLeft = false;

        activeSr = srDown;
    }

    // Update is called once per frame
    void Update()
    {
       KeyBoardMovement();
    }

     private void KeyBoardMovement()
     {
          if(Input.GetKey(inputUp) || moveUp)
       {
            SetDirection(Vector2.up, srUp);
       }
       else if(Input.GetKey(inputDown) || moveDown)
       {
            SetDirection(Vector2.down, srDown);

       }
       else if(Input.GetKey(inputLeft) || moveLeft)
       {
            SetDirection(Vector2.left, srLeft);

       }
       else if(Input.GetKey(inputRight) || moveRight)
       {
            SetDirection(Vector2.right, srRight);

       }
       else if(Input.GetKey("escape"))
       {
          Application.Quit();
       }
       else
       {
            SetDirection(Vector2.zero, activeSr);
       }
     }

    private void FixedUpdate() {
        Vector2 position = rigidBody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidBody.MovePosition(position+translation);
    }

    public void moveUpDown() {moveUp = true;}
    public void moveUpUp() {moveUp = false;}
    public void moveDownDown() {moveDown = true;}
    public void moveDownUp() {moveDown = false;}
    public void moveLeftDown() {moveLeft = true;}
    public void moveLeftUp() {moveLeft = false;}
    public void moveRightDown() {moveRight = true;}
    public void moveRightUp() {moveRight = false;}



    public void SetDirection(Vector2 newDirection, AnimateSpriteRenderer sr)
    {
        direction = newDirection;

        srUp.enabled = sr == srUp;
        srDown.enabled = sr == srDown;
        srLeft.enabled = sr == srLeft;
        srRight.enabled = sr == srRight;
        
        activeSr = sr;
        activeSr.idle = direction == Vector2.zero;

    }

    private void OnTriggerEnter2D(Collider2D other) {
          if(other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
               PlayerDeath();
          }
    }

    private void PlayerDeath() {
          enabled = false;

          srUp.enabled = false;
          srDown.enabled = false;
          srLeft.enabled = false;
          srRight.enabled = false;
          srDeath.enabled = true;  

          Invoke(nameof(whenDying), 1.25f);
    }

    private void whenDying()
    {
          gameObject.SetActive(false);
          FindObjectOfType<GameManager>().checkWinners();
    }
}
