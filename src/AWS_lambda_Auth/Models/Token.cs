namespace AWS_lambda_Auth.Models;

public record class Token(string TokenId, string AccessToken, string refleshToken);