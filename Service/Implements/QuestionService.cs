using BusinessObjects.DTO.Question;
using BusinessObjects.Mapper;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapperlyMapper _mapper;
        private readonly ILessonRepository _lessonRepository;

        public QuestionService(IQuestionRepository questionRepository, IMapperlyMapper mapper, ILessonRepository lessonRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _lessonRepository = lessonRepository;
        }

        public async Task<QuestionResponseDto> CreateQuestionAsync(CreateQuestionRequestDto request, int creatorId)
        {
            try
            {
               
                if (request.LessonId.HasValue)
                 {
                    var lessonExists = await _lessonRepository.GetLessonByIdAsync(request.LessonId.Value);
                   if (lessonExists == null)
                       throw new ArgumentException($"Lesson with ID {request.LessonId} not found.");
                }

                var question = _mapper.CreateQuestionRequestToQuestion(request);
                question.CreatedById = creatorId;
                question.CreatedAt = DateTime.UtcNow;
                var createdQuestion = await _questionRepository.CreateQuestionAsync(question);

                if (createdQuestion == null)
                {
                    throw new InvalidOperationException("Failed to create the question.");
                }
                return _mapper.QuestionToQuestionResponseDto(createdQuestion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateQuestionAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<QuestionResponseDto?> GetQuestionByIdAsync(int questionId)
        {
            try
            {
                var question = await _questionRepository.GetQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return null;
                }
                return _mapper.QuestionToQuestionResponseDto(question);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetQuestionByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<QuestionResponseDto>> GetQuestionsAsync(int? lessonId, string? difficulty)
        {
            try
            {
                var questions = await _questionRepository.GetQuestionsAsync(lessonId, difficulty);
                return _mapper.QuestionsToQuestionResponseDtos(questions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetQuestionsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<QuestionResponseDto> UpdateQuestionAsync(UpdateQuestionRequestDto request)
        {
            try
            {
              
                var existingQuestion = await _questionRepository.GetQuestionByIdAsync(request.Id);
                if (existingQuestion == null)
                {
                    throw new KeyNotFoundException($"Question with ID {request.Id} not found.");
                }
                _mapper.UpdateQuestionFromRequest(request, existingQuestion);
                var isUpdated = await _questionRepository.UpdateQuestionAsync(existingQuestion);
                if (!isUpdated)
                {
                    throw new InvalidOperationException("Failed to update the question.");
                }

              
                return _mapper.QuestionToQuestionResponseDto(existingQuestion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateQuestionAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            try
            {
                var question = await _questionRepository.GetQuestionByIdAsync(questionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Question with ID {questionId} not found.");
                }

               
                 var isInUse = await _questionRepository.IsQuestionInUseAsync(questionId);
                 if (isInUse)
                 {
                    throw new InvalidOperationException("Cannot delete question that is in use by a test.");
                 }
                return await _questionRepository.DeleteQuestionAsync(questionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteQuestionAsync: {ex.Message}");
                throw;
            }
        }
    }
}
