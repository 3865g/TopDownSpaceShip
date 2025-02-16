using Scripts.Services.PersistentProgress;
using Scripts.Services;
using Scripts.Services.Input;
using UnityEngine;
using Scripts.Data;
using UnityEngine.SceneManagement;

namespace Scripts.Hero
{

    public class ShipMove : MonoBehaviour, ISavedProgress
    {

        public float MovementSpeed = 35f;
        public float BonuseSpeed;
        public float BuffSpeed;

        public float FlightAmplitude = 16; 
        //public bool RotateToEnemy = false;
        public Vector3 MovementVector;

        public CharacterController _characterController;
        public IInputService _inputService;

        private bool isReturning = false;



        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _characterController = GetComponent<CharacterController>();

        }



        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                

                MovementVector = movementVector;

            }

            //ReturnFlightAltitude();

            _characterController.Move(movementVector * Time.deltaTime * (MovementSpeed + BonuseSpeed + BuffSpeed));



        }

        public void ReturnFlightAltitude(Vector3 movment)
        {
            
            float returnSpeed = 10f;

            


            if (Mathf.Abs(transform.position.y - FlightAmplitude) > 1f && !isReturning)
            {
                isReturning = true;
            }

            
            if (isReturning)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, FlightAmplitude, transform.position.z);
                 transform.position = Vector3.Lerp(transform.position, targetPosition, returnSpeed * Time.deltaTime);

                if (Mathf.Abs(transform.position.y - FlightAmplitude) < .25f)
                {
                    //transform.position = targetPosition; 
                    isReturning = false;
                }
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            //progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }


        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.SavedLevel)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                //if (savedPosition != null)
                //{
                //    Warp(savedPosition: savedPosition);
                //}
            }
        }

        public void UpdateBonuseSpeed(float bonuseSpeed)
        {
            BonuseSpeed += bonuseSpeed;
        }

        public void UpdateBuffSpeed(float buffSpeed)
        {
            if(BuffSpeed == 0 && buffSpeed < 0)
            {
                return;
            }
            else
            {
                BuffSpeed += buffSpeed;
            }
        }

        private void Warp(Vector3Data savedPosition)
        {
            _characterController.enabled = false;
            transform.position = savedPosition.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }

    }
}
