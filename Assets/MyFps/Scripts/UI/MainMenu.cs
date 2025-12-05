using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

        //UI
        public GameObject mainMenuUI;
        public GameObject optionUI;
        public GameObject creditUI;

        public GameObject loadGameButton;

        //옵션 - 볼륨관리
        public AudioMixer audioMixer;

        //슬라이더
        public Slider bgmSlider;
        public Slider sfxSlider;

        //AudioMixer, PlayerPrefs 파라미터
        private const string BgmVolume = "BgmVolume";
        private const string SfxVolume = "SfxVolume";
        private const string SceneNumber = "SceneNumber";

        //씬 번호
        private int sceneNumber = -1;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //저장 데이터 불러와서 게임 데이터 초기화
            GameDataInit();

            //로드게임 버튼 세팅
            if(PlayerStats.Instance.SceneNumber < 0)
            {
                loadGameButton.SetActive(false);
            }
            else
            {
                loadGameButton.SetActive(true);
            }

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

            fader.FadeTo(loadToScene);
        }

        public void LoadGame()
        {
            //버튼 효과음
            AudioManager.Instance.Play("ButtonHit");
            Debug.Log("LoadGame버튼 클릭");

            fader.FadeTo(PlayerStats.Instance.SceneNumber);
        }

        public void Option()
        {
            ShowOptionUI();
        }

        public void Credits()
        {            
            Debug.Log("Credits버튼 클릭");

            ShowCreditUI();
        }

        public void QuitGame()
        {
            //치팅: 저장된 데이터 리셋
            PlayerPrefs.DeleteAll();

            Application.Quit();                 //어플리케이션 종료 명령/ 에디터에서는 명령 무시, 실제 파일에서는 명령 실행
        }

        private void ShowOptionUI()
        {
            mainMenuUI.SetActive(false);
            optionUI.SetActive(true);
        }

        public void HideOptionUI()
        {
            //옵션 데이터 저장
            SaveOptions();

            //버튼 효과음
            AudioManager.Instance.Play("ButtonHit");

            //UI
            mainMenuUI.SetActive(true);
            optionUI.SetActive(false);
        }

        //옵션 배경음 볼륨 변경시 호출
        public void SetBgmVolume(float value)
        {
            //value값 저장
            //PlayerPrefs.SetFloat(BgmVolume, value);

            //믹서 적용
            audioMixer.SetFloat(BgmVolume, value);
        }

        //옵션 효과음 볼륨 변경시 호출
        public void SetSfxVolume(float value)
        {
            //value값 저장
            //PlayerPrefs.SetFloat(SfxVolume, value);

            //믹서 적용
            audioMixer.SetFloat(SfxVolume, value);
        }

        //옵션 데이터 저장하기
        private void SaveOptions()
        {
            Debug.Log("Save Option Data");

            //볼륨
            PlayerPrefs.SetFloat(BgmVolume, bgmSlider.value);
            PlayerPrefs.SetFloat(SfxVolume, sfxSlider.value);

            //기타 옵션값
            //....

        }

        //옵션 데이터 불러오기
        public void LoadOptions()
        {
            Debug.Log("Load Option Data");

            //볼륨값
            float bgmVolume = PlayerPrefs.GetFloat(BgmVolume, 0f);
            audioMixer.SetFloat(BgmVolume, bgmVolume);                  //믹서 적용
            bgmSlider.value = bgmVolume;                                //UI 적용

            float sfxVolume = PlayerPrefs.GetFloat(SfxVolume, 0f);
            audioMixer.SetFloat(SfxVolume, sfxVolume);                  //믹서 적용
            sfxSlider.value = sfxVolume;                                //UI 적용

            //기타 옵션값
            //....
        }

        //크레딧 
        private void ShowCreditUI()
        {
            mainMenuUI.SetActive(false);
            creditUI.SetActive(true);
        }

        private void GameDataInit()
        {
            //옵션 데이터
            LoadOptions();

            //플레이 데이터
            //sceneNumber = PlayerPrefs.GetInt(SceneNumber, -1);
            PlayData playData = SaveLoad.LoadData();

            //PlayerStats의 데이터 초기화
            PlayerStats.Instance.PlayerStatsInitialize(playData);
        }
        #endregion
    }
}
