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


    private void FixedUpdate()
    {
        //MovementVector = ParentGameObject.GetComponent<CharacterController>().velocity;

        //Vector3 rotationDIrection = Vector3.Cross(ParentGameObject.transform.up, new Vector3(MovementVector.x, 0, MovementVector.z));
        //rotationDIrection *= -1;


        //gameObject.transform.rotation = Quaternion.LookRotation(rotationDIrection, ParentGameObject.transform.right);
    }

    private void Awake()
    {
        //MovementVector = ParentGameObject.GetComponent<CharacterController>().velocity;
        ShipMove = ParentGameObject.GetComponent<ShipMove>();
        CharacterController = ParentGameObject.GetComponent<CharacterController>();


    }



    private void Update()
    {

        MovementVector = CharacterController.velocity;
        //float angleY = -1 * Mathf.Asin(MovementVector.x) * RoationSpeed;
        //float angle = -1 * Mathf.Atan2(MovementVector.z, MovementVector.y) * RoationSpeed;

        //Quaternion rotation = Quaternion.Euler(angle, angleY, angle);

        //transform.rotation = rotation;

        if(MovementVector != Vector3.zero)
        {
            //transform.rotation = (Quaternion.LookRotation(new Vector3(0, 1, 0), MovementVector));

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
            return Quaternion.LookRotation(new Vector3(0, 1, 0), positionToLook);
        }
        else
        {
            return Quaternion.LookRotation(positionToLook);
        }
    }

    private float SpeedFactor()
    {
        return speed * Time.deltaTime;
    }

}
