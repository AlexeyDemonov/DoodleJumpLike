using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerGluer : MonoBehaviour
{
    GameObject _player;

    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject;
            _player.transform.SetParent(this.transform);
        }
    }

    // OnCollisionExit2D is called when this collider2D/rigidbody2D has stopped touching another rigidbody2D/collider2D (2D physics only)
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(_player != null)
        {
            _player.transform.SetParent(null);
            _player = null;
        }
    }
}