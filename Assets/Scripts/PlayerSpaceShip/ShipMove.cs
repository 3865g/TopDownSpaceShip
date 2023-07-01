using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts;
using Scripts.Infrastructure.Services;
using Scripts.Services.Input;
using UnityEngine;
using Scripts.Data;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.PlayerSpaceShip
{

    public class ShipMove : MonoBehaviour, ISavedProgress
    {

        public float MovementSpeed = 35f;

        private CharacterController _characterController;
        private IInputService _inputService;



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
                transform.forward = movementVector;
            }

            _characterController.Move(movementVector * Time.deltaTime * MovementSpeed);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }


        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                {
                    Warp(savedPosition: savedPosition);
                }
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
