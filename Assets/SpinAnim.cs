using UnityEngine;

public class SpinAnim : MonoBehaviour
{
    [SerializeField]
    private float SpinForce = 125f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, SpinForce * Time.deltaTime, 0f, Space.Self);    
    }
}
