using UnityEngine;
using System;

namespace MyFps
{
    /// <summary>
    /// 플레이 데이터 정의
    /// </summary>
    [Serializable]
    public class PlayData
    {
        public int sceneNumber;             //씬 번호
        public string sceneName;            //씬 이름
        public int ammoCount;               //탄환 갯수
        public int weaponType;              //소지 무기 타입
        public float playerHP;              //플레이어 체력

        //...

        //생성자
        public PlayData()
        {
            //초기화
            sceneNumber = PlayerStats.Instance.SceneNumber;
            sceneName = PlayerStats.Instance.SceneName;
            ammoCount = PlayerStats.Instance.AmmoCount;
            weaponType = (int)PlayerStats.Instance.WeaponType;
            playerHP = PlayerStats.Instance.PlayerHP;

        }

    }
}
