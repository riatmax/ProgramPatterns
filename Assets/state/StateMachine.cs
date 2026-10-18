using TMPro;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDuration = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool grounded;

    private IAlienState current;
    private IdleState idle;
    private WalkingState walking;
    private JumpingState jumping;
    private ShootingState shooting;

    private interface IAlienState
    {
        void Enter(StateMachine alien);
        void Update(StateMachine alien);
        void Exit(StateMachine alien);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        idle = new IdleState();
        walking = new WalkingState();
        jumping = new JumpingState();
        shooting = new ShootingState();

        ChangeState(idle);
    }

    private void Update()
    {
        text.text = "Alien State: " + current.GetType().Name;
        current.Update(this);
    }

    private void ChangeState(IAlienState next)
    {
        current?.Exit(this);
        current = next;
        current.Enter(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
            grounded = true;
    }

    // each state is its own class and has its own Enter, Update, and Exit method that the State Machine calls
    private class IdleState : IAlienState
    {
        public void Enter(StateMachine a) { }

        public void Update(StateMachine a)
        {
            if (Input.GetKeyDown(KeyCode.W))
                a.ChangeState(a.jumping);
            else if (Input.GetKeyDown(KeyCode.Space))
                a.ChangeState(a.shooting);
            else if (Input.GetAxisRaw("Horizontal") != 0)
                a.ChangeState(a.walking);
        }

        public void Exit(StateMachine a) { }
    }

    private class WalkingState : IAlienState
    {
        public void Enter(StateMachine a) { }

        public void Update(StateMachine a)
        {
            float move = Input.GetAxisRaw("Horizontal");
            a.rb.linearVelocity = new Vector2(move * 5f, a.rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.W))
                a.ChangeState(a.jumping);
            else if (Input.GetKeyDown(KeyCode.Space))
                a.ChangeState(a.shooting);
            else if (move == 0)
                a.ChangeState(a.idle);
        }

        public void Exit(StateMachine a) { }
    }

    private class JumpingState : IAlienState
    {
        public void Enter(StateMachine a)
        {
            a.grounded = false;
            a.rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }

        public void Update(StateMachine a)
        {
            float move = Input.GetAxisRaw("Horizontal");
            a.rb.linearVelocity = new Vector2(move * 5f, a.rb.linearVelocity.y);

            if (a.grounded)
                a.ChangeState(move != 0 ? a.walking : a.idle);
        }

        public void Exit(StateMachine a) { }
    }

    private class ShootingState : IAlienState
    {
        private float timer;

        public void Enter(StateMachine a)
        {
            timer = a.shootDuration;
            a.animator.SetTrigger("shoot");
            Instantiate(a.bullet, a.shootPoint.position, Quaternion.identity);
            a.rb.linearVelocity = new Vector2(0f, a.rb.linearVelocity.y);
        }

        public void Update(StateMachine a)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                a.ChangeState(a.idle);
        }

        public void Exit(StateMachine a) { }
    }
}