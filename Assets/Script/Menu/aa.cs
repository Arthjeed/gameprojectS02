using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour {
        void Update () {
            clientPredictionValue = nManager.clientPredVal;
            velocityPredictionValue = nManager.velocityPredVal;

            if (photonView.isMine) { } else {

                //Progress Calculation for Interpolation value
                if (prevPosition == Vector3.zero) {
                    prevPosition = realPosition;
                }
                currentDistance = Vector3.Distance (tr.position, realPosition);
                fullDistance = Vector3.Distance (prevPosition, realPosition);
                if (fullDistance != 0) {
                    progress = currentDistance / fullDistance;
                }
                prevPosition = realPosition;

                if (clientPredictionValue != 0) {
                    progress *= clientPredictionValue;
                }

                syncTime += Time.deltaTime;
                tr.rotation = Quaternion.Lerp (tr.rotation, realRotation, syncTime / syncDelay);
                tr.position = Vector3.Lerp (tr.position, realPosition, progress);
            }
        }

        public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {

            if (stream.isWriting) {
                //This is OUR player. We need to send our actual position to the network.

                stream.SendNext (tr.position);
                stream.SendNext (tr.rotation);
                stream.SendNext (pMovement.playerVelocity);
                stream.SendNext (anim.GetFloat ("AimAngle"));
                stream.SendNext (anim.GetFloat ("HorizontalMovement"));
                stream.SendNext (anim.GetFloat ("VerticalMovement"));
                stream.SendNext (anim.GetBool ("Jump"));
            } else {
                //This is someone else's player. We need to receive their position (as of a few
                //milliseconds ago, and update our version of THAT player.

                // Right now, "RealPosition" holds the other's position at the LAST frame.
                // Instead of simply updating "realPosition" and continuing to lerp,
                // we MAY want to set our transform.position immediately to this old "realPosition"
                // and then update realPosition

                realPosition = (Vector3) stream.ReceiveNext ();
                realRotation = (Quaternion) stream.ReceiveNext ();
                playerVelocity = (Vector3) stream.ReceiveNext ();
                anim.SetFloat ("AimAngle", (float) stream.ReceiveNext ());
                anim.SetFloat ("HorizontalMovement", (float) stream.ReceiveNext ());
                anim.SetFloat ("VerticalMovement", (float) stream.ReceiveNext ());
                anim.SetBool ("Jump", (bool) stream.ReceiveNext ());

                syncTime = 0f;
                syncDelay = Time.time - lastSynchronizationTime;
                lastSynchronizationTime = Time.time;

                //Velocity Prediction
                realPosition = realPosition + playerVelocity * velocityPredictionValue * syncDelay;
            }
        }
}
}