
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        float _eTime = 0f;
        float _duration;

        public bool IsOver { get { return _eTime > _duration; } }
        public Timer(float duration)
        {
            _duration = duration;
            _eTime = 0f;
        }
        public void Tick()
        {
            _eTime += Time.deltaTime;
        }
        public void Reset()
        {
            _eTime = 0f;
        }
        
    }
}