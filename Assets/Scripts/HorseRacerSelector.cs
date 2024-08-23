using System;
using System.Collections.Generic;
using TMPro;

public class HorseRacerSelector
{
    private int _index = 0;
    public int Index { get => _index; }
    private HorseRaceManager _manager;
    private List<HorseRacerData> _data;
    public List<HorseRacerData> Data { get => _data; }

    public event Action OnIncrement;
    public event Action OnDecrement;
    public event Action OnSelect;

    public HorseRacerSelector(HorseRaceManager manager)
    {
        _manager = manager;
        _data = manager.Data;
    }

    public void Increment()
    {
        if (_index < _data.Count - 1)
        {
            _index++;
        }

        else
        {
            _index = 0;
        }
        OnIncrement?.Invoke();
    }

    public void Decrement()
    {
        if (_index > 0)
        {
            _index--;
        }

        else
        {
            _index = _data.Count - 1;
        }
        OnDecrement?.Invoke();
    }

    public void SelectRacer()
    {
        _manager.SetPlayerRacer(_index);
        OnSelect?.Invoke();
    }
}