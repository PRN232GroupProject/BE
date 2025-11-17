using BusinessObjects.DTO.Question;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public abstract class MapperHelperBase
    {
       
        protected string MapOptionsToString(Dictionary<string, string> options)
        {
            if (options == null || options.Count == 0)
            {
                return "{}"; 
            }
            return JsonSerializer.Serialize(options);
        }

        protected Dictionary<string, string> MapOptionsToDictionary(string optionsJson)
        {
            if (string.IsNullOrEmpty(optionsJson))
            {
                return new Dictionary<string, string>();
            }
            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<Dictionary<string, string>>(optionsJson, jsonOptions)
                       ?? new Dictionary<string, string>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Options JSON: {ex.Message}");
                return new Dictionary<string, string>();
            }
        }

        protected List<QuestionResponseDto> MapTestQuestionsToQuestionResponseDtos(ICollection<TestQuestion> testQuestions)
        {
            if (testQuestions == null || !testQuestions.Any())
            {
                return new List<QuestionResponseDto>();
            }

            var questions = testQuestions
                .Select(tq => tq.Question)
                .Where(q => q != null)
                .ToList();

           
            return QuestionsToQuestionResponseDtos(questions);
        }
        public abstract List<QuestionResponseDto> QuestionsToQuestionResponseDtos(List<Question> questions);
    }
}
