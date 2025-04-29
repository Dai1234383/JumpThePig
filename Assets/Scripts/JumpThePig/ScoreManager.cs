using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Transform _playerTransform;    // �v���C���[�̍��W

    private int _maxScore;        //�ő�X�R�A
    private int _currentScore;    //���݂̃X�R�A

    public int MaxScore => _maxScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _maxScore = 0;
    }
    private void Update()
    {
        AcquireScore();
    }

    /// <summary>
    /// �X�R�A�̌v��
    /// </summary>
    private void AcquireScore()
    {
        // ���݂̃X�R�A���擾
        _currentScore = Mathf.FloorToInt(_playerTransform.position.y + 3);

        // �ō��X�R�A�������݂̃X�R�A��������΍X�V
        if (_maxScore<_currentScore)
        {
            _maxScore=_currentScore;
        }
    }
}
