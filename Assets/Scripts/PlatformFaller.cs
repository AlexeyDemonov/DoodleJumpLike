using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HitFromPlayerDetector))]
public class PlatformFaller : MonoBehaviour
{
    public float DestroyDelay;

    Rigidbody2D _rigidbody;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        GetComponent<HitFromPlayerDetector>().PlayerHitsFromAbove += HandlePlayerHitFromAbove;
    }

    void HandlePlayerHitFromAbove()
    {
        _animator.SetTrigger("Fall");
        StartCoroutine(DestroyAfterWait());
    }

    IEnumerator DestroyAfterWait()
    {
        yield return new WaitForSeconds(DestroyDelay);

        //Just fall
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}