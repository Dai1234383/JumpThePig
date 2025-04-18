using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _gameText;
    [SerializeField] private Button _toResultButton;

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

    public void GameOverUI()
    {
        _toResultButton.gameObject.SetActive(true);
        _gameText.gameObject.SetActive(true);
        _gameText.text = ("GameOver!!");
    }
}
