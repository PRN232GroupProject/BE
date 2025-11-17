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
using BusinessObjects.DTO.Test;
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
    public partial class MapperlyMapper : MapperHelperBase, IMapperlyMapper
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

        // Update mapping
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
        public override partial List<QuestionResponseDto> QuestionsToQuestionResponseDtos(List<Question> questions);

       

        // Test mappings
        [MapperIgnoreTarget(nameof(Test.CreatedAt))]
        [MapperIgnoreTarget(nameof(Test.CreatedBy))]
        [MapperIgnoreTarget(nameof(Test.StudentTestSessions))]
        [MapperIgnoreTarget(nameof(Test.TestQuestions))]
        [MapperIgnoreTarget(nameof(Test.CreatedById))] 
        public partial Test CreateTestRequestToTest(CreateTestRequestDto request);

        [MapperIgnoreTarget(nameof(Test.CreatedAt))]
        [MapperIgnoreTarget(nameof(Test.CreatedBy))]
        [MapperIgnoreTarget(nameof(Test.StudentTestSessions))]
        [MapperIgnoreTarget(nameof(Test.TestQuestions))]
        [MapperIgnoreTarget(nameof(Test.CreatedById))] 
        public partial void UpdateTestFromRequest(UpdateTestRequestDto request, Test test);

        [MapperIgnoreTarget(nameof(TestResponseDto.Questions))] 
        public partial List<TestResponseDto> TestsToTestResponseDtos(List<Test> tests);

        [MapProperty(nameof(Test.TestQuestions), nameof(TestResponseDto.Questions))]
        [MapProperty(nameof(Test.CreatedById), nameof(TestResponseDto.CreatedBy))] 
        public partial TestResponseDto TestToTestResponseDto(Test test);
    }
}
