using TMPro;
using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 첫번째 트리거에 걸리면 트리거 시퀀스 실행
    /// </summary>
    public class BFirstTrigger : MonoBehaviour
    {
        #region Variables
        //충돌체
        private BoxCollider collider;

        //시퀀스
        //플레이어
        public GameObject player;

        //시퀀스 UI
        public TextMeshProUGUI sequenceText;
        [SerializeField]
        private string sequence = "Looks like a weapon on that table";

        //화살표 오브젝트
        public GameObject arrow;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //시퀀스 플레이
            StartCoroutine(TriggerPlay());

            //충돌체 비활성화(또는 킬)
            collider.enabled = false;
        }
        #endregion

        #region Custom Method
        IEnumerator TriggerPlay()
        {
            //캐릭터 비활성화
            player.SetActive(false);

            //시나리오 텍스트 화면 출력, 1초 딜레이
            sequenceText.text = sequence;
            yield return new WaitForSeconds(2f);

            //화살표 활성화, 1초 딜레이
            arrow.SetActive(true);
            yield return new WaitForSeconds(1f);

            //대사 초기화
            sequenceText.text = "";

            //플레이 캐릭터 활성화
            player.SetActive(true);


        }
        #endregion

    }
}
