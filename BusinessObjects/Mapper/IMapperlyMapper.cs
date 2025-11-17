using BusinessObjects.DTO;
using BusinessObjects.DTO.Answer;
using BusinessObjects.DTO.Chapter;
using BusinessObjects.DTO.Lesson;
using BusinessObjects.DTO.Question;
using BusinessObjects.DTO.Resource;
using BusinessObjects.DTO.Test;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.User.Auth;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public interface IMapperlyMapper
    {
        // User
        UserDTO UserToUserDto(User user);
        LoginResponse UserToLoginResponse(User user);
        User RegisterRequestToUser(RegisterRequest request);
        User RequestDTOToUser(UserRequestDTO request);

        // Chapter
        Chapter CreateChapterRequestToChapter(CreateChapterRequest request);
        ChapterResponse ChapterToChapterResponse(Chapter chapter);
        List<ChapterResponse> ChaptersToChapterResponses(List<Chapter> chapters);
        void UpdateChapterFromRequest(UpdateChapterRequest request, Chapter chapter);
        
        List<LessonResponse> LessonsToLessonResponses(List<Lesson> lessons);
        LessonResponse LessonToLessonResponse(Lesson lesson);
        Lesson CreateLessonRequestToLesson(CreateLessonRequest request);
        void UpdateLessonFromRequest(UpdateLessonRequest request, Lesson lesson);

        // Resource
        Resource CreateResourceRequestToResource(CreateResourceRequest request);
        ResourceResponse ResourceToResourceResponse(Resource resource);
        List<ResourceResponse> ResourcesToResourceResponses(List<Resource> resources);
        void UpdateResourceFromRequest(UpdateResourceRequest request, Resource resource);

        //Question
        Question CreateQuestionRequestToQuestion(CreateQuestionRequestDto request);
        void UpdateQuestionFromRequest(UpdateQuestionRequestDto request, Question question);
        QuestionResponseDto QuestionToQuestionResponseDto(Question question);
        List<QuestionResponseDto> QuestionsToQuestionResponseDtos(List<Question> questions);

        //Test

        Test CreateTestRequestToTest(CreateTestRequestDto request);
        void UpdateTestFromRequest(UpdateTestRequestDto request, Test test);
        TestResponseDto TestToTestResponseDto(Test test);
        List<TestResponseDto> TestsToTestResponseDtos(List<Test> tests);
    }
}