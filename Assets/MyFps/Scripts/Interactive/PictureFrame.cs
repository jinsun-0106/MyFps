using System.Collections;
using TMPro;
using UnityEngine;

namespace MyFps
{
    public class PictureFrame : Interactive
    {
        #region Variables
        //도어스위치
        public GameObject doorSwitch;
        //눈 퍼즐 조각
        public GameObject leftEye;
        public GameObject rightEye;

        //시퀀스 텍스트
        public TextMeshProUGUI squenceText;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(MakePicture());
        }

        IEnumerator MakePicture()
        {
            bool isLeft = PlayerStats.Instance.HavePuzzleItem(PuzzleItem.LeftEye);
            bool isRight = PlayerStats.Instance.HavePuzzleItem(PuzzleItem.RightEye);

            //퍼즐 조각 맞추기
            if (isLeft)
            {
                leftEye.SetActive(true);
            }
            if (isRight)
            {
                rightEye.SetActive(true);
            }

            //모든 퍼즐 조각을 다 맞추었는지 체크
            if (isLeft && isRight)
            {
                doorSwitch.SetActive(true);
            }
            else // 실패
            {
                squenceText.text = "Need more puzzle pieces";
                yield return new WaitForSeconds(2f);
                squenceText.text = "";

                //충돌체 복구
                collider.enabled = true;

            }
        }
        #endregion

    }
}
