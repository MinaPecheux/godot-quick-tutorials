using Godot;

public partial class TimeCounter : Control {
	private int _totalTimeInSecs = 0;

	public override void _Ready() {
		// start Timer at specific time:
		// (or use 'Autostart' property)
		GetNode<Timer>("Timer").Start();
	}
	
	private void _OnTimerTimeout() {
		_totalTimeInSecs++;
		int m = (int)(_totalTimeInSecs / 60f);
		int s = _totalTimeInSecs - m * 60;
		GetNode<Label>("Label").Text = m.ToString("D2")
			+ ":" + s.ToString("D2");
	}
}
