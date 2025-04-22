using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _gameText;     //�J�E���g�_�E���Ȃǂ̃e�L�X�g
    [SerializeField] private Button _toResultButton;        //���U���g�ɍs���{�^��
    [SerializeField] private TextMeshProUGUI _scoreText;    //�X�R�A�\���̃e�L�X�g

    private void Awake()
    {
        Instance = this;
    }

    private async void Start()
    {
        _gameText.text = ("3");
        await UniTask.Delay(1000);
        _gameText.text = ("2");
        await UniTask.Delay(1000);
        _gameText.text = ("1");
        await UniTask.Delay(1000);
        _gameText.text = ("GameStart!!");
        await UniTask.Delay(1000);
        _gameText.gameObject.SetActive(false);
    }
    private void Update()
    {
        UpdateScore();
    }

    /// <summary>
    /// �X�R�A�̍X�V
    /// </summary>
    private void UpdateScore()
    {
        int score = ScoreManager.Instance.MaxScore;
        string scoreText=score.ToString();
        _scoreText.text = ("score:"+scoreText+" m");
    }


    /// <summary>
    /// �Q�[���I�[�o�[����UI��ύX
    /// </summary>
    public void GameOverUI()
    {
        //�Z�b�g�A�N�e�B�u��؂�ւ�
        _toResultButton.gameObject.SetActive(true);
        _gameText.gameObject.SetActive(true);
        //�e�L�X�g��ύX
        _gameText.text = ("GameOver!!");
    }

}
