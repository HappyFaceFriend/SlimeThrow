using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSequenceGenerator : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public Vector2 Position;
        public bool NoLine = false;
        public float Scale = 1f;
        public float Time = 0f;
    }

    [SerializeField] List<SpawnData> _sequence;
    private void OnEnable()
    {
        GenerateSequence();
    }
    public void GenerateSequence()
    {
        _sequence.Sort((a, b) => { if (a.Time < b.Time) return -1; return 1; });
        StartCoroutine(SpawnCoroutine(_sequence));
    }

    IEnumerator SpawnCoroutine(List<SpawnData> sequence)
    {
        yield return null;
        for(int i=0; i < sequence.Count; i++)
        {
            var data = sequence[i];
            EffectManager.InstantiateHitEffect(new Vector3(data.Position.x, data.Position.y, 0) + transform.position, data.Scale, data.NoLine);
            if (i < sequence.Count - 1 && sequence[i + 1].Time == data.Time)
                continue;
            yield return new WaitForSeconds(data.Time);
        }
    }
}
