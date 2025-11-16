using BusinessObjects.DTO;
using BusinessObjects.DTO.Chapter;
using BusinessObjects.DTO.Resource;
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

        // Resource
        Resource CreateResourceRequestToResource(CreateResourceRequest request);
        ResourceResponse ResourceToResourceResponse(Resource resource);
        List<ResourceResponse> ResourcesToResourceResponses(List<Resource> resources);
        void UpdateResourceFromRequest(UpdateResourceRequest request, Resource resource);
    }
}