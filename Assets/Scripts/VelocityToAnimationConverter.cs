using System.Collections;
using UnityEngine;

public class VelocityToAnimationConverter : MonoBehaviour
{
    enum AnimState
    {
        UNDEFINED, Idle, Run, Jump, Fall
    }

    public Rigidbody2D Rigidbody;
    public Animator Animator;
    public Transform BodyToRotate;

    [Range(0.01f, 5f)]
    public float UpdateEveryXSeconds = 0.1f;
    [Range(0f, 50f)]
    public float PoisitveYVelocityTreshold;
    [Range(-50f, 0f)]
    public float NegativeYVelocityTreshold;

    AnimState _currentState;
    bool _bodyFlipped;
    WaitForSeconds _updateWait;

    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        _currentState = AnimState.UNDEFINED;
        _bodyFlipped = false;
        _updateWait = new WaitForSeconds(UpdateEveryXSeconds);

        StartCoroutine(UpdateAnimation());
    }

    IEnumerator UpdateAnimation()
    {
        while (true)
        {
            AnimState newState = AnimState.UNDEFINED;

            if (Rigidbody.velocity == Vector2.zero)
            {
                newState = AnimState.Idle;
            }
            else
            {
                //Check if we need to turn body left/right
                float velocityX = Rigidbody.velocity.x;

                if (velocityX > 0 && _bodyFlipped)
                {
                    BodyToRotate.rotation = Quaternion.identity;
                    _bodyFlipped = false;
                }
                else if (velocityX < 0 && !_bodyFlipped)
                {
                    BodyToRotate.rotation = Quaternion.Euler(0f, 180f, 0f);
                    _bodyFlipped = true;
                }

                //Are we jumping, falling or just running/staying?
                float velocityY = Rigidbody.velocity.y;

                if (velocityY > PoisitveYVelocityTreshold)
                    newState = AnimState.Jump;
                else if (velocityY < NegativeYVelocityTreshold)
                    newState = AnimState.Fall;
                else
                    newState = velocityX != 0 ? AnimState.Run : AnimState.Idle;
            }

            if (newState != _currentState)
            {
                Animator.SetTrigger(newState.ToString());
                _currentState = newState;
            }

            yield return _updateWait;
        }
    }
}