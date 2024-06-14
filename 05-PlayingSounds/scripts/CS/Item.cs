using Godot;

namespace Tutorial05_PlayingSounds {

	public partial class Item : CharacterBody2D {
		private const string SpritesFolder = "res://05-PlayingSounds/art/sprites";
		public bool isAsteroid = false;
		
		public void Initialize() {
			Velocity = Vector2.Left * 350f;
			if (GD.Randf() < 0.33) {
				isAsteroid = true;
				GetNode<Sprite2D>("Sprite2D").Texture =
					GD.Load<Texture2D>($"{SpritesFolder}/asteroid.png");
			}
		}

		public override void _PhysicsProcess(double delta) {
			MoveAndSlide();
		}

		private void _OnScreenExited() {
			QueueFree();
		}
	}

}
