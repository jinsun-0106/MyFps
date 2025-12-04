using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    /// <summary>
    /// 플레이씬02의 오프닝 연출
    /// 페이드인 효과, 배경음 플레이, 시퀀스 텍스트 초기화
    /// </summary>
    public class EOpening : MonoBehaviour
    {
        #region Variables
        //페이더 효과
        public SceneFader fader;

        //플레이어
        public GameObject player;

        //시퀀스 텍스트
        public TextMeshProUGUI sequenceText;

        //시나리오 텍스트
        [SerializeField]
        private string sequence04 = "What was that..?";

        //PlayerPrefs 파라미터
        private const string SceneNumber = "SceneNumber";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //시작하자마자 데이터 저장
            SaveData();

            //시작하자마자 오프닝 연출
            StartCoroutine(SequencePlay());

        }
        #endregion

        #region Custom Method
        //오프닝 시퀀스 연출
        IEnumerator SequencePlay()
        {
            //캐릭터 비활성화
            player.SetActive(false);

            //페이드인 연출 (1초 후 페이드인 효과) - 1초
            fader.FadeStart();

            //시나리오 텍스트 없어짐
            sequenceText.text = "";

            //배경음 플레이
            AudioManager.Instance.PlayBGM("Bgm01");

            yield return new WaitForSeconds(1f);

            //플레이 캐릭터 활성화
            player.SetActive(true);


        }

        //데이터 저장하기
        private void SaveData()
        {
            //저장된 번호 가져오기
            int saveNumber = PlayerPrefs.GetInt(SceneNumber, -1);

            //씬 번호 저장
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;

            if (saveNumber < sceneNumber)
            {
                //저장
                PlayerPrefs.SetInt(SceneNumber, sceneNumber);
                SaveLoad.SaveData();
            }
        }

        #endregion
    }
}
