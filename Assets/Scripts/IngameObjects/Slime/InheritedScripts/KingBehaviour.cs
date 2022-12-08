using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingBehaviour : SlimeBehaviour
{
    Image _image;
    bool _flashed = false;

    protected override void Awake()
    {
        base.Awake();
        _image = GlobalRefs.Flash.GetComponent<Image>();
    }
    protected override void Update()
    {
        base.Update();
        if (this.IsFever() && !_flashed)
        {
            GlobalRefs.Flash.gameObject.SetActive(true);
            StartCoroutine(Fadeout());
            _flashed = true;
            Animator.SetFloat("MoveAnimSpeed", 2);
        }
    }
    protected void OnDestroy()
    {

        if (!gameObject.scene.isLoaded)
            return;
        LastSlimeDestroyEffect();
    }
    IEnumerator Fadeout()
    {

        yield return new WaitForSeconds(0.3f);
        float eTime = 0;
        float duration = 0.5f;
        while(eTime < duration)
        {
            eTime += Time.deltaTime;
            Color color = _image.color;
            color.a = Mathf.Lerp(1, 0, eTime/duration);
            _image.color = color;
            yield return null;
        }
        _image.gameObject.SetActive(false);
    }
}
