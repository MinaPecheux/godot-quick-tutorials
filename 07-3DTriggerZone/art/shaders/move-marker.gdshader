shader_type spatial;

uniform vec4 color : source_color = vec4(1.0);
uniform float radius : hint_range(0.0, 1.0) = 1.0;
uniform float thickness : hint_range(0.0, 0.5) = 0.1;

void fragment() {
	vec2 uv = UV * 2.0 - 1.0;
	float m1 = smoothstep(0, 0.012, radius - length(uv));
	float m2 = smoothstep(0, 0.012, (radius - thickness) - length(uv));
	ALBEDO = color.rgb;
	ALPHA = (m1 - m2) * color.a;
}
