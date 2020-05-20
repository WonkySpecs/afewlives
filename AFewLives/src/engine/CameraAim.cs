using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    readonly struct CameraAim
    {
        public readonly float zoom;
        public readonly Vector2 absPos;
        public readonly Vector2 relPos;
        // Whether to use absolute or relative position for each dimension 
        public readonly bool absX;
        public readonly bool absY;

        public CameraAim(float zoom, Vector2 relPos=new Vector2(), Vector2 absPos=new Vector2(), bool absX=false, bool absY=false)
        {
            this.absPos = absPos;
            this.relPos = relPos;
            this.absX = absX;
            this.absY = absY;
            this.zoom = zoom;
        }

        public void AimCamera(Camera2D cam, Vector2 playerPos)
        {
            cam.targetPos = new Vector2(absX ? absPos.X : playerPos.X + relPos.X,
                                        absY ? absPos.Y : playerPos.Y + relPos.Y);
            cam.targetZoom = zoom;
        }
    }
}
