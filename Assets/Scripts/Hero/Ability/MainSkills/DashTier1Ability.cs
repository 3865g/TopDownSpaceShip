using UnityEngine;
using UnityEngine.VFX;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "MainSkills / DashTier1", order = 0)]
    public class DashTier1Ability : ConfigurationAbility
    {
        public float DashRange;
        public float BonusSpeed;


        private CharacterController _characterController;
        private ShipMove _shipMove;

        public GameObject Trail;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Vector3 _rotation;
        private GameObject _spawnedTrail;
        private TrailController _effect;
        private bool _abilityIsActive;

        public override void Activate(GameObject parent)
        {

            if (!_characterController)
            {
                _characterController = parent.GetComponent<CharacterController>();
            }

            if (!_shipMove)
            {
                _shipMove = parent.GetComponent<ShipMove>();
            }


            if (!_abilityIsActive)
            {

                _startPosition = parent.transform.position;
                _endPosition = _startPosition + (_shipMove.MovementVector * DashRange);
                _characterController.Move(_shipMove.MovementVector * DashRange);


                _rotation = parent.transform.rotation.eulerAngles;

                _spawnedTrail = Instantiate(Trail);
                _effect = _spawnedTrail.GetComponent<TrailController>();

                _effect.SetVariables(_startPosition, _endPosition, _rotation);

                _shipMove.UpdateBuffSpeed(BonusSpeed);
                _abilityIsActive = true;
            }

            
        }

        public override void Deactivate(GameObject parent)
        {

            if (_abilityIsActive)
            {
                Destroy(_spawnedTrail);
                _shipMove.UpdateBuffSpeed(-BonusSpeed);
                _abilityIsActive = false;
                //Debug.LogError(" DashDeactivate" );
            }
        }

    }
}

