using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitFromPlayerDetector : MonoBehaviour
{
    public bool AccurateCalculation;

    public event Action PlayerHitsFromSide;
    public event Action PlayerHitsFromAbove;
    public event Action PlayerHitsFromBelow;

    string _playerTag = "Player";

    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_playerTag))
        {
            float thisX = this.transform.position.x;

            float lowerXbound;
            float higherXbound;

            if (AccurateCalculation)
            {
                float halfSize = this.transform.lossyScale.x / 2;
                lowerXbound = thisX - halfSize;
                higherXbound = thisX + halfSize;
            }
            else
            {
                float fullSize = this.transform.lossyScale.x;
                lowerXbound = thisX - fullSize;
                higherXbound = thisX + fullSize;
            }

            var player = collision.gameObject;
            float playerX = player.transform.position.x;

            bool playerHitsInXBounds = lowerXbound < playerX && playerX < higherXbound;

            if (!playerHitsInXBounds)
            {
                PlayerHitsFromSide?.Invoke();
            }
            else/* if (playerHitsInXBounds)*/
            {
                float playerY = player.transform.position.y;
                bool playerHitsFromBelow = playerY < this.transform.position.y;

                if (playerHitsFromBelow)
                    PlayerHitsFromBelow?.Invoke();
                else
                    PlayerHitsFromAbove?.Invoke();
            }
        }
    }
}