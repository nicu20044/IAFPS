using Microsoft.OpenApi.Extensions;

namespace IAFPS_Back.Models;

public class Edge(
    Node start,
    Node end,
    int roadLanesCount,
    decimal length,
    int maxAdmissibleSpeed)
{
    public int Id { get; init; } = 1;
    public Node Start { get; set; } = start;
    public Node End { get; set; } = end;
    public RoadState CurrentState { get; private set; } = RoadState.Free;

    private readonly Dictionary<RoadState, decimal[]> _transitionsProbability = new()
    {
        [RoadState.Free] = [0.6m, 0.2m, 0.2m],
        [RoadState.Moderated] = [0.2m, 0.5m, 0.3m],
        [RoadState.Congested] = [0.1m, 0.3m, 0.6m]
    };

    private int MaxAdmissibleCarsCount { get; } = (int)(length * roadLanesCount) / 5;
    private int CurrentCarsCount { get; set; }
    private int RoadLanesCount { get; } = roadLanesCount;
    private int MaxAdmissibleSpeed { get; } = maxAdmissibleSpeed;
    private decimal Length { get; } = length;


    public decimal GetWeight() => MaxAdmissibleSpeed * RoadLanesCount * MaxAdmissibleCarsCount /
                                  (Length * (CurrentCarsCount != 0 ? CurrentCarsCount : 1));

    public void AddCarsOnRoad() => ++CurrentCarsCount;
    public void RemoveCarsOnRoad() => --CurrentCarsCount;

    public void UpdateTransitionProbability()
    {
        decimal trafficDensity = (decimal)CurrentCarsCount / MaxAdmissibleCarsCount;

        switch (trafficDensity)
        {
            case < 0.3m:
                _transitionsProbability[RoadState.Free] = [0.7m, 0.2m, 0.1m];
                _transitionsProbability[RoadState.Moderated] = [0.3m, 0.5m, 0.2m];
                _transitionsProbability[RoadState.Congested] = [0.1m, 0.2m, 0.7m];
                break;
            case >= 0.3m and < 0.7m:
                _transitionsProbability[RoadState.Free] = [0.4m, 0.4m, 0.2m];
                _transitionsProbability[RoadState.Moderated] = [0.2m, 0.6m, 0.2m];
                _transitionsProbability[RoadState.Congested] = [0.1m, 0.3m, 0.6m];
                break;
            default:
                _transitionsProbability[RoadState.Free] = [0.1m, 0.2m, 0.7m];
                _transitionsProbability[RoadState.Moderated] = [0.1m, 0.5m, 0.4m];
                _transitionsProbability[RoadState.Congested] = [0.05m, 0.15m, 0.8m];
                break;
        }
    }
}
