shader_type canvas_item;

uniform sampler2D tex : hint_default_white, repeat_enable;
uniform float speed = 0.3;

void fragment() {
	vec2 uv = vec2(UV.x + speed * 0.1 * TIME, UV.y);
	COLOR = texture(tex, uv);
}
