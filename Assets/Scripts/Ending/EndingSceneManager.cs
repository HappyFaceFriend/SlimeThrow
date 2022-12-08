using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    [System.Serializable]
    public class CreditData
    {
        public string Title;
        public string Name;
    }
    [SerializeField] Credit _creditPrefab;
    [SerializeField] float _creditInterval;
    [SerializeField] float _creditShowDuration;
    [SerializeField] Transform _creditPosition;

    [SerializeField] CreditData [] _creditDatas;

    [SerializeField] Transform[] _afterCredits;

    [SerializeField] Transform _thankyou;
    public void AnimEvent_StartCredit()
    {
        StartCoroutine(CreditCoroutine());
    }

    IEnumerator CreditCoroutine()
    {
        for(int i=0; i<_creditDatas.Length; i++)
        {
            yield return new WaitForSeconds(_creditInterval);
            Credit credit = Instantiate(_creditPrefab, _creditPosition.position, Quaternion.identity);
            credit.transform.SetParent(_creditPosition.parent);
            credit.Init(_creditDatas[i].Title, _creditDatas[i].Name);
            credit.transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(_creditShowDuration);
            credit.Kill();
        }

        for(int i=0; i<_afterCredits.Length; i++)
        {
            yield return new WaitForSeconds(_creditInterval);
            _afterCredits[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(_creditShowDuration);
            _afterCredits[i].GetComponent<Animator>().SetTrigger("Destroy");
        }
        yield return new WaitForSeconds(_creditInterval);
        _thankyou.gameObject.SetActive(true);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
