using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class DExitTrigger : MonoBehaviour
    {
        #region Variables
        //참조: 충돌체
        private BoxCollider collider;

        //시퀀스
        public Door door;

        //사운드
        public AudioSource bgm02;

        //씬 이동
        public SceneFader fader;
        private string loadToScene = "NextScene";

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SequencePlay());

            //충돌체 비활성화(또는 킬)
            collider.enabled = false;
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            //문 열기
            door.Activate();

            bgm02.Stop();

            //씬 종료시 구현 내용
            //....

            yield return new WaitForSeconds(1f);

            //fader.FadeTo(loadToScene);
            Debug.Log($"Go to {loadToScene}");
        }
        #endregion
    }
}
