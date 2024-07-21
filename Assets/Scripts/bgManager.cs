using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject backgroundPrefab;

    private void Start()
    {
        if (backgroundPrefab != null)
        {
            Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Background prefab not assigned in BackgroundManager");
        }
    }
}
