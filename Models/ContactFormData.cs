namespace CourtComplaintFormBackend.Models
{
    public class ContactFormData
    {
        public int Id { get; set; }          // Unique ID for each contact form entry
        public string Name { get; set; }     // Name of the person filling the form
        public string Email { get; set; }    // Email of the person
        public string Message { get; set; }  // The message sent through the form
        public DateTime SubmittedAt { get; set; }  // When the form was submitted
    }
}
