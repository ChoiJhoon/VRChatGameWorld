
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerUI : UdonSharpBehaviour
{
    void Start()
    {
        int playerCount = VRCPlayerApi.GetPlayerCount(); // 현재 플레이어 수 가져오기
        VRCPlayerApi[] players = new VRCPlayerApi[playerCount];
        Quaternion rot = players[0].GetBoneRotation(HumanBodyBones.Head);
        Vector3 pos = players[0].GetBonePosition(HumanBodyBones.Head); 
    }
}
