using System;
/// <summary>
/// Represents an advanced boolean with events.
/// </summary>
public class Flag
{
	bool _up=false;
	/// <summary>
	/// Whether the <see cref="Flag"/> is raised or lowered.
	/// </summary>
	public bool up
    {
		get => _up;
		set
        {
			_up = value;
			OnChanged?.Invoke(value);
			if (value)
            {
				OnRaised?.Invoke();
            } else
            {
				OnLowered?.Invoke();
            }
        }
    }
	public Flag()
    {

    }
	public Flag(bool up)
	{
		_up = up;
	}
	public void Raise()
    {
		up = true;
    }
	public void Lower()
    {
		up = false;
    }
	public bool Toggle()
    {
		up = !up;
		return up;
    }
	public event Action OnRaised;
	public event Action OnLowered;
	public event Action<bool> OnChanged;
}
