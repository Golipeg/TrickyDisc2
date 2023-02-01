using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreLabel;
    [SerializeField] private TextMeshProUGUI _bestScoreLabel;
    [SerializeField] private float _newBestScoreAniimationDuration;
    [SerializeField] private AudioSource _bestScoreChangedAudio;


    private void Awake()
    {
        var currentScore = PlayerPrefs.GetInt(GlobalConstants.SCORE_PREFS_KEY);
        var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            ShowNewBestScoreAnimation();
            SaveNewBestScore(bestScore);
        }

        _bestScoreLabel.text = $"BEST:{bestScore.ToString()}";
        _currentScoreLabel.text = currentScore.ToString();
    }

    [UsedImplicitly]
    public void RestartGame() //вызывается при нажатии на кнопку рестарт 
    {
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
    }

    [UsedImplicitly]
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void SaveNewBestScore(int bestScore)
    {
        PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, bestScore);
        PlayerPrefs.Save();
    }

    private void ShowNewBestScoreAnimation()
    {
        _bestScoreLabel.transform.DOPunchScale(Vector3.one, _newBestScoreAniimationDuration, 0);
        _bestScoreChangedAudio.Play();
    }
}