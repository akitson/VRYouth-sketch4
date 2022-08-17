using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomSettings : MonoBehaviour{
    public static int SceneEnvironment = 1;
    public static int RoomNumber = 1;

    public void SetScene(Int32 sceneNum) {
        SceneEnvironment = sceneNum + 1;
    }

    public void SetRoomNumber(Int32 roomNum) {
        RoomNumber = roomNum + 1;
    }
}
