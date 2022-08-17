using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public enum State : short
{
    Welcome,
    Intro,
    Mannequin,
    Head,
    Face,
    Shoulders,
    Arms,
    Chest,
    Stomach,
    Body,
    Emojis1,
    Emojis2,
    Eword,
    Resize,
    End,
}

public class StateManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] GameObject mannequin;
    [SerializeField] TMP_Text tmpText;
    [SerializeField] EmojiBallManager ebm;

    [SerializeField] State currState = State.Welcome;
    [SerializeField] float timer;
    [SerializeField] int test_playerCount = 1;

    private AudioSource audioSource;
    private bool playOneShot = true;
    private PhotonView pv;



    void Start(){
        tmpText.text = "";
        audioSource = GetComponent<AudioSource>();
        pv = GetComponent<PhotonView>();
        
        Debug.Log("STATE: "  + currState); // STATE: Intro
        audioSource.clip = audioClips[0];
        timer = 0f;
    }

    void Update(){
        if(PhotonNetwork.CurrentRoom != null && (PhotonNetwork.CurrentRoom.PlayerCount > 1 || test_playerCount > 1))
            timer += Time.deltaTime;

        TurnMannequinOn();

        Debug.Log("CURRENT STATE: " + currState);
        // Debug.Log("PLAYER COUNT: " + PhotonNetwork.CurrentRoom.PlayerCount);
        // Debug.Log("B DOWN: " + ControllerAdditions.bPressed);
        // Debug.Log(PhotonNetwork.CurrentRoom);
        
        if(currState == State.Welcome && timer > 3f){
            if(playOneShot){
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 120f + 18f){
                pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Intro);
            }
        }
        else if(currState == State.Intro && timer > 1f){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Intro
                audioSource.clip = audioClips[3];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 61f){
                pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Mannequin);
            }
        }
        else if(currState == State.Mannequin && timer > 2f){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Mannequin
                audioSource.clip = audioClips[4];
                audioSource.Play();
                playOneShot = false;
            }
            
            if(timer > 61f){
                pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Head);
            }
        }
        else if(currState == State.Head){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Head
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                mannequin.transform.Find("Head").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("PlacementLocations/FaceAndHeadPlacement").GetComponent<Outline>().enabled = true;
                audioSource.clip = audioClips[5];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 30f + 33f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to move to the next body part";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Face);
                    }
                }
            }
        }
        else if(currState == State.Face){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Face
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                audioSource.clip = audioClips[6];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 30f + 20f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to move to the next body part";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("UnhighlightMannequin", RpcTarget.AllBufferedViaServer);
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Shoulders);
                    }
                }
            }
        }
        else if(currState == State.Shoulders){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Shoulders
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                mannequin.transform.Find("Torso").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("PlacementLocations/NeckAndShouldersPlacement").GetComponent<Outline>().enabled = true;
                audioSource.clip = audioClips[7];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 30f + 19f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to move to the next body part";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("UnhighlightMannequin", RpcTarget.AllBufferedViaServer);
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Arms);
                    }
                }
            }
        }
        else if(currState == State.Arms){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Arms
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                mannequin.transform.Find("Arms").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("PlacementLocations/ArmsPlacement").GetComponent<Outline>().enabled = true;
                audioSource.clip = audioClips[8];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 30f + 5f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to move to the next body part";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("UnhighlightMannequin", RpcTarget.AllBufferedViaServer);
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Chest);
                    }
                }
            }
        }
        else if(currState == State.Chest){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Chest
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                mannequin.transform.Find("Torso").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("PlacementLocations/ChestPlacement").GetComponent<Outline>().enabled = true;
                audioSource.clip = audioClips[9];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 30f + 16f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to move to the next body part";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("UnhighlightMannequin", RpcTarget.AllBufferedViaServer);
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Stomach);
                    }
                }
            }
        }
        else if(currState == State.Stomach){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Stomach
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                mannequin.transform.Find("Stomach").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("PlacementLocations/StomachPlacement").GetComponent<Outline>().enabled = true;
                audioSource.clip = audioClips[10];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 30f + 16f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to move to the next body part";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("UnhighlightMannequin", RpcTarget.AllBufferedViaServer);
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Body);
                    }
                }
            }
        }
        else if(currState == State.Body){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Body
                if(PhotonNetwork.LocalPlayer.ActorNumber > 1){
                    Debug.Log("PLAYER 2 TEXT");
                    tmpText.text = "Place an emoji on the mannequin";
                } 
                mannequin.transform.Find("Arms").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("Extras").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("Head").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("Joints").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("Legs").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("Stomach").GetComponent<Outline>().enabled = true;
                mannequin.transform.Find("Torso").GetComponent<Outline>().enabled = true;
                audioSource.clip = audioClips[11];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 60f + 10f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to go to the next exercise";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("UnhighlightMannequin", RpcTarget.AllBufferedViaServer);
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Emojis1);
                    }
                }
            }
        }
        else if(currState == State.Emojis1){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Emojis1
                audioSource.clip = audioClips[12];
                audioSource.Play();
                playOneShot = false;
            }

            if(!audioSource.isPlaying && PhotonNetwork.LocalPlayer.ActorNumber == 1){
                tmpText.text = "Move close to the mannequin";

                if(ObjectDistTo.isClose){
                    pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Emojis2);
                }
            }
        }
        else if(currState == State.Emojis2 && timer > 1f){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Emojis2
                audioSource.clip = audioClips[13];
                audioSource.Play();
                playOneShot = false;
            }

            if(timer > 60f + 15f){
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
                    tmpText.text = "Click (B) to go to the next exercise";

                    if(ControllerAdditions.bPressed){
                        pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Eword);
                    }
                }
            }
        }
        else if(currState == State.Eword){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Eword
                ebm.EnableEmojiBallTap();
                audioSource.clip = audioClips[14];
                audioSource.Play();
                playOneShot = false;
            }

            if(PhotonNetwork.LocalPlayer.ActorNumber > 1 && timer > 27f){
                tmpText.text = "Touch the emoji orbs and pick an emotion that fits better\nPress (B) to go to the next exercise";

                if(ControllerAdditions.bPressed){
                    pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.Resize);
                }
            }
        }
        else if(currState == State.Resize){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: Resize
                ebm.EnableEmojiBallScale();
                audioSource.clip = audioClips[15];
                audioSource.Play();
                playOneShot = false;
            }

            if(PhotonNetwork.LocalPlayer.ActorNumber > 1 && timer > 23f){
                tmpText.text = "Resize emotion balls\nPress (B) when ready to finish exercise";

                if(ControllerAdditions.bPressed){
                    pv.RPC("ChangeState", RpcTarget.AllBufferedViaServer, (short)State.End);
                }
            }
        }
        else if(currState == State.End){
            if(playOneShot){
                Debug.Log("STATE: "  + currState); // STATE: End
                audioSource.clip = audioClips[16];
                audioSource.Play();
                playOneShot = false;
            }
        }
    }


    [PunRPC]
    void ChangeState(short s){
        tmpText.text = "";
        timer = 0f;
        playOneShot = true;
        audioSource.Stop();
        currState = (State)s;
    }

    [PunRPC]
    void UnhighlightMannequin(){
        foreach (var outline in mannequin.GetComponentsInChildren<Outline>()) {
            outline.enabled = false;
        }
    }

    void TurnMannequinOn(){
        if(mannequin.activeSelf == true){
            return;
        }
        else if((short)currState >= (short)State.Intro){
            mannequin.SetActive(true);
        }
    }
}
