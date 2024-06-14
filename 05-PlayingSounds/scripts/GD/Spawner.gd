extends PathFollow2D

@export var item_prefab : PackedScene
@export var spawn_timer : Timer
@onready var scene = $/root/Root

func spawn():
	var item = item_prefab.instantiate()
	scene.add_child(item)
	progress_ratio = randf_range(0.1, 0.9)
	item.global_position = position
	item.initialize()

	spawn_timer.wait_time = randf_range(0.5, 2.0)
	spawn_timer.start()

