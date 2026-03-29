#nullable disable
namespace FlowerBloomStore.Application.DTOs
{
    public class RegisterResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string UserId { get; set; }
    }
}
