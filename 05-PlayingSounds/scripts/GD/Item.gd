class_name Item
extends CharacterBody2D

const SPRITES_FOLDER : String = 'res://05-PlayingSounds/art/sprites'
var is_asteroid : bool = false

func initialize():
	velocity = Vector2.LEFT * 350.0
	if randf() < 0.33:
		is_asteroid = true
		$Sprite2D.texture = load(SPRITES_FOLDER + '/asteroid.png')

func _physics_process(_delta):
	move_and_slide()

func on_screen_exited():
	queue_free()
