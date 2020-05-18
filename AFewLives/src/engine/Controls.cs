
using Microsoft.Xna.Framework.Input;

namespace AFewLives
{
    class Controls
    {
        private bool[] wasPressed = new bool[(int)Control.SIZE];
        private bool[] isPressed = new bool[(int)Control.SIZE];

        public void Update(KeyboardState keyboard)
        {
            isPressed.CopyTo(wasPressed, 0);
            isPressed[(int)Control.MoveLeft] = keyboard.IsKeyDown(Keys.A);
            isPressed[(int)Control.MoveRight] = keyboard.IsKeyDown(Keys.D);
            isPressed[(int)Control.Jump] = keyboard.IsKeyDown(Keys.Space);
            isPressed[(int)Control.MoveUp] = keyboard.IsKeyDown(Keys.W);
            isPressed[(int)Control.MoveDown] = keyboard.IsKeyDown(Keys.S);
            isPressed[(int)Control.Interact] = keyboard.IsKeyDown(Keys.E);
            isPressed[(int)Control.ToggleLife] = keyboard.IsKeyDown(Keys.Q);
            isPressed[(int)Control.ToggleZoom] = keyboard.IsKeyDown(Keys.Z);
            isPressed[(int)Control.Pause] = keyboard.IsKeyDown(Keys.P);
        }

        public bool WasPressed(Control control)
        {
            return isPressed[(int)control] && !wasPressed[(int)control];
        }

        public bool IsDown(Control control)
        {
            return isPressed[(int)control];
        }
    }

    enum Control
    {
        MoveLeft, MoveRight, Jump,     // Alive only
        MoveUp, MoveDown,              // Ghost only
        Interact,                      // Either
        ToggleLife, Pause, ToggleZoom, // Temporary debug controls
        SIZE
    }
}
