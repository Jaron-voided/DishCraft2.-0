namespace API.UserDtos;

public class UserDto
{
    public string DisplayName { get; set; }
    public string Token { get; set; }
    public string Image { get; set; }
    public string Username { get; set; }
    
    // Add statistics
    public int IngredientsCount { get; set; }
    public int RecipesCount { get; set; }
    public int DayPlansCount { get; set; }
}