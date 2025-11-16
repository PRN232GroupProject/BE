using BusinessObjects.DTO;
using BusinessObjects.DTO.Chapter;
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
        UserDTO UserToUserDto(User user);
        LoginResponse UserToLoginResponse(User user);
        User RegisterRequestToUser(RegisterRequest request);
        Chapter CreateChapterRequestToChapter(CreateChapterRequest request);
        ChapterResponse ChapterToChapterResponse(Chapter chapter);
        List<ChapterResponse> ChaptersToChapterResponses(List<Chapter> chapters);
        void UpdateChapterFromRequest(UpdateChapterRequest request, Chapter chapter);
    }
}