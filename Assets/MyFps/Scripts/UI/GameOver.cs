using TMPro;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 게임오버 UI 처리
    /// 플레이씬 다시하기, 메인메유 가기
    /// </summary>
    public class GameOver : MonoBehaviour
    {
        #region Variables
        //씬 이동
        public SceneFader fader;
        [SerializeField]
        private string backToScene = "PlayScene01";
        [SerializeField]
        private string loadToScene = "MainMenu";

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //마우스 커서 초기화(UI 화면에서)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //페이드 인
            fader.FadeStart();
        }

        #endregion

        #region Custom Method
        //메인메뉴 버튼을 눌렀을 때 호출
        public void Retry()
        {
            fader.FadeTo(backToScene);
        }

        public void MainMenu()
        {
            Debug.Log("Go to MainMenu");
            fader.FadeTo(loadToScene);
        }

        #endregion
    }
}
