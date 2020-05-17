using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace AFewLives
{
    class Camera2D
    {
        private float _zoom;
        public float Zoom
        {
            get => _zoom;
            set => _zoom = Math.Max(0, value);
        }

        public Vector2 pos;
        public float rot;
        private Matrix _transform;

        private float cachedZoom;
        private Vector2 cachedPos;
        private float cachedRot;

        public Camera2D()
        {
            _zoom = 1f;
            pos = new Vector2();
            rot = 0;

            cachedRot = -1;
            cachedZoom = -1;
        }

        public Matrix GetTransform(Viewport viewport)
        {
            // If no changes since last call, return existing transform
            if (pos == cachedPos && _zoom == cachedZoom && rot == cachedRot) return _transform;

            _transform = Matrix.CreateTranslation(-pos.X, -pos.Y, 0) *
                         Matrix.CreateRotationZ(rot) *
                         Matrix.CreateScale(_zoom) *
                         Matrix.CreateTranslation(viewport.Width * 0.5f, viewport.Height * 0.5f, 0);
            cachedPos = pos;
            cachedZoom = _zoom;
            cachedRot = rot;
            return _transform;
        }
    }
}
