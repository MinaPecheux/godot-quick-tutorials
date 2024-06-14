extends Node

const SHAKE_TIME : float = 1.0
const SHAKE_AMOUNT : float = 5000.0

@onready var cam : Camera2D = $Camera2D
@onready var noise = FastNoiseLite.new()

var cam_base_pos : Vector2
var shake_delay : float

func shake():
	cam_base_pos = cam.global_position
	shake_delay = SHAKE_TIME

func _process(delta):
	if shake_delay > 0.0:
		cam.global_position = cam_base_pos + \
			Vector2(get_noise(0), get_noise(1))
		shake_delay -= delta

func get_noise(_seed : int):
	noise.seed = _seed
	return noise.get_noise_1d(randf() * shake_delay) * SHAKE_AMOUNT
