using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using UnityEngine;

namespace GMDClone.Gameplay.Character
{
    public class CubeRotationAnimation : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private CharacterGround _ground;
        [Header("Gameplay Settings")]
        [SerializeField] private float _degress = 90f;
        [SerializeField] private float _duration = 2.5f;
        [SerializeField] private AnimationCurve _ease;

        private CancellationTokenSource _cancellationTokenSource;
        private static Quaternion DefaultRotationOnEnable { get; } = Quaternion.identity;

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Play().Forget();
        }

        private void OnDisable()
        {
            transform.rotation = DefaultRotationOnEnable;
            _cancellationTokenSource.Cancel();
        }

        private async UniTask Play()
        {
            while (true)
            {
                await UniTask.WaitWhile(() => _ground.IsOnGround == true, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                await transform.DORotate(Vector3.forward * _degress, _duration, RotateMode.LocalAxisAdd).SetEase(_ease).AsyncWaitForCompletion().AsUniTask();
            }
        }
    }
}