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

        [SerializeField]
        protected string action = "Do Action";

        //인터렉티브 액션
        private InteractiveUI aUI;

        #endregion

        #region Unity Event Method
        protected virtual void Awake()
        {
            //참조
            collider = GetComponent<Collider>();

            aUI = GameObject.Find("GameHUD").GetComponent<InteractiveUI>();            

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

                //충돌체 제거
                collider.enabled = false;

                HideActionUI();

                //Do Action 영역 - 인터렉티브 액션
                DoAction();

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
            aUI.ShowActionUI(action);
        }

        protected virtual void HideActionUI()
        {
            
            aUI.HideActionUI();
            
        }

        #endregion
    }
}
