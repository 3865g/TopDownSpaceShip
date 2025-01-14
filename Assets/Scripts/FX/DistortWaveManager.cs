using System.Collections;
using UnityEngine;

public class DistortWaveManager : MonoBehaviour
{
    [SerializeField] private float _shockWaveTime = 0.75f;
    private Coroutine _distortWaveCoroutine;
    private Material _material;


    private void Awake()
    {
        _material = GetComponent<Material>();
    }

    public void CallDistortWave()
    {
        _distortWaveCoroutine = StartCoroutine(DistortWaveAction(-0.1f, 1f));
    }

    private IEnumerator DistortWaveAction(float startPos, float endPos)
    {
        float lerpedAmount = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / _shockWaveTime));

            yield return null;

        }
    }
}
