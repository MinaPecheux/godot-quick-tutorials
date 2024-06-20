extends RichTextLabel

var stylised : bool = false

func _input(event):
	if event is InputEventKey and not event.pressed \
		and event.keycode == KEY_SPACE:
		stylised = not stylised
		if stylised:
			text = '[center]Let\'s [i][font_size=100]do[/font_size][/i]'\
				+ ' some [b][color=#aa0000]styling[/color][/b] in '\
				+ '[img=80]res://icon.svg[/img]![/center]'
		else:
			text = '[center]Let\'s do some styling ' \
				+ 'in [img=80]res://icon.svg[/img]![/center]'
