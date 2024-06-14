using Godot;

namespace Tutorial05_PlayingSounds {

	public partial class Player : CharacterBody2D {

		public const float Speed = 700.0f;

		[Export] private AudioStream _pickupAudio;
		[Export] private AudioStream _hitAudio;
		private AudioStreamPlayer _audio;

		public override void _Ready() {
			_audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		}

		public override void _PhysicsProcess(double delta) {
			Vector2 velocity = Velocity;

			float direction = Input.GetAxis("ui_up", "ui_down");
			if (!Mathf.IsZeroApprox(direction)) {
				velocity.Y = direction * Speed;
			} else {
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}

		private void _OnArea2DBodyEntered(Node2D body) {
			if (body is Item i) {
				if (i.isAsteroid) {
					_audio.Stream = _hitAudio;
					GetNode<ScreenShake>("/root/Root/ScreenShake").Shake();
				}
				else _audio.Stream = _pickupAudio;

				body.QueueFree();
				_audio.Play();
			}
		}
	}

}
