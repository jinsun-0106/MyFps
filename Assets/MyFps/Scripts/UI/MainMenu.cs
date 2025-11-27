using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 메인메뉴 씬을 관리하는 클래스
    /// 메인메뉴 버튼 기능, 신페이더 기능
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "PlayScene01";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //마우스 커서 초기화(UI 화면에서)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //페이드인 시작
            fader.FadeStart();

            //배경음 플레이
            AudioManager.Instance.Play("MenuMusic");
        }

        #endregion

        #region Custom Method
        public void NewGame()
        {
            //버튼 효과음
            AudioManager.Instance.Play("ButtonHit");

            //플레이어 데이터 초기화
            //PlayerStats.Instance.SetWeaponType(WeaponType.None);

            fader.FadeTo(loadToScene);
        }

        public void LoadGame()
        {
            //버튼 효과음
            AudioManager.Instance.Play("ButtonHit");
            Debug.Log("LoadGame버튼 클릭");
        }

        public void Option()
        {
            AudioManager.Instance.PlayBGM("SHAmb");

            Debug.Log("Option버튼 클릭");
        }

        public void Credits()
        {
            
            Debug.Log("Credits버튼 클릭");
        }

        public void QuitGame()
        {
            
            Application.Quit();                 //어플리케이션 종료 명령/ 에디터에서는 명령 무시, 실제 파일에서는 명령 실행
        }
        #endregion
    }
}
