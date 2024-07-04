using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / DashTier1", order = 0)]
    public class DashTier1Ability : ActiveAbility
    {
        public float DashVelocity;

        private CharacterController _characterController;
        private ShipMove _shipMove;

        public GameObject Trail;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Vector3 _rotation;
        private GameObject _spawnedTrail;
        private TrailController _effect;

        public override void Activate(GameObject parent)
        {


            if(Trail != null)
            {
                
            }

            if (!_characterController)
            {
                _characterController = parent.GetComponent<CharacterController>();
            }

            if (!_shipMove)
            {
                _shipMove = parent.GetComponent<ShipMove>();
            }

            

            _startPosition = parent.transform.position;

            _characterController.Move(_shipMove.MovementVector * DashVelocity);

            _endPosition = parent.transform.position;
            _rotation = parent.transform.rotation.eulerAngles;

            _spawnedTrail = Instantiate(Trail, parent.transform) as GameObject;
            _effect = _spawnedTrail.GetComponent<TrailController>();

            _effect.SetVariables(_startPosition, _endPosition, _rotation);           

            
        }

        public override void Deactivate(GameObject parent)
        {
            Destroy(_spawnedTrail);
        }

    }
}

