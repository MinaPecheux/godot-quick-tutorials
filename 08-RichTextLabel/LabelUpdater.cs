using Godot;

public partial class LabelUpdater : RichTextLabel {

	private bool _stylised = false;

	public override void _Input(InputEvent @event) {
		if (@event is InputEventKey k && !k.Pressed &&
			k.Keycode == Key.Space) {
			_stylised = !_stylised;
			if (_stylised)
				Text = "[center]Let's [i][font_size=100]do[/font_size][/i]"
					+ " some [b][color=#aa0000]styling[/color][/b] in "
					+ "[img=80]res://icon.svg[/img]![/center]";
			else
				Text = "[center]Let's do some styling "
					+ "in [img=80]res://icon.svg[/img]![/center]";
		}
	}

}
