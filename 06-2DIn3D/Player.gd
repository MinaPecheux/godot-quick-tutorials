extends Node3D

const MOVE_TIME : float = 1.0
var moving : bool = false

func _input(event : InputEvent):
	if event.is_action_pressed('ui_accept'):
		move_to(Vector3(randf_range(-4, 4), 0, randf_range(-4, 4)))

func move_to(pos : Vector3):
	if moving:
		return

	moving = true
	var p_len = pos.length() * 100.0 / 6.0
	var tween = get_tree().create_tween()
	tween.set_parallel(true)
	tween.tween_property(self, 'position', pos, MOVE_TIME)\
		.set_ease(Tween.EASE_IN_OUT).set_trans(Tween.TRANS_QUART)
	tween.tween_property(%ProgressBar, 'value', p_len, MOVE_TIME)\
		.set_ease(Tween.EASE_IN_OUT).set_trans(Tween.TRANS_QUART)
	tween.tween_callback(func(): moving = false)
