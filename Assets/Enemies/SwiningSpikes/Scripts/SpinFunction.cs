using UnityEngine;

public class SpinFunction : MonoBehaviour
{
    [SerializeField]
    public float SpinForce = 150f;
    public float MaxRotate = 180;
    [SerializeField]
    private float CurrentRotation = 0f;
    public int SpinDirection = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Spin Function
        if (CurrentRotation < MaxRotate)
        {
            //Counts the rotation and Spins the object
            var rotation = SpinForce * Time.deltaTime;
            CurrentRotation += rotation;            
            transform.Rotate(0, 0, rotation * SpinDirection, Space.Self);
        }
        else
        {
            //Flips the animation and spin direction whilst reseting the rotation counter
            //Flip();
            SpinDirection *= -1;
            CurrentRotation = 0;
        }
    }

    //Flips the object
    private void Flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }
}
