using Godot;
using System.Collections.Generic;

namespace Tutorial07_3DTriggerZone {

	public partial class Barbarian : Node3D {
		#region Alert system
		enum State { Idle, Warned, Alerted }
		private const float AlertAmount = 10f;
		private static Dictionary<State, Color> StateColors = new() {
			{ State.Idle, new Color("#3cff0f64") }, // green
			{ State.Warned, new Color("#ffd00f64") }, // yellow
			{ State.Alerted, new Color("#ff000f64") }, // red
		};

		[Export] private State state = State.Idle;
		private float _alertLevel = 0f;
		private List<Node3D> _enemies = new();

		private ShaderMaterial _mat;
		private Node3D _warnIcon, _alertIcon;
		#endregion

		private void _OnBodyEntered(Node3D body) {
			if (body.IsInGroup("enemies"))
				_DetectEnemy(body);
		}

		private void _OnBodyExited(Node3D body) {
			if (body.IsInGroup("enemies"))
				_enemies.Remove(body);
		}

		private async void _DetectEnemy(Node3D enemy) {
			_enemies.Add(enemy);
			_warnIcon.Show();
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
			_warnIcon.Hide();
		}
		public override void _Ready() {
			_mat = GetNode<Node3D>("%ZoneDebug").Get("surface_material_override/0").AsGodotObject() as ShaderMaterial;
			_warnIcon = GetNode<Node3D>("WarnIcon");
			_alertIcon = GetNode<Node3D>("AlertIcon");
			_warnIcon.Hide();
			_alertIcon.Hide();
		}
		public override void _Process(double delta) {
			_UpdateAlertLevel((float) delta);
		}
		private async void _UpdateAlertLevel(float delta) {
			if (_enemies.Count == 0) {
				if (_alertLevel < 0) _alertLevel = 0;
				if (_alertLevel == 0) return;
				_alertLevel -= AlertAmount * delta;
			} else {
				if (_alertLevel > 100) _alertLevel = 100;
				if (_alertLevel == 100) return;
				_alertLevel += _enemies.Count * AlertAmount * delta;
			}

			Color col;
			if (_alertLevel < 33) {
				state = State.Idle;
				col = _LerpColor(
					StateColors[State.Idle],
					StateColors[State.Warned],
					_alertLevel / 33f);
			} else if (_alertLevel < 66) {
				state = State.Warned;
				col = _LerpColor(
					StateColors[State.Warned],
					StateColors[State.Alerted],
					(_alertLevel - 33f) / 33f);
			} else {
				if (state != State.Alerted) {
					_alertIcon.Show();
					await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
					_alertIcon.Hide();
				}
				state = State.Alerted;
				col = StateColors[State.Alerted];
			}

			_mat.SetShaderParameter("color", col);
		}
		private Color _LerpColor(Color c1, Color c2, float t) {
			return new Color(
				Mathf.Lerp(c1.R, c2.R, t),
				Mathf.Lerp(c1.G, c2.G, t),
				Mathf.Lerp(c1.B, c2.B, t),
				Mathf.Lerp(c1.A, c2.A, t)
			);
		}
	}

}
