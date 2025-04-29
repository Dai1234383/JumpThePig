using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �^�C�g���V�[���ւ̑J��
    /// </summary>
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("title");
    }

    /// <summary>
    /// �Q�[���V�[���ւ̑J��
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// ���U���g�V�[���ւ̑J��
    /// </summary>
    public void LoadResultScene()
    {
        SceneManager.LoadScene("Result");
    }
}
