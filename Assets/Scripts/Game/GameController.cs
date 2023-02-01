using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   [SerializeField] private float _sceneChangeDelay;

   private void Awake()
   {
      Application.targetFrameRate = 60;
   }

   [UsedImplicitly]// вызывается по ивенту смерти игрока
   public void OnPlayerDied()
   {
      StartCoroutine(ShowGameOver());
   }

   private IEnumerator ShowGameOver()
   {
      yield return new WaitForSeconds(_sceneChangeDelay);// задержка , чтобы успели проиграться все анимации. 
      SceneManager.LoadSceneAsync(GlobalConstants.GAME_OVER_SCENE);
   }
}
