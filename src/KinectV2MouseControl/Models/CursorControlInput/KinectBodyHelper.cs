﻿using Microsoft.Kinect;

namespace KinectV2MouseControl
{
    public static class KinectBodyHelper
    {
        const double HAND_LIFT_Z_DISTANCE = 0.3f;
        const double HAND_UP_Y_DISTANCE = 0.2f;
        const double GESTURE_Y_OFFSET = -0.65f;
        const double GESTURE_X_OFFSET = 0.185f;
        
        static MVector2[] gestureOffsets = new MVector2[] {
           new MVector2(GESTURE_X_OFFSET, GESTURE_Y_OFFSET),
           new MVector2(-GESTURE_X_OFFSET, GESTURE_Y_OFFSET)
        };

        public static bool IsHandLiftForward(this Body body, bool isLeft)
        {
            JointType hand = isLeft ? JointType.HandLeft : JointType.HandRight;
            return body.Joints[hand].Position.Z - body.Joints[JointType.SpineBase].Position.Z < -HAND_LIFT_Z_DISTANCE;
        }

        public static HandState GetHandState(this Body body, bool isLeft)
        {
            return isLeft ? body.HandLeftState : body.HandRightState;
        }

        public static bool IsHandLiftUpward(this Body body, bool isLeft)
        {
            JointType hand = isLeft ? JointType.HandLeft : JointType.HandRight;
            if (body.Joints[hand].TrackingState != TrackingState.Tracked)
            {
                return false;
            }
            return body.Joints[hand].Position.Y - body.Joints[JointType.SpineBase].Position.Y > HAND_UP_Y_DISTANCE;
        }

        public static MVector2 GetHandRelativePosition(this Body body, bool isLeft)
        {
            CameraSpacePoint handPos = body.Joints[isLeft ? JointType.HandLeft : JointType.HandRight].Position;
            CameraSpacePoint spineBase = body.Joints[JointType.SpineBase].Position;
            
            return handPos.ToMVector2() - spineBase.ToMVector2() + gestureOffsets[isLeft ? 0 : 1];
        }

        public static MVector2 ToMVector2(this CameraSpacePoint jointPoint)
        {
            return new MVector2(jointPoint.X, jointPoint.Y);
        }

    }
}
