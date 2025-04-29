using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// タイトルシーンへの遷移
    /// </summary>
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("title");
    }

    /// <summary>
    /// ゲームシーンへの遷移
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// リザルトシーンへの遷移
    /// </summary>
    public void LoadResultScene()
    {
        SceneManager.LoadScene("Result");
    }
}
