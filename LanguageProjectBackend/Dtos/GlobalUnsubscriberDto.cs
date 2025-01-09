namespace LanguageProjectBackend.Dtos
{
    public class GlobalUnsubscriberDto
    {
        public string Email { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
