extends Node3D

#region Alert system
enum State { IDLE, WARNED, ALERTED }

const ALERT_AMOUNT = 10.0
const STATE_COLORS = {
	State.IDLE: Color('#3cff0f64'), # green
	State.WARNED: Color('#ffd00f64'), # yellow
	State.ALERTED: Color('#ff000f64'), # red
}

var state : State = State.IDLE
var alert_level : float = 0.0
var enemies = []

@onready var mat : ShaderMaterial = %ZoneDebug.get('surface_material_override/0')
@onready var warn_icon : Node3D = $WarnIcon
@onready var alert_icon : Node3D = $AlertIcon
#endregion

func on_body_entered(body : Node3D):
	if body.is_in_group('enemies'):
		detect_enemy(body)

func on_body_exited(body : Node3D):
	if body.is_in_group('enemies'):
		for i in range(enemies.size()):
			if enemies[i] == body:
				enemies.remove_at(i)
				break

func detect_enemy(enemy : Node3D):
	enemies.append(enemy)
	warn_icon.show()
	await get_tree().create_timer(1.0).timeout
	warn_icon.hide()

func _ready():
	warn_icon.hide()
	alert_icon.hide()

func _process(delta):
	update_alert_level(delta)

func update_alert_level(delta):
	if enemies.is_empty():
		if alert_level < 0:
			alert_level = 0
		if alert_level == 0:
			return
		alert_level -= ALERT_AMOUNT * delta
	else:
		if alert_level > 100:
			alert_level = 100
		if alert_level == 100:
			return
		alert_level += enemies.size() * ALERT_AMOUNT * delta

	var col
	if alert_level < 33:
		state = State.IDLE
		col = lerp(
			STATE_COLORS[State.IDLE],
			STATE_COLORS[State.WARNED],
			alert_level / 33.0)
	elif alert_level < 66:
		state = State.WARNED
		col = lerp(
			STATE_COLORS[State.WARNED],
			STATE_COLORS[State.ALERTED],
			(alert_level - 33.0) / 33.0)
	else:
		if state != State.ALERTED:
			alert_icon.show()
			await get_tree().create_timer(1.0).timeout
			alert_icon.hide()
		state = State.ALERTED
		col = STATE_COLORS[State.ALERTED]

	mat.set_shader_parameter('color', col)
