namespace DentinhoFeliz.Application.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string Pergunta { get; set; }
        public string[] Opcoes { get; set; }
    }
}