namespace IAFPS_Back.Models;

public class Agent
{
    public Guid Id { get; init; }
    public Node HomeNode { get; set; } = null!;
    public Node WorkNode { get; set; } = null!;
    public TimeOnly WorkStartTime { get; set; }
    public TimeOnly WorkEndTime { get; set; }
    public int YearsOfExperience { get; set; }
    
    public Edge CurrentEdge { get; set; }
    public decimal TravelledDistanceOnEdge { get; set; }
    public LocalTimeIterator TravelledTimeOnEdge { get; set; }

    private Dictionary<AgentStates, decimal[]> _transitionProbabilities = new()
    {
        [AgentStates.Home] = [0.4m, 0.6m, 0m, 0m],
        [AgentStates.OnRoad] = [0.2m, 0.3m, 0.4m, 0.1m],
        [AgentStates.Work] = [0m, 0.4m, 0.6m, 0m],
        [AgentStates.Resting] = [0m, 0.4m, 0m, 0.6m]
    };
}
