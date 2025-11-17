using BusinessObjects.DTO.Answer;
using BusinessObjects.Mapper;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapperlyMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapperlyMapper mapper, ILogger<AnswerService> logger)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<AnswerResponse> CreateAnswerAsync(CreateAnswerRequest request)
        {
            try
            {
                Console.WriteLine("Creating Student Answer...");
                // Map to entity
                var answer = _mapper.CreateAnswerRequestToAnswer(request);
                Console.WriteLine($"Mapped answer - ID: {answer.Id}, Question - ID: {answer.QuestionId}");

                // Create answer
                var createdAnswer = await _answerRepository.CreateAnswerAsync(answer);
                if (createdAnswer == null)
                {
                    throw new InvalidOperationException("Failed to create answer. Answer may already exist.");
                    
                }
                Console.Write($"Answer created successfully with ID: {createdAnswer.Id}");

                // Map to response DTO
                var response = _mapper.AnswerToAnswerResponse(createdAnswer);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating answer: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteAnswerAsync(int answerId)
        {
            try
            {
                Console.WriteLine("Deleting Student Answer...");

                var result = await _answerRepository.DeleteAnswerAsync(answerId);
                if (!result)
                {
                    throw new KeyNotFoundException($"No answer found with ID: {answerId} to delete.");
                }
                else
                {
                    Console.WriteLine($"Answer with ID: {answerId} deleted successfully.");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting answer: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<AnswerResponse>> GetAllAnswersAsync()
        {
            try
            {
                var answers = await _answerRepository.GetAllAnswersAsync();
                return _mapper.AnswersToAnswerResponses(answers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving answers: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<AnswerResponse> GetAnswerByIdAsync(int answerId)
        {
            try
            {
                var answer = await _answerRepository.GetAnswerByIdAsync(answerId);
                if (answer == null)
                {
                    throw new KeyNotFoundException($"No answer found with ID: {answerId}");
                }
                return _mapper.AnswerToAnswerResponse(answer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving answer: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<AnswerResponse> UpdateAnswerAsync(UpdateAnswerRequest request)
        {
            try
            {
                Console.WriteLine($"Updating answer ID: {request.Id}");

                // Get existing answer
                var existingAnswer = await _answerRepository.GetAnswerByIdAsync(request.Id);
                if (existingAnswer == null)
                {
                    throw new KeyNotFoundException($"Answer with ID {request.Id} not found.");
                }

                Console.WriteLine($"Found existing answer ID: {existingAnswer.Id}");


                // Update properties manually to avoid tracking issues
                existingAnswer.SessionId = request.SessionId;
                existingAnswer.QuestionId = request.QuestionId;
                existingAnswer.SelectedAnswer = request.SelectedAnswer;
                existingAnswer.IsCorrect = request.IsCorrect;

                Console.WriteLine($"Updated properties - Session ID: {request.SessionId}, Question ID: {request.QuestionId}, Selected Answer: {request.SelectedAnswer}, IsCorrect: {request.IsCorrect} ");

                // Save changes
                var isUpdated = await _answerRepository.UpdateAnswerAsync(existingAnswer);

                if (!isUpdated)
                {
                    throw new InvalidOperationException("Failed to update resource.");
                }

                Console.WriteLine("Resource updated successfully");

                // Return updated session
                var response = _mapper.AnswerToAnswerResponse(existingAnswer);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAnswerAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
