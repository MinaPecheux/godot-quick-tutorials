shader_type spatial;

uniform vec4 color : source_color = vec4(1.0);
uniform float thickness = 0.2;

void fragment() {
	float m1 = smoothstep(0, 0.01, thickness - UV.x);
	float m2 = smoothstep(0, 0.01, UV.x - (1.0 - thickness));
	float m3 = smoothstep(0, 0.01, thickness - UV.y);
	float m4 = smoothstep(0, 0.01, UV.y - (1.0 - thickness));
	float m = max(max(m1, m2), max(m3, m4));
	ALBEDO = color.rgb;
	ALPHA = m * color.a;
}
