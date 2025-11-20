using TMPro;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 인터렉티브 오브젝트를 관리하는 클래스들의 부모 클래스
    /// </summary>
    public abstract class Interactive : MonoBehaviour
    {
        //추상 메서드
        #region abstract
        protected abstract void DoAction();

        #endregion

        #region Variables
        //참조
        protected Collider collider;

        //인터렉티브 UI
        [Header ("Interactive UI")]
        //크로스헤어
        public GameObject extraCross;

        //액션 UI
        public GameObject actionUI;
        public TextMeshProUGUI actionText;

        [SerializeField]
        protected string action = "Do Action";

        //인터렉티브 액션

        #endregion

        #region Unity Event Method
        protected virtual void Awake()
        {
            //참조
            collider = GetComponent<Collider>();
        }

        protected virtual void OnMouseOver()
        {
            //일정거리 이상되면 UI 숨김
            if (PlayerCasting.distanceFromTarget > 2f)
            {
                HideActionUI();
                return;
            }

            ShowActionUI();

            //만약 Action 버튼을 누르면
            if (Input.GetButtonDown("Action"))
            {
                //Do Action 영역 - 인터렉티브 액션
                DoAction();

                //충돌체 제거
                collider.enabled = false;

            }

        }

        protected virtual void OnMouseExit()
        {
            HideActionUI();
        }
        #endregion

        #region Custom Method
        protected virtual void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
            extraCross.SetActive(true);
        }

        protected virtual void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
            extraCross.SetActive(false);
        }

        #endregion
    }
}
