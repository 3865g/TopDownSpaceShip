using Scripts.Hero;
using UnityEngine;

public class RotateWithSpeed : MonoBehaviour
{
    public GameObject ParentGameObject;
    public ShipMove ShipMove;
    public CharacterController CharacterController;
    public Vector3 MovementVector;
    public float RoationSpeed =100;
    public float speed = 15f;

    private Quaternion _originalRotation = new Quaternion(0, 0, 0, 0);
    private Quaternion _zeroSpeed;



    private void Awake()
    {
        ShipMove = ParentGameObject.GetComponent<ShipMove>();
        CharacterController = ParentGameObject.GetComponent<CharacterController>();

    }



    private void Update()
    {
        _zeroSpeed = Quaternion.LookRotation(new Vector3(1, 0, 0), _originalRotation.eulerAngles);
        MovementVector = Vector3.Lerp(CharacterController.velocity, _zeroSpeed.eulerAngles, 0.25f);

        if(MovementVector != Vector3.zero)
        {

            transform.rotation = SmoothedRotation(transform.rotation, MovementVector);
            
        }
        else
        {
            transform.rotation = SmoothedRotation(transform.rotation, _originalRotation.eulerAngles);
        }

    }


    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
    {
        return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
    }

    private Quaternion TargetRotation(Vector3 positionToLook)
    {
        if(positionToLook != Vector3.zero)
        {
            return Quaternion.LookRotation(positionToLook);
        }
        else
        {
            return Quaternion.LookRotation(new Vector3(0, 1, 0), positionToLook);
        }
    }

    private float SpeedFactor()
    {
        return speed * Time.deltaTime;
    }

}
