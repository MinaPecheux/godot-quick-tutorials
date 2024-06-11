using Godot;
public partial class PlayerTopDown : CharacterBody2D {
	public const float Speed = 400.0f;
	public override void _PhysicsProcess(double delta) {
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector(
			"ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero) {
			velocity = direction * Speed;
			LookAt(Position + direction);
		} else {
			velocity.X = Mathf.MoveToward(
				Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(
				Velocity.Y, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
