extends CharacterBody2D

const SPEED = 400.0

func _physics_process(_delta):
	var direction = Input.get_vector(
		'ui_left', 'ui_right', 'ui_up', 'ui_down')

	if direction.length():
		velocity = direction * SPEED
		look_at(position + direction)
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.y = move_toward(velocity.y, 0, SPEED)

	move_and_slide()
