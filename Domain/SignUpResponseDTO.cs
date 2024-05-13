namespace HumanResourcesApi.Domain
{
    public class SignUpResponseDTO
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
