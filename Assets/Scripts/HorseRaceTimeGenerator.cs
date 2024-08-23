using UnityEngine;
/// <summary>
/// The <c>HorseRaceTimeGenerator</c> class generates an array of times that will be assigned to each racer.
/// </summary>
public class HorseRaceTimeGenerator : IRaceTimeGenerator
{
    private int _count;
    private float _minTime;
    private float _maxTime;
    private float _minIntervalPercentage;
    private float _maxIntervalPercentage;

    /// <summary>
    /// Constructs an instance of the <c>HorseRaceTimeGenerator</c> class.
    /// </summary>
    /// <param name="count">The number of times to generate; typically equals the number of racers in the race.</param>
    /// <param name="minTime">The base minimum amount of time the race will take.</param>
    /// <param name="maxTime">The base maximum amount of time the race will take.</param>
    /// <param name="minIntervalPercentage">The minimum amount of variation from a racer's initially calculated time.</param>
    /// <param name="maxIntervalPercentage">The maximum amount of variation from a racer's initially calculated time.</param>
    public HorseRaceTimeGenerator(int count, float minTime, float maxTime, float minIntervalPercentage, float maxIntervalPercentage)
    {
        _count = count;
        _minTime = minTime;
        _maxTime = maxTime;
        _minIntervalPercentage = minIntervalPercentage;
        _maxIntervalPercentage = maxIntervalPercentage;
    }

    public float[] GenerateRaceTimes()
    {
        if (_count < 1)
        {
            Debug.LogWarning($"{this}: Tried generating times with a count less than one ({_count}). Changing count to 1.");
            _count = 1;
        }

        float[] value = new float[_count];
        value[0] = _minTime;

        if (_count == 1)
        {
            return value;
        }

        value[value.Length - 1] = _maxTime;
        float interval = (_maxTime - _minTime) / (_count - 1);

        //  We calculate times by evenly spacing out times in between the min and max times...
        for (int i = 1; i < value.Length - 1; i++)
        {
            float time = _minTime + (interval * i);

            //  Then applying an offset to provide a feeling of randomness.
            float offset = Random.Range(interval * _minIntervalPercentage, interval * _maxIntervalPercentage) * ((Random.Range(0, 2) * 2) - 1);
            time += offset;

            //  Calculating interval percentage this way will sometimes result in a time being lesser than its previous, but we can
            //  use situations like these to manufacture close races between horses. 
            if (time <= value[i - 1])
            {
                time = value[i - 1] + Random.Range(0.03f, 0.08f);
            }
            time = Mathf.Round(time * 100f) / 100f;
            value[i] = time;        
        }

        return value;
    }
}
