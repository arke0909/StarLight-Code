/*using UnityEngine;


public class NotifyValue<T> : MonoBehaviour where T : MonoBehaviour
{
    public delegate void ValueChanged(T prev, T next);

    public event ValueChanged OnValueChanged;
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            T before = _value;
            _value = value;
            if ((before == null && value != null) || !before.Equals(_value))
            {
                OnValueChanged?.Invoke(before, _value);
            }
        }
    }

    public NotifyValue(T value)
    {
        _value = value;
    }
    public NotifyValue()
    {
        _value = default(T);
    }

}
*/