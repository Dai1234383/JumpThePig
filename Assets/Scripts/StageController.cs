using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject[] _platformPrefabs;     // 生成する足場のPrefab
    [SerializeField] private Transform _playerTransform;        // 生成する足場の位置
    [SerializeField] private float _xRange = 6f;                // 生成する足場の範囲
    [SerializeField] private float _yInterval = 2f;             // 生成する足場の高さの差
    [SerializeField] private int _initialPlatformCount = 10;    // 最初に生成する数
    [SerializeField] private float _spawnBufferHeight = 10f;　　// プレイヤーの上どのくらいで生成するか
    [SerializeField] private Transform _cameraTransform;        // カメラ
    [SerializeField] private float _cameraFollowSpeed = 2f;     // カメラの追従速度

    [SerializeField] private GameObject _Wall;

    private float _highestPlatformY;
    private float _cameraTargetY;

    private void Start()
    {
        // 初期足場生成
        for (int i = 0; i < _initialPlatformCount; i++)
        {
            SpawnPlatform(i * _yInterval);
        }

        _highestPlatformY = _initialPlatformCount * _yInterval;
        _cameraTargetY = _cameraTransform.position.y;
    }

    private void Update()
    {
        // カメラ追従処理
        _cameraTargetY = _playerTransform.position.y;

        Vector3 cameraPos = _cameraTransform.position;
        cameraPos.y = Mathf.Lerp(cameraPos.y, _cameraTargetY, Time.deltaTime * _cameraFollowSpeed);
        _cameraTransform.position = cameraPos;

        // 壁（デッドラインなど）のY位置をプレイヤーに追従
        Vector3 wallPos = _Wall.transform.position;
        wallPos.y = _playerTransform.position.y;
        _Wall.transform.position = wallPos;

        // ステージの自動生成処理も忘れずに！
        SpawnStage();
    }

    private void SpawnStage()
    {
        // 足場生成処理
        float targetY = _playerTransform.position.y + _spawnBufferHeight;

        while (_highestPlatformY < targetY)
        {
            SpawnPlatform(_highestPlatformY);
            _highestPlatformY += _yInterval;
        }
    }



    private void SpawnPlatform(float yPos)
    {
        float xPos = Random.Range(-_xRange, _xRange);
        GameObject prefab = _platformPrefabs[Random.Range(0, _platformPrefabs.Length)];
        Vector3 spawnPos = new Vector3(xPos, yPos, 0);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
