using System;
using CryEngine;

namespace CryEngine.Extensions
{
	[EntityComponent(Guid="d8e576fa-eb28-1ad9-14cc-2c8838ab10ba")]
	public static class JescoExtensions
	{

        public static Vector3 MoveTo(Vector3 currentPostion, Vector3 targetPosition)
        {
            if (currentPostion != targetPosition)
            {
                currentPostion = targetPosition;
            }

            return targetPosition;
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            Vector3 vector3 = target - current;
            float magnitude = vector3.Magnitude;
            if ((double)magnitude <= maxDistanceDelta || magnitude < 1.40129846432482E-45)
                return target;
            return current + vector3 / magnitude * maxDistanceDelta;
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Math.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Vector3 vector = current - target;
            Vector3 vector3_1 = target;
            float maxLength = maxSpeed * smoothTime;
            Vector3 vector3_2 = ClampMagnitude(vector, maxLength);
            target = current - vector3_2;
            Vector3 vector3_3 = (currentVelocity + num1 * vector3_2) * deltaTime;
            currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
            Vector3 vector3_4 = target + (vector3_2 + vector3_3) * num3;
            if ((double)Vector3.Dot(vector3_1 - current, vector3_4 - vector3_1) > 0.0)
            {
                vector3_4 = vector3_1;
                currentVelocity = (vector3_4 - vector3_1) / deltaTime;
            }
            return vector3_4;
        }

        internal static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            if ((double)vector.Magnitude > (double)maxLength * (double)maxLength)
                return vector.Normalized * maxLength;
            return vector;
        }
    }
}