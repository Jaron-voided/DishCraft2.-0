namespace Application.DTOs;

public class WeekPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<DayPlanDto> DayPlans { get; set; }
    
    public string AppUserId { get; set; }
}