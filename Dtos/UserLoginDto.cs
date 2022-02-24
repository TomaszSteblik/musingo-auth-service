namespace musingo_auth_service.Dtos;

public class UserLoginDto
{
    public string LoginId { get; set; }
    public string PictureUrl { get; set; }
    public string Name { get; set; }
    public double AverageScore { get; set; }
}