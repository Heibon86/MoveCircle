using System.Collections.Generic;
using ClickHandler;
using Game.Player;
using UniRx;
using UnityEngine;

namespace Game.Bonuses
{
    public class BonusPool : MonoBehaviour
    {
        private List<SquareBonus> _squareBonuses = new List<SquareBonus>();

        private SquareBonus _squareBonusPrefab;
        
        private int _maxBonuses;

        public void Initialize(SquareBonus squareBonus, int maxBonuses)
        {
            _squareBonusPrefab = squareBonus;
            _maxBonuses = maxBonuses;
            
            FillArray();
        }

        public SquareBonus GetSquareBonus()
        {
            for (int i = 0; i < _squareBonuses.Count; i++)
            {
                if (!_squareBonuses[i].gameObject.activeSelf)
                {
                    return _squareBonuses[i];
                }
            }
            return null;
        }

        private void FillArray()
        {
            SquareBonus squareBonus;

            for (int i = 0; i < _maxBonuses; i++)
            {
                squareBonus = Instantiate(_squareBonusPrefab, transform);
                squareBonus.Trigger.Where(result => result.Key.Equals(KeysStorage.Collision)).Subscribe(PushPool);
                squareBonus.gameObject.SetActive(false);
                _squareBonuses.Add(squareBonus);
            }
        }

        private void PushPool(Callback callback)
        {
            callback.SquareBonus.gameObject.SetActive(false);
        }
    }
}
