using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineController : MonoBehaviour
{
    [SerializeField] private float _riseSpeed = 1.5f;
    [SerializeField] private string _playerTag = "Player";

    private void Update()
    {
        if (GameStateManager.Instance.GameState == GameStateManager.GameStateName.GAME)
        {
            // ã•ûŒü‚ÉˆÚ“®
            transform.position += Vector3.up * _riseSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_playerTag))
        {
            GameStateManager.Instance.GameOver();
        }
    }
}
