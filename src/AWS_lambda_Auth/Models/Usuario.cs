namespace AWS_lambda_Auth.Models;

public record class User(string Cpf = "", string Email = "", string Nome = "", string Password = "");
