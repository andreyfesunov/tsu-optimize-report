namespace BackendBase.Dto;

public class UserInfo
{
    private string? UserId { get; set; }

    public string GetUserId()
    {
        if (UserId != null) return UserId;
        throw new Exception("UserInfo not initialized.");
    }

    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}