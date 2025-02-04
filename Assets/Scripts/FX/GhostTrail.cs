using UnityEngine;
using System.Collections;

public class GhostTrail : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 Direction;
    public GameObject meshPrefab; 
    public int numberOfMeshes = 10;
    public Gradient colorGradient; 
    public float fadeDelay = 0.05f; 
    public float fadeDuration = .05f; 

    private GameObject[] meshes;


    public void SpawnMeshesBetweenPoints()
    {
        StartCoroutine(FadeMeshes());
        if (pointA == null || pointB == null || meshPrefab == null)
        {
            return;
        }

        meshes = new GameObject[numberOfMeshes]; 

        Vector3 startPosition = pointA;
        Vector3 endPosition = pointB;
        Vector3 direction = (endPosition - startPosition).normalized; 
        float distance = Vector3.Distance(startPosition, endPosition);
        float step = distance / (numberOfMeshes + 1);

        for (int i = 0; i < numberOfMeshes; i++)
        {
            
            Vector3 spawnPosition = startPosition + direction * (step * (i + 1));

            
            GameObject meshInstance = Instantiate(meshPrefab, spawnPosition, Quaternion.Euler(Direction), transform);
            meshes[i] = meshInstance; 

            
            Renderer renderer = meshInstance.GetComponent<Renderer>();
            if (renderer != null)
            {
                float t = (float)i / (numberOfMeshes - 1);
                renderer.material.color = colorGradient.Evaluate(t);
            }
        }
    }

    IEnumerator FadeMeshes()
    {
        
        yield return new WaitForSeconds(0.01f);

       
        for (int i = 0; i < meshes.Length; i++)
        {
            //yield return StartCoroutine(FadeOut(meshes[i]));

            meshes[i].SetActive(false);
            yield return new WaitForSeconds(fadeDelay * i);
        }
    }

    IEnumerator FadeOut(GameObject mesh)
    {
        Renderer renderer = mesh.GetComponent<Renderer>();
        if (renderer == null) yield break;

        Material material = renderer.material;
        Color startColor = material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            material.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        
        mesh.SetActive(false);
    }
}
