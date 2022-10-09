using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrababble
{
    void SetGrabbed(GrabController grabController);

    void OnReleasedAtGround();
}
