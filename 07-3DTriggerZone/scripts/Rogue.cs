using Godot;

namespace Tutorial07_3DTriggerZone {

	public partial class Rogue : CharacterBody3D {

		public const float Speed = 5.0f;

		[Export] private PackedScene _targetPointPrefab;
		private Node3D _root;
		private Camera3D _cam;
		private NavigationAgent3D _agent;

		public override void _Ready() {
			_root = GetNode<Node3D>("/root/Root");
			_cam = _root.GetNode<Camera3D>("%Camera3D");
			_agent = GetNode<NavigationAgent3D>("Agent");
		}

		public override void _PhysicsProcess(double delta) {
			if (Input.IsActionJustReleased("move-click")) {
				Vector2 m = GetViewport().GetMousePosition();
				var _from = _cam.ProjectRayOrigin(m);
				var to = _from + _cam.ProjectRayNormal(m) * 1000f;

				var spaceState = GetWorld3D().DirectSpaceState;
				var query = PhysicsRayQueryParameters3D.Create(
					_from, to, 2 /* /!\ bitmask */
				);
				var result = spaceState.IntersectRay(query);
				if (result.Count > 0) {
					Node3D p = _targetPointPrefab.Instantiate<Node3D>();
					_root.AddChild(p);
					p.GlobalPosition = result["position"].AsVector3();
					_SetTargetPosition(result["position"].AsVector3());
				}
			}

			Vector3 velocity = Velocity;

			if (!_agent.IsNavigationFinished()) {
				Vector3 next = _agent.GetNextPathPosition();
				LookAt(new Vector3(next.X, GlobalPosition.Y, next.Z), Vector3.Up);
				Vector3 diff = next - GlobalPosition;
				Vector3 dir = diff.Normalized();
				velocity.X = dir.X * Speed;
				velocity.Z = dir.Z * Speed;
			} else {
				velocity.X = 0f;
				velocity.Z = 0f;
			}

			Velocity = velocity;
			MoveAndSlide();
		}

		private void _SetTargetPosition(Vector3 pos) {
			var map = GetWorld3D().NavigationMap;
			var p = NavigationServer3D.MapGetClosestPoint(map, pos);
			_agent.TargetPosition = p;
		}
	}

}
