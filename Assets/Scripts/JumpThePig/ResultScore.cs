using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ResultScore : MonoBehaviour
{
    public static ResultScore Instance;

    [Header("現在のスコアを表示するText")]
    [SerializeField] private TextMeshProUGUI _gameScore;

    [Header("ランキングスコア表示用Text（5つ）")]
    [SerializeField] private TextMeshProUGUI[] _scoreTexts;

    [Header("1位更新時に表示するText（任意で非表示にしておく）")]
    [SerializeField] private TextMeshProUGUI _newRecordText;

    private const int MaxRanking = 5; // 表示するランキング数
    private List<int> _scoreList = new List<int>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadScores();          // 保存済みスコアを読み込み
        AddCurrentScore();     // 今回のスコアをランキングに追加
        SaveScores();          // 更新後のランキングを保存
        DisplayScores();       // UIにスコアを表示
    }

    /// <summary>
    /// PlayerPrefsに保存された過去のスコアを読み込みます。
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
    /// 現在のスコアをリストに追加し、上位5件に絞ります。
    /// また、New Recordならそのテキストを表示します。
    /// </summary>
    private void AddCurrentScore()
    {
        int currentScore = ScoreManager.Instance.MaxScore;
        _scoreList.Add(currentScore);

        // 降順に並べて上位5件を取得
        _scoreList = _scoreList.OrderByDescending(s => s).Take(MaxRanking).ToList();

        // スコア表示
        _gameScore.text = $"score: {currentScore} m";

        // New Record のチェック（現在のスコアが1位と等しいなら表示）
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
    /// ランキングスコアをUIに表示します。
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
    /// 現在のランキングデータを保存します。
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
    /// ランキングをリセットするメソッド（UIボタンなどから呼び出し）
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
