using BusinessObjects.DTO;
using BusinessObjects.DTO.Answer;
using BusinessObjects.DTO.Chapter;
using BusinessObjects.DTO.Lesson;
using BusinessObjects.DTO.Question;
using BusinessObjects.DTO.Resource;
using BusinessObjects.DTO.Test;
using BusinessObjects.DTO.TestSession;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.User.Auth;
using BusinessObjects.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    [Mapper]
    public partial class MapperlyMapper : IMapperlyMapper
    {
        // User to UserDto mapping
        [MapProperty(nameof(User.Role.Name), nameof(UserDTO.Role))]
        public partial UserDTO UserToUserDto(User user);

        // User to LoginResponse mapping - only Role name
        [MapProperty(nameof(User.Role.Name), nameof(LoginResponse.Role))]
        public partial LoginResponse UserToLoginResponse(User user);

        // RegisterRequest to User mapping
        public partial User RegisterRequestToUser(RegisterRequest request);

        [MapProperty(nameof(UserRequestDTO.Role), nameof(User.RoleId))]
        public partial User RequestDTOToUser(UserRequestDTO request);

        // Lesson mappings
        [MapProperty(nameof(Lesson.Id), nameof(LessonResponse.LessonId))]
        public partial LessonResponse LessonToLessonResponse(Lesson lesson);
        public partial List<LessonResponse> LessonsToLessonResponses(List<Lesson> lessons);
        public partial Lesson CreateLessonRequestToLesson(CreateLessonRequest request);
        // UpdateLessonRequest → Lesson (partial update)
        [MapperIgnoreTarget(nameof(Lesson.CreatedAt))]
        [MapperIgnoreTarget(nameof(Lesson.CreatedBy))]
        [MapperIgnoreTarget(nameof(Lesson.Resources))]
        [MapperIgnoreTarget(nameof(Lesson.Questions))]
        public partial void UpdateLessonFromRequest(UpdateLessonRequest request, Lesson lesson);

        // Chapter mappings

        [MapProperty(nameof(CreateChapterRequest.ChapterName), nameof(Chapter.Name))]
        public partial Chapter CreateChapterRequestToChapter(CreateChapterRequest request);
        [MapProperty(nameof(Chapter.Name), nameof(ChapterResponse.ChapterName))]
        [MapProperty(nameof(Chapter.Id), nameof(ChapterResponse.ChapterId))]
        [MapProperty(nameof(Chapter.Lessons), nameof(ChapterResponse.Lessons))]
        public partial ChapterResponse ChapterToChapterResponse(Chapter chapter);
        public partial List<ChapterResponse> ChaptersToChapterResponses(List<Chapter> chapters);

        // Update mapping - map only the fields that should be updated

        [MapProperty(nameof(UpdateChapterRequest.ChapterName), nameof(Chapter.Name))]
        [MapperIgnoreTarget(nameof(Chapter.Lessons))]
        public partial void UpdateChapterFromRequest(UpdateChapterRequest request, Chapter chapter);


        // Resource mappings
        [MapProperty(nameof(CreateResourceRequest.ResourceTitle), nameof(Resource.Title))]
        [MapProperty(nameof(CreateResourceRequest.ResourceType), nameof(Resource.Type))]
        [MapProperty(nameof(CreateResourceRequest.ResourceUrl), nameof(Resource.Url))]
        [MapProperty(nameof(CreateResourceRequest.ResourceDescription), nameof(Resource.Description))]
        public partial Resource CreateResourceRequestToResource(CreateResourceRequest request);


        [MapProperty(nameof(Resource.Id), nameof(ResourceResponse.ResourceId))]
        [MapProperty(nameof(Resource.LessonId), nameof(ResourceResponse.LessonId))]
        [MapProperty(nameof(Resource.Title), nameof(ResourceResponse.ResourceTitle))]
        [MapProperty(nameof(Resource.Type), nameof(ResourceResponse.ResourceType))]
        [MapProperty(nameof(Resource.Url), nameof(ResourceResponse.ResourceUrl))]
        [MapProperty(nameof(Resource.Description), nameof(ResourceResponse.ResourceDescription))]
        public partial ResourceResponse ResourceToResourceResponse(Resource resource);
        public partial List<ResourceResponse> ResourcesToResourceResponses(List<Resource> resources);


        [MapProperty(nameof(UpdateResourceRequest.ResourceTitle), nameof(Resource.Title))]
        [MapProperty(nameof(UpdateResourceRequest.ResourceType), nameof(Resource.Type))]
        [MapProperty(nameof(UpdateResourceRequest.ResourceUrl), nameof(Resource.Url))]
        [MapProperty(nameof(UpdateResourceRequest.ResourceDescription), nameof(Resource.Description))]
        public partial void UpdateResourceFromRequest(UpdateResourceRequest request, Resource resource);


        // Question mappings

        [MapperIgnoreTarget(nameof(Question.CreatedAt))]
        [MapperIgnoreTarget(nameof(Question.CreatedBy))]
        [MapperIgnoreTarget(nameof(Question.Lesson))]
        [MapperIgnoreTarget(nameof(Question.CreatedById))]
        public partial Question CreateQuestionRequestToQuestion(CreateQuestionRequestDto request);


        [MapperIgnoreTarget(nameof(Question.CreatedAt))]
        [MapperIgnoreTarget(nameof(Question.CreatedBy))]
        [MapperIgnoreTarget(nameof(Question.Lesson))]
        [MapperIgnoreTarget(nameof(Question.CreatedById))]

        public partial void UpdateQuestionFromRequest(UpdateQuestionRequestDto request, Question question);
        public partial QuestionResponseDto QuestionToQuestionResponseDto(Question question);
        public partial List<QuestionResponseDto> QuestionsToQuestionResponseDtos(List<Question> questions);
        // ==========================================
        // Test Session Mapping
        // ==========================================

        // CreateTestSessionRequest -> StudentTestSession
        [MapperIgnoreTarget(nameof(StudentTestSession.Id))]
        [MapperIgnoreTarget(nameof(StudentTestSession.StudentAnswers))]
        public partial StudentTestSession CreateTestSessionRequestToStudentTestSession(CreateTestSessionRequest request);


        // StudentTestSession -> StudentTestSessionResponse
        [MapProperty(nameof(StudentTestSession.Id), nameof(StudentTestSessionResponse.SessionId))]
        [MapProperty(nameof(StudentTestSession.StudentAnswers), nameof(StudentTestSessionResponse.Answers))]
        public partial StudentTestSessionResponse StudentTestSessionToResponse(StudentTestSession entity);


        // StudentAnswer -> StudentAnswerResponse
        public partial StudentAnswerResponse StudentAnswerToStudentAnswerResponse(StudentAnswer answer);
        public partial List<StudentAnswerResponse> StudentAnswersToResponses(List<StudentAnswer> answers);

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


        // Test Session mappings
        [MapProperty(nameof(CreateTestSessionRequest.UserId), nameof(StudentTestSession.UserId))]
        [MapProperty(nameof(CreateTestSessionRequest.TestId), nameof(StudentTestSession.TestId))]
        [MapProperty(nameof(CreateTestSessionRequest.StartTime), nameof(StudentTestSession.StartTime))]
        [MapProperty(nameof(CreateTestSessionRequest.EndTime), nameof(StudentTestSession.EndTime))]
        [MapProperty(nameof(CreateTestSessionRequest.Score), nameof(StudentTestSession.Score))]
        [MapProperty(nameof(CreateTestSessionRequest.Status), nameof(StudentTestSession.Status))]
        public partial StudentTestSession CreateTestSessionRequestToTestSession(CreateTestSessionRequest request);

        [MapProperty(nameof(StudentTestSession.Id), nameof(TestSessionResponse.Id))]
        [MapProperty(nameof(StudentTestSession.UserId), nameof(TestSessionResponse.UserId))]
        [MapProperty(nameof(StudentTestSession.TestId), nameof(TestSessionResponse.TestId))]
        [MapProperty(nameof(StudentTestSession.StartTime), nameof(TestSessionResponse.StartTime))]
        [MapProperty(nameof(StudentTestSession.EndTime), nameof(TestSessionResponse.EndTime))]
        [MapProperty(nameof(StudentTestSession.Score), nameof(TestSessionResponse.Score))]
        [MapProperty(nameof(StudentTestSession.Status), nameof(TestSessionResponse.Status))]
        public partial TestSessionResponse TestSessionToTestSessionResponse(StudentTestSession session);
        public partial List<TestSessionResponse> TestSessionsToTestSessionResponses(List<StudentTestSession> sessions);

        [MapProperty(nameof(UpdateTestSessionRequest.Id), nameof(StudentTestSession.Id))]
        [MapProperty(nameof(UpdateTestSessionRequest.UserId), nameof(StudentTestSession.UserId))]
        [MapProperty(nameof(UpdateTestSessionRequest.TestId), nameof(StudentTestSession.TestId))]
        [MapProperty(nameof(UpdateTestSessionRequest.StartTime), nameof(StudentTestSession.StartTime))]
        [MapProperty(nameof(UpdateTestSessionRequest.EndTime), nameof(StudentTestSession.EndTime))]
        [MapProperty(nameof(UpdateTestSessionRequest.Score), nameof(StudentTestSession.Score))]
        [MapProperty(nameof(UpdateTestSessionRequest.Status), nameof(StudentTestSession.Status))]
        public partial void UpdateTestSessionFromRequest(UpdateTestSessionRequest request, StudentTestSession session);

        // Answer mappings
        [MapProperty(nameof(CreateAnswerRequest.SessionId), nameof(StudentAnswer.SessionId))]
        [MapProperty(nameof(CreateAnswerRequest.QuestionId), nameof(StudentAnswer.QuestionId))]
        [MapProperty(nameof(CreateAnswerRequest.SelectedAnswer), nameof(StudentAnswer.SelectedAnswer))]
        public partial StudentAnswer CreateAnswerRequestToAnswer(CreateAnswerRequest request);

        [MapProperty(nameof(StudentAnswer.Id), nameof(AnswerResponse.Id))]
        [MapProperty(nameof(StudentAnswer.SessionId), nameof(AnswerResponse.SessionId))]
        [MapProperty(nameof(StudentAnswer.QuestionId), nameof(AnswerResponse.QuestionId))]
        [MapProperty(nameof(StudentAnswer.SelectedAnswer), nameof(AnswerResponse.SelectedAnswer))]
        public partial AnswerResponse AnswerToAnswerResponse(StudentAnswer answer);
        public partial List<AnswerResponse> AnswersToAnswerResponses(List<StudentAnswer> answers);

        [MapProperty(nameof(UpdateAnswerRequest.Id), nameof(StudentAnswer.Id))]
        [MapProperty(nameof(UpdateAnswerRequest.SessionId), nameof(StudentAnswer.SessionId))]
        [MapProperty(nameof(UpdateAnswerRequest.QuestionId), nameof(StudentAnswer.QuestionId))]
        [MapProperty(nameof(UpdateAnswerRequest.SelectedAnswer), nameof(StudentAnswer.SelectedAnswer))]
        public partial void UpdateAnswerFromRequest(UpdateAnswerRequest request, StudentAnswer answer);
    }
}
