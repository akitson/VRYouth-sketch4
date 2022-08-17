using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyLoad : MonoBehaviour
{
    public void LoadScene() {
        Debug.Log($"Room # = {RoomSettings.RoomNumber}, Scene # = {RoomSettings.SceneEnvironment}");
        SceneManager.LoadScene(RoomSettings.SceneEnvironment);
    }
}
