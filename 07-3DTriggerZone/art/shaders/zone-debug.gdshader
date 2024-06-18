shader_type spatial;

uniform vec4 color : source_color = vec4(1.0);

void fragment() {
	vec2 uv = UV * 2.0 - 1.0;
	float m1 = smoothstep(0, 0.012, 1.0 - length(uv));
	float m2 = smoothstep(0, 0.012, 0.98 - length(uv));
	ALBEDO = color.rgb;
	ALPHA = (m1 - m2) * color.a + m2 * min(color.a * 0.05, 0.05);
}
