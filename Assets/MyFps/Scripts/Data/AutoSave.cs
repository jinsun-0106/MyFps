using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    public class AutoSave : MonoBehaviour
    {
        private const string SceneNumber = "SceneNumber";

        private void Awake()
        {
            SaveData();
        }


        //데이터 저장하기
        private void SaveData()
        {
            PlayData playData = SaveLoad.LoadData();
            PlayerStats.Instance.PlayerStatsInitialize(playData);

            //저장된 번호 가져오기
            //int saveNumber = PlayerPrefs.GetInt(SceneNumber, -1);
            int saveNumber = PlayerStats.Instance.SceneNumber;

            //씬 번호 저장
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;

            if (saveNumber <= sceneNumber)
            {
                //저장
                //PlayerPrefs.SetInt(SceneNumber, sceneNumber);
                PlayerStats.Instance.SetSceneNumber(sceneNumber);
                SaveLoad.SaveData();
            }
            else
            {
                //새로 게임 시작 데이터를 강제로 셋팅 (?)
                PlayerStats.Instance.PlayerStatsInit();
            }
        }
    }
}
