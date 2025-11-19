using UnityEngine;
using TMPro;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 플레이씬의 오프닝 연출
    /// </summary>
    public class AOpening : MonoBehaviour
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
        private string sequence01 = "I need to get out of here";

        #endregion

        #region Unity Event Method
        void Start ()
        {
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

            //페이드인 연출 (1초 후 페이드인 효과) - 2초
            fader.FadeStart(2f);

            //시나리오 텍스트 화면 출력
            sequenceText.text = sequence01;

            //3초 후 텍스트 지움
            yield return new WaitForSeconds(3f);
            sequenceText.text = "";

            //플레이 캐릭터 활성화
            player.SetActive(true);

        }

        #endregion
    }
}
