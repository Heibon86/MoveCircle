using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Bonuses;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.Spawners
{
    public class BonusesSpawner : MonoBehaviour
    {
        [SerializeField] private int _maxSquareBonusCount;
        [SerializeField] private int _maxDelaySpawnMilliSecond;
        [SerializeField] private AssetReference _squareBonusAsset;

        [Inject] private BonusPool _bonusPool;
        [Inject] private GameArea _gameArea;
        [Inject] private Camera _camera;

        public async void Initialize()
        {
            _gameArea.CalculateGameArea(_camera);
            
            var result = Addressables.LoadAssetAsync<GameObject>(_squareBonusAsset);
            
            await UniTask.WaitUntil(() => result.IsDone);
        
            if (result.Result.TryGetComponent(out SquareBonus squareBonus))
            {
                _bonusPool.Initialize(squareBonus, _maxSquareBonusCount);
            }
            
            SpawnSquareBonuses();
        }

        private async void SpawnSquareBonuses()
        {
            Vector2 randomPoint = _gameArea.GetRandomPoint(); 
            
            int delaySpawn = CalculateDelaySpawn();

            await UniTask.Delay(delaySpawn);

            SquareBonus squareBonus = _bonusPool.GetSquareBonus();
            
            if (squareBonus != null)
            {
                squareBonus.transform.localPosition = randomPoint;
                squareBonus.gameObject.SetActive(true);
            }
            
            SpawnSquareBonuses();
        }

        private int CalculateDelaySpawn()
        {
            int delaySpawn = Random.Range(0, _maxDelaySpawnMilliSecond);
            return delaySpawn;
        }
    }
}
