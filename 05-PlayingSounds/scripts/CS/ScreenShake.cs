using Godot;

namespace Tutorial05_PlayingSounds {

    public partial class ScreenShake : Node {
        private Camera2D _cam;
        private FastNoiseLite _noise;

        private Vector2 _camBasePosition;
        private const float ShakeTime = 1.0f;
        private const float ShakeAmount = 5000.0f;
        private float _screenShakeDelay;

        public override void _Ready() {
            _cam = GetNode<Camera2D>("Camera2D");
            _noise = new();
        }

        public void Shake() {
            _camBasePosition = _cam.GlobalPosition;
            _screenShakeDelay = ShakeTime;
        }

        public override void _Process(double delta) {
            if (_screenShakeDelay > 0f) {
                _cam.GlobalPosition = _camBasePosition
                    + new Vector2(_GetNoise(0), _GetNoise(1));
                _screenShakeDelay -= (float) delta;
            }
        }

        private float _GetNoise(int seed) {
            _noise.Seed = seed;
            return _noise.GetNoise1D(
                GD.Randf() * _screenShakeDelay) * ShakeAmount;
        }
    }

}
