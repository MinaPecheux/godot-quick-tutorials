extends CharacterBody2D

const SPEED = 700.0
@export var pickup_audio : AudioStream
@export var hit_audio : AudioStream
@onready var audio = $AudioStreamPlayer

func _physics_process(_delta):
	var dir = Input.get_axis('ui_up', 'ui_down')
	if not is_zero_approx(dir):
		velocity.y = dir * SPEED
	else:
		velocity.y = move_toward(velocity.y, 0, SPEED)
	move_and_slide()

func on_area_2d_body_entered(body : Node2D):
	if body is Item:
		var i = body as Item
		if i.is_asteroid:
			$/root/Root/ScreenShake.shake()
			audio.stream = hit_audio
		else: audio.stream = pickup_audio
		audio.play()
		body.queue_free()
