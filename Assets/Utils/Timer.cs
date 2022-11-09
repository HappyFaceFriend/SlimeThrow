
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        float _eTime = 0f;
        float _duration;

        public bool IsOver { get { return _eTime >= _duration; } }
        public Timer(float duration, bool startWithOver = false)
        {
            _duration = duration;
            if (startWithOver)
                _eTime = _duration;
            else
                _eTime = 0f;
        }
        public void Tick()
        {
            _eTime += Time.deltaTime;
        }
        public void Reset(float newDuration = -1f)
        {
            if (newDuration > 0f)
                _duration = newDuration;
            _eTime = 0f;
        }
        
    }
}