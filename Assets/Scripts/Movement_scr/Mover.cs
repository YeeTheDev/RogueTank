using System.Collections;
using UnityEngine;
using Yee.Math;

namespace RTank.Movement
{
    public class Mover : MonoBehaviour
    {
        [Range(1, 10)] [SerializeField] float speed;
        [Range(1, 10)] [SerializeField] float rotationSpeed;

        public IEnumerator MoveAndRotate(Vector3 addedPoint)
        {
            Vector3 movePoint = transform.position + addedPoint;
            Vector3 direction = movePoint - transform.position;

            float angle = MathY.CalculateFlatAngle(transform.forward, direction);
            if (angle > 1) { yield return Rotate(angle, direction); }

            yield return Move(movePoint, direction);
        }

        public IEnumerator Rotate(float angle, Vector3 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(direction, transform.up);

            if (angle > 1)
            {
                float time = 0;
                float timeToAdd = rotationSpeed * Time.fixedDeltaTime;
                while (time < 1f)
                {
                    time += angle > 91 ? timeToAdd * 0.5f : timeToAdd;
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, time);
                    yield return new WaitForFixedUpdate();
                }
                transform.rotation = lookAt;
            }
        }

        public IEnumerator Move(Vector3 movePoint, Vector3 direction)
        {
            while (Vector3.SqrMagnitude(transform.position - movePoint) > 0.01f)
            {
                transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
                yield return new WaitForFixedUpdate();
            }

            transform.position = movePoint;
        }
    }
}