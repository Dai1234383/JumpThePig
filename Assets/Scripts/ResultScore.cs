using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ResultScore : MonoBehaviour
{
    public static ResultScore Instance;

    [Header("���݂̃X�R�A��\������Text")]
    [SerializeField] private TextMeshProUGUI _gameScore;

    [Header("�����L���O�X�R�A�\���pText�i5�j")]
    [SerializeField] private TextMeshProUGUI[] _scoreTexts;

    [Header("1�ʍX�V���ɕ\������Text�i�C�ӂŔ�\���ɂ��Ă����j")]
    [SerializeField] private TextMeshProUGUI _newRecordText;

    private const int MaxRanking = 5; // �\�����郉���L���O��
    private List<int> _scoreList = new List<int>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadScores();          // �ۑ��ς݃X�R�A��ǂݍ���
        AddCurrentScore();     // ����̃X�R�A�������L���O�ɒǉ�
        SaveScores();          // �X�V��̃����L���O��ۑ�
        DisplayScores();       // UI�ɃX�R�A��\��
    }

    /// <summary>
    /// PlayerPrefs�ɕۑ����ꂽ�ߋ��̃X�R�A��ǂݍ��݂܂��B
    /// </summary>
    private void LoadScores()
    {
        _scoreList.Clear();
        for (int i = 0; i < MaxRanking; i++)
        {
            int score = PlayerPrefs.GetInt($"HighScore{i}", 0);
            _scoreList.Add(score);
        }
    }

    /// <summary>
    /// ���݂̃X�R�A�����X�g�ɒǉ����A���5���ɍi��܂��B
    /// �܂��ANew Record�Ȃ炻�̃e�L�X�g��\�����܂��B
    /// </summary>
    private void AddCurrentScore()
    {
        int currentScore = ScoreManager.Instance.MaxScore;
        _scoreList.Add(currentScore);

        // �~���ɕ��ׂď��5�����擾
        _scoreList = _scoreList.OrderByDescending(s => s).Take(MaxRanking).ToList();

        // �X�R�A�\��
        _gameScore.text = $"score: {currentScore} m";

        // New Record �̃`�F�b�N�i���݂̃X�R�A��1�ʂƓ������Ȃ�\���j
        if (_scoreList[0] == currentScore && _newRecordText != null)
        {
            _newRecordText.gameObject.SetActive(true);
            _newRecordText.text = "New Record!";
        }
        else if (_newRecordText != null)
        {
            _newRecordText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �����L���O�X�R�A��UI�ɕ\�����܂��B
    /// </summary>
    private void DisplayScores()
    {
        for (int i = 0; i < _scoreTexts.Length; i++)
        {
            if (i < _scoreList.Count)
            {
                _scoreTexts[i].text = $"No.{i + 1} : {_scoreList[i]} m";
            }
            else
            {
                _scoreTexts[i].text = $"No.{i + 1} : 0m";
            }
        }
    }

    /// <summary>
    /// ���݂̃����L���O�f�[�^��ۑ����܂��B
    /// </summary>
    private void SaveScores()
    {
        for (int i = 0; i < _scoreList.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", _scoreList[i]);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// �����L���O�����Z�b�g���郁�\�b�h�iUI�{�^���Ȃǂ���Ăяo���j
    /// </summary>
    public void ResetScores()
    {
        for (int i = 0; i < MaxRanking; i++)
        {
            PlayerPrefs.DeleteKey($"HighScore{i}");
        }

        PlayerPrefs.Save();

        _scoreList.Clear();
        for (int i = 0; i < _scoreTexts.Length; i++)
        {
            _scoreTexts[i].text = $"No.{i + 1} : 0m";
        }

        if (_newRecordText != null)
        {
            _newRecordText.gameObject.SetActive(false);
        }
    }
}
