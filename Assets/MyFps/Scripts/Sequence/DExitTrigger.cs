using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class DExitTrigger : MonoBehaviour
    {
        #region Variables
        //참조: 충돌체
        private BoxCollider boxCollider;

        //시퀀스
        public Door door;

        //사운드
        public AudioSource bgm02;

        //씬 이동
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "PlayScene02";

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SequencePlay());

            //충돌체 비활성화(또는 킬)
            boxCollider.enabled = false;
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            //문 열기
            door.Activate();

            bgm02.Stop();

            //씬 종료시 구현 내용

            //플레이 데이터 저장
            SavePlayData();

            yield return new WaitForSeconds(1f);

            fader.FadeTo(loadToScene);
        }

        private void SavePlayData()
        {
            //저장할 데이터 세팅
            PlayerStats.Instance.SetSceneName(loadToScene);
            SaveLoad.SaveData();

        }
        #endregion
    }
}
