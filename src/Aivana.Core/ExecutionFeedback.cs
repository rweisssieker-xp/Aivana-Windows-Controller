namespace Aivana.Core;

public enum ExecutionPhase { Waiting, Running, Step, Completed, Cancelled, Failed, Rejected }

// Deliberately excludes action values, arguments and exception messages.
public sealed record ExecutionProgress(Guid TileId, ExecutionPhase Phase, int Step = 0,
    int Steps = 0, int Repetition = 0, int Repetitions = 0);
