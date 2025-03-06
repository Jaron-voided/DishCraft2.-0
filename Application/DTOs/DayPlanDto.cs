namespace Application.DTOs;

public class DayPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<DayPlanRecipeDto> DayPlanRecipes { get; set; }
    public string AppUserId { get; set; }

    public Guid WeekPlanId { get; set; }
    public WeekPlanDto WeekPlan { get; set; }
}