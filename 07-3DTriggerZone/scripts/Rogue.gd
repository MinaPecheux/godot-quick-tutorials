extends CharacterBody3D

const SPEED = 4.0
var move_marker_prefab : PackedScene = preload('res://07-3DTriggerZone/assets/move_marker.tscn')
@onready var root : Node3D = $/root/Root
@onready var cam : Camera3D = root.get_node('%Camera3D')
@onready var agent = $Agent

func _physics_process(_delta):
	if Input.is_action_just_released('move-click'):
		var m = get_viewport().get_mouse_position()
		var from = cam.project_ray_origin(m)
		var to = from + cam.project_ray_normal(m) * 1000.0
		var space_state = get_world_3d().direct_space_state
		var query = PhysicsRayQueryParameters3D.new()
		query.from = from; query.to = to; query.collision_mask = 2
		var result = space_state.intersect_ray(query)
		if result.size() > 0:
			var p = move_marker_prefab.instantiate()
			root.add_child(p)
			p.global_position = result['position']
			set_target_position(result['position'])

	if not agent.is_navigation_finished():
		var next = agent.get_next_path_position()
		look_at(Vector3(next.x, global_position.y, next.z), Vector3.UP)
		var direction = (next - global_position).normalized()
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
	else:
		velocity.x = 0
		velocity.z = 0

	move_and_slide()

func set_target_position(pos : Vector3):
	var map = get_world_3d().navigation_map
	var p = NavigationServer3D.map_get_closest_point(map, pos)
	agent.target_position = p
