using UnityEngine;
using TMPro;

namespace MyFps
{
    /// <summary>
    /// 플레이어와 인터렉션 기능 오브젝트
    /// 인터렉티브 : 마우스를 가져가면 UI 활성화 빼면 UI 비활성화
    /// 인터렉션 기능 : 도어 오픈
    /// </summary>
    public class DoorCellOpen : MonoBehaviour
    {
        #region Variables
        //크로스헤어
        public GameObject extraCross;

        //액션 UI
        public GameObject actionUI;
        public TextMeshProUGUI actionText;

        [SerializeField]
        private string action = "Open The Door";

        //액션
        public Animator animator;
        private Collider collider;

        //애니메이션 파라미터
        const string Open = "Open";
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            collider = GetComponent<Collider>();
        }
        private void OnMouseOver()
        {
            if(PlayerCasting.distanceFromTarget > 2f)
            {
                HideActionUI();
                return;
            }

            ShowActionUI();

            //만약 Action 버튼을 누르면
            if (Input.GetButtonDown("Action"))
            {
                OpenDoor();
            }
        }

        private void OnMouseExit()
        {
            HideActionUI();
        }

        #endregion

        #region Custom Method
        private void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
            extraCross.SetActive(true);
        }

        private void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
            extraCross.SetActive(false);
        }

        void OpenDoor()
        {
            //UI 감추기
            HideActionUI();

            //애니메이션
            animator.SetTrigger(Open);

            //충돌체 기능 제거
            collider.enabled = false;
            
        }

        #endregion
    }
}
