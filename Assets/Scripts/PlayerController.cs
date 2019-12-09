using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GroundChecker[] GroundCheckers;

    Rigidbody2D _rigidbody;
    float _horizontalInput;
    bool _jump;

    bool IsGrounded
    {
        get
        {
            foreach (var checker in GroundCheckers)
            {
                if (checker.IsGrounded)
                    return true;
            }

            /*else*/
            return false;
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _jump == false)
        {
            _jump = true;
        }
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        var newVelocity = new Vector2(_horizontalInput * Speed, _rigidbody.velocity.y);

        if (_rigidbody.velocity != newVelocity)
            _rigidbody.velocity = newVelocity;

        if (_jump)
        {
            _jump = false;

            if (this.IsGrounded)
                _rigidbody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}