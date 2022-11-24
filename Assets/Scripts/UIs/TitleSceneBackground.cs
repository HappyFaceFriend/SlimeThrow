using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneBackground : MonoBehaviour
{
    [SerializeField] TitleSceneManager _manager;

    public void EnableCameraMove()
    {
        _manager.EnableCameraMove();
    }
}
