using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem [] _particles;
    public void Init(List<Color> colors)
    {
        for (int i=0; i<3; i++)
        {
            if(i < colors.Count)
            {
                ParticleSystem.MainModule p = _particles[i].main;
                p.startColor = colors[i];
                if (colors.Count == 2)
                {
                    var burst = _particles[i].emission.GetBurst(0);
                    burst.count = 3;
                    _particles[i].emission.SetBurst(0, burst);
                }
                else if(colors.Count == 3)
                {
                    var burst = _particles[i].emission.GetBurst(0);
                    burst.count = 2;
                    _particles[i].emission.SetBurst(0, burst);
                }
            }
            else
            {
                _particles[i].gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        Destroy(gameObject, 1.1f);
    }
}
