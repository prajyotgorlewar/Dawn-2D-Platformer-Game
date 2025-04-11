using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for OnPointerDown and OnPointerUp

namespace ClearSky
{
    public class DemoCollegeStudentController : MonoBehaviour
    {
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;
        public float jumpPower = 20f; // Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        private bool isJumping = false;
        private bool alive = true;
        private bool isKickboard = false;

        // Movement flags
        private bool moveLeft = false;
        private bool moveRight = false;

        // UI button references
        public GameObject leftButton;
        public GameObject rightButton;
        public GameObject jumpButton;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            // Add event listeners for button press (OnPointerDown) and button release (OnPointerUp)
            EventTrigger leftTrigger = leftButton.AddComponent<EventTrigger>();
            AddEventTrigger(leftTrigger, EventTriggerType.PointerDown, (eventData) => { MoveLeft(true); });
            AddEventTrigger(leftTrigger, EventTriggerType.PointerUp, (eventData) => { MoveLeft(false); });

            EventTrigger rightTrigger = rightButton.AddComponent<EventTrigger>();
            AddEventTrigger(rightTrigger, EventTriggerType.PointerDown, (eventData) => { MoveRight(true); });
            AddEventTrigger(rightTrigger, EventTriggerType.PointerUp, (eventData) => { MoveRight(false); });

            EventTrigger jumpTrigger = jumpButton.AddComponent<EventTrigger>();
            AddEventTrigger(jumpTrigger, EventTriggerType.PointerDown, (eventData) => { Jump(); });
        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                Hurt();
                Die();
                KickBoard();
                Run();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }

        void KickBoard()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && isKickboard)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !isKickboard)
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }
        }

        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);

            if (moveLeft && !isKickboard)
            {
                direction = -1;
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }
            else if (moveRight && !isKickboard)
            {
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }

            if (!isKickboard)
            {
                transform.position += moveVelocity * movePower * Time.deltaTime;
            }
            else if (isKickboard)
            {
                transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
            }
        }

        void Jump()
        {
            if (!anim.GetBool("isJump") && !isJumping)
            {
                isJumping = true;
                anim.SetBool("isJump", true);
                rb.linearVelocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isJumping = false;
            }
        }

        void MoveLeft(bool isPressed)
        {
            moveLeft = isPressed;
        }

        void MoveRight(bool isPressed)
        {
            moveRight = isPressed;
        }

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }

        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }

        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("die");
                alive = false;
            }
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("idle");
                alive = true;
            }
        }

        // Helper method to add events for UI buttons
        private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(action));
            trigger.triggers.Add(entry);
        }
    }
}
