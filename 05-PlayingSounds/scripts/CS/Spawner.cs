using Godot;

namespace Tutorial05_PlayingSounds {

	public partial class Spawner : PathFollow2D {
		[Export] private PackedScene _itemPrefab;
		[Export] private Timer _itemSpawnTimer;
		private Node _scene;

		public override void _Ready() {
			_scene = GetNode("/root/Root");
		}
		
		private void _Spawn() {
			Item item = _itemPrefab.Instantiate<Item>();
			_scene.AddChild(item);
			ProgressRatio = (float) GD.RandRange(0.1f, 0.9f);
			item.GlobalPosition = Position;
			item.Initialize();

			_itemSpawnTimer.WaitTime = GD.RandRange(0.5f, 2f);
			_itemSpawnTimer.Start();
		}
	}

}
