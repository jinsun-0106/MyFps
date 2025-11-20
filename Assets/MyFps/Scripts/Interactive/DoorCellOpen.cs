using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace MyFps
{
    /// <summary>
    /// 플레이어와 인터렉션 기능 오브젝트
    /// 인터렉티브 : 마우스를 가져가면 UI 활성화 빼면 UI 비활성화
    /// 인터렉션 기능 : 도어 오픈
    /// </summary>
    public class DoorCellOpen : Interactive
    {
        #region Variables      

        [Header("Interactive Action")]
        //액션
        public Animator animator;        

        //문 여는 소리
        public AudioSource audioSource;

        //애니메이션 파라미터
        const string Open = "Open";
        #endregion

        #region Custom Method

        protected override void DoAction()
        {
            //UI 감추기
            HideActionUI();

            //애니메이션
            animator.SetTrigger(Open);

            //사운드 플레이
            if(audioSource)     //오디오 소스가 널이 아니면
            {
                audioSource.Play();
            }
            
        }

        #endregion
    }
}
