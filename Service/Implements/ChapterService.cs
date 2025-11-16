using BusinessObjects.DTO.Chapter;
using BusinessObjects.Mapper;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IMapperlyMapper _mapper;
        public ChapterService(IChapterRepository chapterRepository, IMapperlyMapper mapperlyMapper)
        {
            _chapterRepository = chapterRepository;
            _mapper = mapperlyMapper;
        }

        public async Task<ChapterResponse> CreateChapterAsync(CreateChapterRequest request)
        {
            try
            {
                Console.WriteLine($"Creating chapter: {request.ChapterName}, Grade: {request.Grade}");

                // Validate input
                if (string.IsNullOrWhiteSpace(request.ChapterName))
                {
                    throw new ArgumentException("Chapter name is required.");
                }

                if (request.Grade <= 0)
                {
                    throw new ArgumentException("Grade must be greater than 0.");
                }

                // Check if chapter already exists
                var exists = await _chapterRepository.ChapterNameExistsAsync(request.ChapterName, request.Grade);
                if (exists)
                {
                    throw new InvalidOperationException($"Chapter '{request.ChapterName}' already exists for grade {request.Grade}.");
                }

                // Map to entity
                var chapter = _mapper.CreateChapterRequestToChapter(request);

                Console.WriteLine($"Mapped chapter - Name: {chapter.Name}, Grade: {chapter.Grade}");

                // Create chapter
                var createdChapter = await _chapterRepository.CreateChapterAsync(chapter);

                if (createdChapter == null)
                {
                    throw new InvalidOperationException("Failed to create chapter. Chapter may already exist.");
                }

                Console.WriteLine($"Chapter created successfully with ID: {createdChapter.Id}");

                // Map to response
                return _mapper.ChapterToChapterResponse(createdChapter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateChapterAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ChapterResponse?> GetChapterByIdAsync(int chapterId)
        {
            try
            {
                var chapter = await _chapterRepository.GetChapterByIdAsync(chapterId);

                if (chapter == null)
                {
                    return null;
                }

                return _mapper.ChapterToChapterResponse(chapter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetChapterByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ChapterResponse>> GetAllChaptersAsync()
        {
            try
            {
                var chapters = await _chapterRepository.GetAllChaptersAsync();
                return _mapper.ChaptersToChapterResponses(chapters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllChaptersAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ChapterResponse> UpdateChapterAsync(UpdateChapterRequest request)
        {
            try
            {
                Console.WriteLine($"Updating chapter ID: {request.Id}, Name: {request.ChapterName}, Grade: {request.Grade}");

                // Validate input
                if (string.IsNullOrWhiteSpace(request.ChapterName))
                {
                    throw new ArgumentException("Chapter name is required.");
                }

                if (request.Grade <= 0)
                {
                    throw new ArgumentException("Grade must be greater than 0.");
                }

                // Get existing chapter
                var existingChapter = await _chapterRepository.GetChapterByIdAsync(request.Id);
                if (existingChapter == null)
                {
                    throw new KeyNotFoundException($"Chapter with ID {request.Id} not found.");
                }

                Console.WriteLine($"Found existing chapter: {existingChapter.Name}");

                // Check if another chapter with same name exists
                var exists = await _chapterRepository.ChapterNameExistsAsync(
                    request.ChapterName,
                    request.Grade,
                    request.Id);

                if (exists)
                {
                    throw new InvalidOperationException($"Another chapter with name '{request.ChapterName}' already exists for grade {request.Grade}.");
                }

                // Update properties manually to avoid tracking issues
                existingChapter.Name = request.ChapterName;
                existingChapter.Grade = request.Grade;
                existingChapter.Description = request.Description;

                Console.WriteLine($"Updated properties - Name: {existingChapter.Name}, Grade: {existingChapter.Grade}");

                // Save changes
                var isUpdated = await _chapterRepository.UpdateChapterAsync(existingChapter);

                if (!isUpdated)
                {
                    throw new InvalidOperationException("Failed to update chapter.");
                }

                Console.WriteLine("Chapter updated successfully");

                // Return updated chapter
                return _mapper.ChapterToChapterResponse(existingChapter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateChapterAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteChapterAsync(int chapterId)
        {
            try
            {
                Console.WriteLine($"Deleting chapter ID: {chapterId}");

                var chapter = await _chapterRepository.GetChapterByIdAsync(chapterId);
                if (chapter == null)
                {
                    throw new KeyNotFoundException($"Chapter with ID {chapterId} not found.");
                }

                var result = await _chapterRepository.DeleteChapterAsync(chapterId);

                Console.WriteLine($"Delete result: {result}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteChapterAsync: {ex.Message}");
                throw;
            }
        }
    }
}
