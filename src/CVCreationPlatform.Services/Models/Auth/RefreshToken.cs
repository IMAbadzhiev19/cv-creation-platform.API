namespace CVCreationPlatform.AuthService.Models.Auth
{
	public class RefreshToken
	{
		public string Token { get; set; } = null!;
		public DateTime Created { get; set; }
		public DateTime Expires { get; set; }
	}
}
