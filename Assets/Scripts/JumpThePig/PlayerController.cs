using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 2D プレイヤーの左右移動 & ジャンプを制御するスクリプト
/// 自動生成された PlayerInput クラスを使って実装
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("操作設定")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    [Header("地面判定")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rb;
    private PlayerInput _inputActions;       // 自動生成された PlayerInput クラス
    private float _currentSpeed;            // 現在の速度
    private bool _isGrounded = false;        // 地面判定


    private Vector2 _moveInput;             // 入力値

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _currentSpeed = _moveSpeed;
    }


    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance.GameState == GameStateManager.GameStateName.GAME)
        {
            // 左右移動
            Vector2 velocity = _rb.velocity;
            velocity.x = _moveInput.x * _currentSpeed;
            _rb.velocity = velocity;
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameStateManager.Instance.GameState != GameStateManager.GameStateName.GAME) return;

        _moveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    /// <param name="context"></param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameStateManager.Instance.GameState != GameStateManager.GameStateName.GAME) return;

        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }

    // 地面に接しているかを判定する
    private void CheckGround()
    {
        if (_groundCheck != null)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }
        else
        {
            _isGrounded = false;
        }
    }

    // 地面チェック用 Gizmo 表示（シーンビュー）
    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
