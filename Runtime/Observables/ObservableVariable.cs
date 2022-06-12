using System;

public sealed class ObservableVariable<T>
{
    private T value;

    public T Value
    {
        get => value;

        set
        {
            this.value = value;

            OnChange?.Invoke(value);
        }
    }

    public event Action<T> OnChange;
}