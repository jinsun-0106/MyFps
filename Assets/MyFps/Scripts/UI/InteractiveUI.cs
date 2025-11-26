using TMPro;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 인터렉티브 UI 관리 (show, hide)하는 클래스
    /// </summary>
    public class InteractiveUI : MonoBehaviour
    {
        #region Variables
        //인터렉티브 UI
        [Header("Interactive UI")]
        //크로스헤어
        public GameObject extraCross;

        //액션 UI
        public GameObject actionUI;
        public TextMeshProUGUI actionText;
        #endregion

        #region Custom Method
        public void ShowActionUI(string action)
        {
            actionUI.SetActive(true);
            actionText.text = action;
            extraCross.SetActive(true);
        }

        public void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
            extraCross.SetActive(false);
        }
        #endregion
    }
}
