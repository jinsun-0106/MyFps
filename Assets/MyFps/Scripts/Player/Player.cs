using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MyFps
{
    /// <summary>
    /// 플레이어를 관리(제어하는) 클래스
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region Variables
        //게임오버 씬 이동
        [SerializeField] private string loadToScene = "GameOver";
        public SceneFader fader;

        //참조
        private PlayerHealth playerHealth;

        //데미지 이펙트
        public GameObject damageUI;

        //데미지 사운드
        public AudioSource hurt01;
        public AudioSource hurt02;
        public AudioSource hurt03;

        //public CinemachineShake shake;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            playerHealth = GetComponent<PlayerHealth>();

        }

        private void OnEnable()
        {
            //데미지/죽음 이벤트 등록
            playerHealth.onDamage += OnDamage;
            playerHealth.onDie += OnDie;
        }

        private void OnDisable()
        {
            //데미지/죽음 이벤트 제거
            playerHealth.onDamage -= OnDamage;
            playerHealth.onDie -= OnDie;
        }
        #endregion

        #region Custom Method
        //데미지 입을 때 호출되는 함수
        private void OnDamage()
        {
            StartCoroutine(DamageEffect());
        }

        IEnumerator DamageEffect()
        {
            //화면 전체 빨간색 플래쉬 효과
            damageUI.SetActive(true);

            //데미지 사운드 3개 중 1랜덤 발생
            int randNumber = Random.Range(1, 4);
            if(randNumber == 1)
            {
                hurt01.Play();
            }
            else if(randNumber == 2)
            {
                hurt02.Play();
            }
            else if (randNumber == 3)
            {
                hurt03.Play();
            }

            //화면 흔들림 효과

            yield return new WaitForSeconds(1.0f);
            damageUI.SetActive(false);
        }

        //죽었을 때 호출되는 함수
        public void OnDie()
        {
            //게임오버 씬으로 이동
            Debug.Log("게임오버");
            fader.FadeTo(loadToScene);

        }

        #endregion
    }
}
