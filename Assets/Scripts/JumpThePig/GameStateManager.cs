using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    /// <summary>
    /// �C���Q�[���̏�ԊǗ�
    /// </summary>
    public enum GameStateName
    {
        START,
        GAME,
        CLEAR,
        OVER,
    }


    private GameStateName _gameState;
    public GameStateName GameState => _gameState;

    private void Awake()
    {
        Instance = this;
    }

    private async void Start()
    {
        _gameState = GameStateName.START;

        // 3�b�ҋ@
        await UniTask.Delay(3000);
        GameStart();
    }

    /// <summary>
    /// �Q�[���X�^�[�g����
    /// </summary>
    public void GameStart()
    {
        _gameState = GameStateName.GAME;
    }

    /// <summary>
    /// �Q�[���N���A����
    /// </summary>
    public void GameClear()
    {
        _gameState = GameStateName.CLEAR;
    }

    /// <summary>
    /// �Q�[���I�[�o�[����
    /// </summary>
    public void GameOver()
    {
        UIManager.Instance.GameOverUI();
        _gameState = GameStateName.OVER;
    }
}
